using BCrypt.Net;
using SV19T1081026.DataLayers;
using SV19T1081026.DomainModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SV19T1081026.BusinessLayers
{
    /// <summary>
    /// 
    /// </summary>
    public static class UserAccountService
    {
        private static readonly IUserAccountDAL userAccountDB;
        /// <summary>
        /// 
        /// </summary>
        static UserAccountService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            userAccountDB = new DataLayers.SqlServer.UserAccountDAL(connectionString);
        }
        /// <summary>
        /// Xác thực tài khoản đăng nhập (nếu thành công trả về thông tin tài khoản, ngược lại trả về null)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserAccount Authorize(string userName, string password)
        {
            return userAccountDB.Get(userName, password);
        }
        /// <summary>
        /// Thêm tài khoản người dùng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long Add(UserAccount data)
        {
            if (userAccountDB.ExistsUserName(data.UserName, 0))
                return -1;
            return userAccountDB.Add(data);
        }

        public static long Update(UserAccount data)
        {
            if (userAccountDB.ExistsUserName(data.UserName, data.UserId))
                return -1;
            return userAccountDB.Update(data);
        }
        /// <summary>
        /// Lấy thông tin tài khoản theo userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static UserAccount GetUserAccount(long userId)
        {
            return userAccountDB.Get(userId);
        }

        public static object Authorize(string username, object p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin tài khoản theo userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static UserAccount GetUserAccount(string userName)
        {
            return userAccountDB.Get(userName);
        }
        /// <summary>
        /// Thay đổi mật khẩu: có kiểm tra mật khẩu cũ
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = userAccountDB.Get(userName, oldPassword);
            if (user == null)
                return false;
            return userAccountDB.ChangePassword(userName, newPassword);
        }
        /// <summary>
        /// Thay đổi thông tin tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool UpdateUserAccount(long userId, string firstName, string lastName, string email, string phone)
        {
            return userAccountDB.Update(userId, firstName, lastName, email, phone);
        }
        /// <summary>
        /// lấy danh sách tài khoản người dùng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<UserAccount> ListUserAccount(int page,
                                                        int pageSize,
                                                        string searchValue,
                                                        out int rowCount)
        {
            rowCount = userAccountDB.Count(searchValue);
            return userAccountDB.List(page, pageSize, searchValue).ToList();
        }

        public static bool Delete(long UserId)
        {
            if (userAccountDB.InUsed(UserId))
                return false;
            return userAccountDB.Delete(UserId);
        }

        public static bool ExistsUserName(string userName, long userId = 0)
        {
            if (userAccountDB.ExistsUserName(userName, userId))
                return true;
            else return false;
        }

    }
}