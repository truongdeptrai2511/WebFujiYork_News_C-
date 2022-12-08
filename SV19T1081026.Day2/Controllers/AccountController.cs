using SV19T1081026.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SV19T1081026.DomainModels;
using SV19T1081026.Day2.Models;
using System.Security.Principal;
using SV19T1081026.Day2;

namespace SV19T1081026.AdminTool.Controllers
{
    [Authorize]
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        public ActionResult Login(string username ="", string password="")
        {
            if (Request.HttpMethod == "POST")
            { 
                if(string.IsNullOrEmpty(username)||string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Nhập thông tin!");
                    return View();
                }
                var userAccount = UserAccountService.Authorize(username, password);
                if(userAccount == null)
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                    return View();
                }

                 WebUserData userData = new WebUserData() { 
                    UserId = userAccount.UserId.ToString(),
                    UserName = userAccount.UserName,
                    FullName = $"{userAccount.FirstName}{userAccount.LastName}",
                    ClientIP = Request.UserHostAddress,
                    SessionId = Session.SessionID,
                    GroupName = userAccount.GroupName
                };
                

                FormsAuthentication.SetAuthCookie(userData.ToCookieString(), false);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Xử lý điều hướng nếu chưa đăng nhập hoặc không đúng quyền
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult NotAuthorize()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Login");
        }

        public ActionResult ChangePassword()
        {

            return View();
        }
        public ActionResult SavePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var data = WebUserData.FromCookie(User.Identity.Name);
            UserAccount model = UserAccountService.GetUserAccount(data.UserName);
            if (string.IsNullOrWhiteSpace(oldPassword))
                ModelState.AddModelError("oldPassword", "Mật khẩu cũ không được để trống");
            if (string.IsNullOrWhiteSpace(newPassword))
                ModelState.AddModelError("newPassword", "Mật khẩu mới không được để trống");
            if (string.IsNullOrWhiteSpace(confirmPassword))
                ModelState.AddModelError("confirmPassword", "Xác nhận mật khảu không được để trống");
            if (!newPassword.Equals(confirmPassword))
                ModelState.AddModelError("confirmPassword", "Xác nhận mật khẩu không trùng với mật khẩu mới");
            if (!ModelState.IsValid)
            {
                return View("ChangePassword");
            }
            if (UserAccountService.ChangePassword(model.UserName, oldPassword, newPassword))
            {
                TempData["ChangePasswordSuccess"] = "Đã thay đổi mật khẩu thành công";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("oldPassword", "Mật khẩu cũ không chính xác");
                return View("ChangePassword");
            }
        }
    }
}