using System;
using System.Web.Mvc;
using System.Web.Security;
using SV19T1081026.AdminTool;
using SV19T1081026.BusinessLayers;
using SV19T1081026.DomainModels;
using SV19T1081026.Lib;

namespace SV19T1081026.NewsPayper.Controllers
{
    public class HomeController : Controller
    {
        private const string POST_SEARCH = "PostSearchInput";
        public ActionResult Index()
        {


            Models.PostSearchInput model = Session[POST_SEARCH] as Models.PostSearchInput;
            if (model == null)
            {
                model = new Models.PostSearchInput()
                {
                    Page = 1,
                    PageSize = WebConfig.DefaultPageSize,
                    SearchValue = "",
                    CategoryId = 0
                };
            }
            

            return View(model);
        }
        public ActionResult Search(Models.PostSearchInput input, string id, string categoryUrlName = "", string searchValue = "")
        {
            int rowCount = 0;
            int categoryId = 0;
            if (categoryUrlName != "")
            {
                PostCategory category = ContentService.GetCategory(categoryUrlName);
                ViewBag.CategoryUrlName = category.CategoryUrlName;
                ViewBag.CategoryName = category.CategoryName;
                categoryId = category.CategoryId;
            }
            if (searchValue != "")
            {
                ViewBag.SearchValue = searchValue;
            }
            if (input.Page <= 0) input.Page = 1;
            if (input.PageSize <= 0) input.PageSize = WebConfig.DefaultPageSize;
            if (string.IsNullOrEmpty(input.SearchValue)) input.SearchValue = "";
            if (id != null && int.Parse(id) != 0)
                input.CategoryId = int.Parse(id);
            var data = ContentService.ListPosts(input.Page,
                                                input.PageSize,
                                                input.SearchValue,
                                                input.CategoryId,
                                                out rowCount);

            Models.PostSearchOutput model = new Models.PostSearchOutput()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                CategoryId = input.CategoryId,
                RowCount = rowCount,
                Data = data
            };
            Session[POST_SEARCH] = input;
            return View(model);
        }
        public ActionResult Login(string username = "", string password = "")
        {
            if (Request.HttpMethod == "POST")
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Nhập thông tin!");
                    return View();
                }

                var userAccount = UserAccountService.Authorize(username, CryptHelper.EncodeMD5(password));
                if (userAccount == null)
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                    return View();
                }

                WebUserData userData = new WebUserData()
                {
                    UserId = userAccount.UserId.ToString(),
                    UserName = userAccount.UserName,
                    FullName = $"{userAccount.LastName} {userAccount.FirstName}",
                    GroupName = userAccount.GroupName,
                    ClientIP = Request.UserHostAddress,
                    SessionId = Session.SessionID
                };

                FormsAuthentication.SetAuthCookie(userData.ToCookieString(), false);
                return base.Content("<script>location.reload();</script>");
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult ShowPost(Models.PostSearchInput input, string id)
        {
            var itemp = new SV19T1081026.DomainModels.Post();
            if (TempData["CommentSuccess"] != null)
                ViewBag.ChangePasswordSuccess = TempData["CommentSuccess"].ToString();
            int a;
            var listComments = ContentService.ListComments(1, 0, "",long.Parse(id), out a);
            ViewBag.CountComment = a;
            ViewBag.ListComments = listComments;
            if (input.Page <= 0) input.Page = 1;
            if (input.PageSize <= 0) input.PageSize = WebConfig.DefaultPageSize;
            if (string.IsNullOrEmpty(input.SearchValue)) input.SearchValue = "";
            if (id != null && int.Parse(id) != 0)
                input.PostId = int.Parse(id);
            var data = ContentService.GetPost(input.PostId);
            ViewBag.PostID = id;
            ViewBag.bcmt = itemp.AllowComment;
            Models.PostSearchOutput model = new Models.PostSearchOutput()
            {
                PostId = int.Parse(id),
                DataPost = data,
            };
            Session[POST_SEARCH] = input;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(PostComment model)
        {
            model.CreatedTime = DateTime.Now;
            model.IsAccepted = false;
            ContentService.AddComment(model);
            TempData["ChangePasswordSuccess"] = "Đã thay đổi mật khẩu thành công";
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Register(UserAccount model = null, string passConfirm = "")
        {
            if (Request.HttpMethod == "POST")
            {
                if (string.IsNullOrWhiteSpace(model.UserName))
                    ModelState.AddModelError("UserName", "Tên người dùng không được để trống");
                else if (UserAccountService.ExistsUserName(model.UserName, model.UserId))
                    ModelState.AddModelError("UserName", "Tên người dùng bị trùng");
                if (string.IsNullOrWhiteSpace(model.FirstName))
                    ModelState.AddModelError("FirstName", "Họ không được để trống");
                if (string.IsNullOrWhiteSpace(model.LastName))
                    ModelState.AddModelError("LastName", "Tên không được để trống");
                if (string.IsNullOrWhiteSpace(model.Email))
                    ModelState.AddModelError("Email", "Email không được để trống");
                else if (!StringUtils.IsEmailValid(model.Email))
                    ModelState.AddModelError("Email", "Email không hợp lệ");
                if (string.IsNullOrWhiteSpace(model.Phone))
                    ModelState.AddModelError("Phone", "Số điện thoại không được để trống");
                else if (!StringUtils.IsPhoneNumber(model.Phone))
                    ModelState.AddModelError("Phone", "Số điện thoại không hợp lệ");
                if (string.IsNullOrWhiteSpace(model.Password))
                    ModelState.AddModelError("Password", "Mật khẩu không được để trống");
                else if (!StringUtils.IsValidPassword(model.Password))
                    ModelState.AddModelError("Password", "Mật khẩu không hợp lệ");
                if (string.IsNullOrWhiteSpace(passConfirm))
                    ModelState.AddModelError("PassConfirm", "Nhập lại mật khẩu không được để trống");
                else if (!model.Password.Equals(passConfirm))
                    ModelState.AddModelError("PassConfirm", "Mật khẩu không trùng nhau");

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                model.Password = CryptHelper.EncodeMD5(model.Password);
                model.GroupName = "member";
                model.IsLocked = false;
                model.RegisteredTime = DateTime.Now;
                UserAccountService.Add(model);
                ViewBag.RegisterSuccess = true;
                UserAccount data1 = new UserAccount();
                return View(data1);
            }
            UserAccount data = new UserAccount();
            return View(data);
        }
        public ActionResult _MenuBar()
        {
            Models.PostCategoryOutput pco = new Models.PostCategoryOutput();
            return View(pco);
        }
    }

}