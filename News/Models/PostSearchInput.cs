using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV19T1081026.AdminTool.Models;
namespace News.Models
{
    public class PostSearchInput : PaginationSearchInput
    {
        /// <summary>
        /// Phân loại bài viết cần tìm (0 nếu tìm tất cả phân loại)
        /// </summary>
        public int CategoryId { get; set; }
    }
}