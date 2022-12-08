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
        /// Tìm kiếm và lấy dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        IList<PostComment> List(int page = 1, int pageSize = 20, string searchValue = "");
        /// <summary>
        /// Đến số lượng phân loại bình luận
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(String searchValue = "");
        /// <summary>
        /// Lấy một phân loại bình luận theo commentId
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        PostComment Get(int commentId);
        /// <summary>
        /// Lấy một phân loại bình luận
        /// </summary>
        /// <param name="commentContent"></param>
        /// <returns></returns>
        PostComment Get(String commentContent);
        /// <summary>
        /// Cập nhật phân loại bình luận
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(PostComment data);
        /// <summary>
        /// Cập nhật phân loại bình luận
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(PostComment data);
        /// <summary>
        /// Xoá bình luận
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        bool Delete(int commentId);
        /// <summary>
        /// Kiểm tra xem 1 phân loại tinh hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        bool IsAccepted(int commentId);


    }

}
