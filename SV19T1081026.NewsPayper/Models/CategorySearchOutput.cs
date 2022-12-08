using SV19T1081026.DomainModels;
using System.Collections.Generic;
using SV19T1081026.BusinessLayers;

namespace SV19T1081026.NewsPayper.Models
{
    public class CategorySearchOutput : PaginationSearchOutput
    {

        /// <summary>
        /// Danh sách loại tin được hiển thị trên trang
        /// </summary>
        public List<PostCategory> Data { get; set; }

        public List<PostCategory> GetData
        {
            get
            {
                return ContentService.ListCategories();
            }
        }
    }
}