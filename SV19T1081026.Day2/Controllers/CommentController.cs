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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}