using SV19T1081026.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081026.DataLayers.SqlServer
{
    public class PostCommentDAL : _BaseDAL, IPostCommentDAL
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public PostCommentDAL(String connectionString) : base(connectionString) { }
        public int Add(PostComment data)
        {
            int result = 0;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"";
                    cmd.CommandType = CommandType.Text;
                    

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return result;
        }

        public int Count(string searchValue = "")
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            int count = 0;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT COUNT(*) 
                                        FROM PostComment
                                        WHERE (@searchValue = '') OR (CreatedTime LIKE @searchValue)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return count;
        }

        public bool Delete(int commentId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM PostComment WHERE CommentId = @commentId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@commentId", commentId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }

        public PostComment Get(int commentId)
        {
            PostComment data = null;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM PostComment WHERE CommentId = @commentId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentId", commentId);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new PostComment()
                            {
                                CommentId = Convert.ToInt32(dbReader["CommentId"]),
                                CreatedTime = Convert.ToDateTime(dbReader["CreatedTime"]),
                                CommentContent = Convert.ToString(dbReader["CommentContent"]),
                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public PostComment Get(string commentContent)
        {
            PostComment data = null;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM PostComment WHERE CommentContent = @commentContent";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentContent", commentContent);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new PostComment()
                            {
                                CommentId = Convert.ToInt32(dbReader["CommentId"]),
                                CreatedTime = Convert.ToDateTime(dbReader["CreatedTime"]),
                                CommentContent = Convert.ToString(dbReader["CommentContent"]),
                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool InUsed(int commentId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT	CASE 
			                                WHEN EXISTS(SELECT * FROM PostComment WHERE CommentId = @commentId) THEN 1
			                                ELSE 0
		                                END AS InUsed";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentId", commentId);
                    result = Convert.ToBoolean(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return result;
        }

        public bool IsAccepted(int commentId)
        {
            throw new NotImplementedException();
        }

        public IList<PostComment> List(int page = 1, int pageSize = 20, string searchValue = "")
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            List<PostComment> data = new List<PostComment>();
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT *
                                        FROM (
		                                        SELECT	*, ROW_NUMBER() OVER(ORDER BY CommentId) AS RowNumber
		                                        FROM	PostComment
		                                        WHERE	(@searchValue = N'') OR (CommentContent LIKE @searchValue)
	                                        ) AS c
                                        WHERE (@pageSize = 0) OR (c.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize)
                                        ORDER BY c.RowNumber;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new PostComment()
                            {
                                CommentId = Convert.ToInt32(dbReader["CommentId"]),
                                CreatedTime = Convert.ToDateTime(dbReader["CreatedTime"]),
                                CommentContent = Convert.ToString(dbReader["CommentContent"]),
                            });
                        }
                        dbReader.Close();
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(PostComment data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE PostComment 
                                        SET CommentContent = @CommentContent, CreatedTime = @CreatedTime 
                                        WHERE CommentId = @CommentId";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentId", data.CommentId);
                    cmd.Parameters.AddWithValue("@CommentContent", data.CommentContent);
                    cmd.Parameters.AddWithValue("@CreatedTime", data.CreatedTime);
                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }
    }
}
