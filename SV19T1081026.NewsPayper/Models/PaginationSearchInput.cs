

namespace SV19T1081026.NewsPayper.Models
{
    /// <summary>
    /// Thông tin đầu vào để tìm kiếm, phân trang
    /// </summary>
    public class PaginationSearchInput
    {
        /// <summary>
        /// Trang cần hiển thị
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Số dòng trên mỗi trang 
        /// </summary>
        public int PageSize { get; set; }

        public string SearchValue { get; set; }
    }
}