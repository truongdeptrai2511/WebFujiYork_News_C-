using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV19T1081026.BusinessLayers;
using SV19T1081026.DomainModels;

namespace SV19T1081026.AdminTool.Controllers
{
    [Authorize(Roles = WebAccountRoles.MODERATOR)]
    public class CommentController : Controller
    {
        private const string COMMENT_SEARCH = "CommentSearchInput";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session[COMMENT_SEARCH] as Models.PaginationSearchInput;
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
            var data = ContentService.ListComments(input.Page, input.PageSize, input.SearchValue, 0, out rowCount);
            Models.CommentSearchOutput model = new Models.CommentSearchOutput()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[COMMENT_SEARCH] = input; //Lưu lại đầu vào tìm kiếm
            return View(model);
        }
        public ActionResult Delete(string id)
        {
            int commentId = 0;
            bool result = int.TryParse(id, out commentId);
            if (!result)
                return RedirectToAction("Index");
            ContentService.DeleteComment(commentId);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            int commentId = 0;
            bool result = int.TryParse(id, out commentId);
            if (!result)
                return RedirectToAction("Index");
            var model = ContentService.GetComment(commentId);
            return View(model);
        }

        public ActionResult ChangeState(string id)
        {
            int commentId = 0;
            bool result = int.TryParse(id, out commentId);
            if (!result)
                return RedirectToAction("Index");
            ContentService.ChangeState(commentId);
            return null;
        }
    }
}