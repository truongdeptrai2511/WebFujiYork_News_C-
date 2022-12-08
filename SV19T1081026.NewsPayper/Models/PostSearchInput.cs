namespace SV19T1081026.NewsPayper.Models
{
    public class PostSearchInput : PaginationSearchInput
    {
        /// <summary>
        /// Phân loại bài viết cần tìm (0 nếu tìm tất cả phân loại)
        /// </summary>
        public int CategoryId { get; set; }
        
        public int PostId { get; set; }
    }
}