using System.Web.Mvc;
using SV19T1081026.DomainModels;
using SV19T1081026.BusinessLayers;
using SV19T1081026.Lib;

namespace SV19T1081026.AdminTool.Controllers
{
    [Authorize(Roles = WebAccountRoles.ADMINISTRATOR)]
    public class UserAccountController : Controller
    {
        private const string USER_ACCOUNT_SEARCH = "UserAccountSearchInput";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            Models.PaginationSearchInput model = Session[USER_ACCOUNT_SEARCH] as Models.PaginationSearchInput;
            if (model == null)
            {
                model = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = WebConfig.DefaultPageSize,
                    SearchValue = ""
                };
            }
            return View(model);
            //int rowCount = 0;
            //int pageSize = 5;
            //int pageCount = 1;
            //var model = UserAccountService.ListUserAccount(page, pageSize, searchValue, out rowCount);
            //pageCount = rowCount / pageSize;
            //if (rowCount % pageSize > 0)
            //    pageCount++;

            //ViewBag.Page = page;
            //ViewBag.RowCount = rowCount;
            //ViewBag.PageCount = pageCount;
            //ViewBag.SearchValue = searchValue;
            //return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            if (input.Page <= 0)
            {
                input.Page = 1;
            }
            if (input.PageSize <= 0)
                input.PageSize = 3;
            int rowCount;
            var data = UserAccountService.ListUserAccount(input.Page, input.PageSize, input.SearchValue, out rowCount);
            Models.UserAccountOutput model = new Models.UserAccountOutput()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[USER_ACCOUNT_SEARCH] = input; //Lưu lại đầu vào tìm kiếm
            return View(model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Cập nhật tài khoản người dùng";
            int userAccountId = 0;
            bool result = int.TryParse(id, out userAccountId);
            if (!result)
                return RedirectToAction("Index");

            var model = UserAccountService.GetUserAccount(userAccountId);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveData(UserAccount model)
        {
            //TODO: Kiểm tra tính hợp lệ của dữ liệu
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
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "Số điện thoại không được để trống");
            if (string.IsNullOrWhiteSpace(model.Password))
                ModelState.AddModelError("Password", "Mật khẩu không được để trống");



            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.UserId > 0 ? "Cập nhật tài khoản người dùng" : "Bổ sung tài khoản người dùng";
                return View("Edit", model);
            }
            model.Password = CryptHelper.EncodeMD5(model.Password);
            if (model.UserId > 0)
            {
                long updateResult = UserAccountService.Update(model);
            }
            else
            {
                UserAccountService.Add(model);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung tài khoản người dùng";
            UserAccount model = new UserAccount()
            {
                UserId = 0,
                UserName = "",
                IsLocked = false
            };
            return View("Edit", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int userId = 0;
            bool result = int.TryParse(id, out userId);
            if (!result)
                return RedirectToAction("Index");

            if (Request.HttpMethod == "POST")
            {
                UserAccountService.Delete(userId);
                return RedirectToAction("Index");
            }
            else
            {
                var model = UserAccountService.GetUserAccount(userId);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }

        }
    }
}

