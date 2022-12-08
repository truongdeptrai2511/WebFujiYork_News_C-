using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1081026.DomainModels;
using SV19T1081026.DataLayers;
using System.Configuration;

namespace SV19T1081026.BusinessLayers
{
    public class CommentService
    {
        private static IPostCommentDAL postCommentDB;

        static CommentService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            postCommentDB = new DataLayers.SqlServer.PostCommentDAL(connectionString);
        }
        /// <summary>
        /// Lấy comment theoo id
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public static PostComment GetComment(int commentId)
        {
            return postCommentDB.Get(commentId);
        }
        /// <summary>
        /// Lấy toàn bộ comment
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<PostComment> ListComment(int page,
                                                int pageSize,
                                                string searchValue,
                                                out int rowCount)
        {
            rowCount = postCommentDB.Count(searchValue);
            return postCommentDB.List(page, pageSize, searchValue).ToList();
        }
    }
}
