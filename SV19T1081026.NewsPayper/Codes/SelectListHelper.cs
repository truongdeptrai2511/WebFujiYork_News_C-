using System.Collections.Generic;
using System.Web.Mvc;
using SV19T1081026.BusinessLayers;
using SV19T1081026.DomainModels;

namespace SV19T1081026.NewsPayper
{
    /// <summary>
    /// Hỗ trợ các chức năng hiển thị dữ liệu dưới dạng SelectListItem
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Danh sách phân loại tin
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> PostCategories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in ContentService.ListCategories())
            {
                list.Add(new SelectListItem() { Value = item.CategoryUrlName.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        public static List<Post> Get3Post()
        {
            List<Post> list = new List<Post>();
            int a;
            foreach (var item in ContentService.ListPostsUser(1, 3, "", 0, out a))
            {
                list.Add(item);
            }
            return list;
        }
    }
}