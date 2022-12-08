
using System.Web.Mvc;

using SV19T1081026.AdminTool;
using SV19T1081026.BusinessLayers;

namespace News.Controllers
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
                    CategoryId = 0,     
                };
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(Models.PostSearchInput input)
        {
            if (input.Page <= 0) input.Page = 1;
            if (input.PageSize <= 0) input.PageSize = WebConfig.DefaultPageSize;
            if (string.IsNullOrEmpty(input.SearchValue)) input.SearchValue = "";

            int rowCount;
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
        public ActionResult _MenuBar()
        {
            News.Models.PostCategoryOutput pco = new News.Models.PostCategoryOutput();
            return View(pco);
        }
    }
}