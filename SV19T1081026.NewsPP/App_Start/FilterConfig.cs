﻿using System.Web;
using System.Web.Mvc;

namespace SV19T1081026.NewsPP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
