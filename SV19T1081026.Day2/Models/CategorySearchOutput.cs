using SV19T1081026.DomainModels;
using System.Collections.Generic;


namespace SV19T1081026.AdminTool.Models
{
    /// <summary>
    /// Kết quả tìm kiếm, phân loại tin
    /// </summary>
    public class CategorySearchOutput : PaginationSearchOutput
    {

        /// <summary>
        /// Danh sách loại tin được hiển thị trên trang
        /// </summary>
        public List<PostCategory> Data { get; set; }
    }
}