﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV19T1081026.BusinessLayers;

namespace SV19T1081026.AdminTool
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
                list.Add(new SelectListItem() { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
    }
}