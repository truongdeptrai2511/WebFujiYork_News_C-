namespace SV19T1081026.AdminTool.Models
{
    /// <summary>
    /// Đầu vào dùng cho tìm kiếm, phân trang đối với bài viết
    /// </summary>
    public class PostSearchInput : PaginationSearchInput
    {
        /// <summary>
        /// Phân loại bài viết cần tìm (0 nếu tìm tất cả phân loại)
        /// </summary>
        public int CategoryId { get; set; }
    }
}