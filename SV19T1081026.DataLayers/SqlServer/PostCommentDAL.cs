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
        public PostCommentDAL(string connectionString) : base(connectionString)
        {
        }

        public long Add(PostComment data)
        {
            long postCommentId = 0;
            bool itemIsAccepted = data.IsAccepted;
            long isAccepted = Convert.ToInt32(itemIsAccepted);
            isAccepted = 1;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO PostComment(CreatedTime, CommentContent, IsAccepted, UserId, PostId)
                                        VALUES(@CreatedTime, @CommentContent, @IsAccepted, @UserId, @PostId);
                                        SELECT SCOPE_IDENTITY()";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CreatedTime", data.CreatedTime);
                    cmd.Parameters.AddWithValue("@CommentContent", data.CommentContent ?? "");
                    cmd.Parameters.AddWithValue("@IsAccepted", isAccepted);
                    cmd.Parameters.AddWithValue("@UserId", data.UserId);
                    cmd.Parameters.AddWithValue("@PostId", data.PostId);


                    postCommentId = Convert.ToInt64(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return postCommentId;
        }

        public bool ChangeState(long postCommentId)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE  PostComment
                                        SET     IsAccepted = ~IsAccepted
                                        WHERE   CommentId = @CommentId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentId", postCommentId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }

        public int Count(string searchValue = "", long postId = 0)
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            int count = 0;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT	COUNT(*)
		                                FROM	PostComment as pc
				                                JOIN Post as p on p.PostId = pc.PostId
				                                JOIN UserAccount as u on u.UserId = pc.UserId
		                                WHERE	((@searchValue = N'') OR (p.Title LIKE @searchValue) OR (u.UserName LIKE @searchValue)) 
                                                AND ((@PostId = 0) OR (p.PostId = @PostId))";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@PostId", postId);

                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return count;
        }

        public bool Delete(long postCommentId)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM PostComment
                                        WHERE   CommentId = @CommentId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentId", postCommentId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }

        public PostComment Get(long postCommentId)
        {
            PostComment data = null;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT pc.*,p.Title,u.UserName,u.FirstName,u.LastName 
                                        FROM PostComment as pc
		                                     JOIN Post as p on p.PostId = pc.PostId
		                                     JOIN UserAccount as u on u.UserId = pc.UserId
                                        WHERE pc.CommentId = @CommentId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentId", postCommentId);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new PostComment()
                            {
                                CommentId = Convert.ToInt32(dbReader["CommentId"]),
                                CreatedTime = Convert.ToDateTime(dbReader["CreatedTime"]),
                                CommentContent = Convert.ToString(dbReader["CommentContent"]),
                                IsAccepted = Convert.ToBoolean(dbReader["IsAccepted"]),
                                UserId = Convert.ToInt32(dbReader["UserId"]),
                                PostId = Convert.ToInt32(dbReader["PostId"]),
                                Commenter = new UserAccount()
                                {
                                    UserId = Convert.ToInt32(dbReader["PostId"]),
                                    UserName = Convert.ToString(dbReader["UserName"]),
                                    FirstName = Convert.ToString(dbReader["FirstName"]),
                                    LastName = Convert.ToString(dbReader["LastName"]),
                                },
                                Post = new Post()
                                {
                                    PostId = Convert.ToInt32(dbReader["PostId"]),
                                    Title = Convert.ToString(dbReader["Title"]),
                                }
                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public IList<PostComment> List(int page = 1, int pageSize = 20, string searchValue = "", long postId = 0)
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
		                                        SELECT	pc.*,p.Title,u.UserName,u.FirstName,u.LastName, ROW_NUMBER() OVER(ORDER BY pc.CommentId DESC) AS RowNumber
		                                        FROM	PostComment as pc
				                                        JOIN Post as p on p.PostId = pc.PostId
				                                        JOIN UserAccount as u on u.UserId = pc.UserId
		                                        WHERE	((@searchValue = N'') OR (p.Title LIKE @searchValue) OR (u.UserName LIKE @searchValue)) 
                                                        AND ((@PostId = 0) OR (p.PostId = @PostId))
	                                        ) AS c
                                        WHERE @pageSize = 0 OR ( c.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize)
                                        ORDER BY c.RowNumber;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@PostId", postId);
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
                                IsAccepted = Convert.ToBoolean(dbReader["IsAccepted"]),
                                UserId = Convert.ToInt32(dbReader["UserId"]),
                                PostId = Convert.ToInt32(dbReader["PostId"]),
                                Commenter = new UserAccount()
                                {
                                    UserId = Convert.ToInt32(dbReader["PostId"]),
                                    UserName = Convert.ToString(dbReader["UserName"]),
                                    FirstName = Convert.ToString(dbReader["FirstName"]),
                                    LastName = Convert.ToString(dbReader["LastName"]),
                                },
                                Post = new Post()
                                {
                                    PostId = Convert.ToInt32(dbReader["PostId"]),
                                    Title = Convert.ToString(dbReader["Title"]),
                                }
                            });
                        }
                        dbReader.Close();
                    }
                }
                connection.Close();
            }
            return data;
        }
    }
}
