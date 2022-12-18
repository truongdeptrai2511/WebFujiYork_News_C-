using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1081026.DomainModels;

namespace SV19T1081026.DataLayers
{
    /// <summary>
    /// Định nghĩa các chưc năng xử lý dữ liệu liên quan đến phân loại bình luận
    /// </summary>
    public interface IPostCommentDAL
    {
        /// <summary>
        /// Lấy danh sách bình luận
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        IList<PostComment> List(int page = 1, int pageSize = 20, string searchValue = "", long postId = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postCommentId"></param>
        /// <returns></returns>
        PostComment Get(long postCommentId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long Add(PostComment data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postCommentId"></param>
        /// <returns></returns>
        bool ChangeState(long postCommentId);
        /// <summary>
        /// /
        /// </summary>
        /// <param name="postCommentId"></param>
        /// <returns></returns>
        bool Delete(long postCommentId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        int Count(string searchValue = "", long postId = 0);
    }

}
