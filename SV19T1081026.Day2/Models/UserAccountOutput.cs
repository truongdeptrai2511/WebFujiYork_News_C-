using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV19T1081026.DomainModels;

namespace SV19T1081026.AdminTool.Models
{
    /// <summary>
    /// Danh sách tài khoản người dùng được hiển thị trên trang
    /// </summary>
    public class UserAccountOutput : PaginationSearchOutput
    {
        public List<UserAccount> Data { get; set; }
    }
}