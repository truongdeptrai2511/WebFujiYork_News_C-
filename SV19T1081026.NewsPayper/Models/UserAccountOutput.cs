using System.Collections.Generic;
using SV19T1081026.DomainModels;

namespace SV19T1081026.NewsPayper.Models
{
    /// <summary>
    /// Danh sách tài khoản người dùng được hiển thị trên trang
    /// </summary>
    public class UserAccountOutput : PaginationSearchOutput
    {
        public List<UserAccount> Data { get; set; }
    }
}