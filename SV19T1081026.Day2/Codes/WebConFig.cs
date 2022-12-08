using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SV19T1081026.AdminTool
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebConfig
    {
        /// <summary>
        /// Get value from AppSettings in web.config
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string AppSettings(string name)
        {
            try
            {
                return ConfigurationManager.AppSettings[name];
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// DefaultPageSize
        /// </summary>
        public static int DefaultPageSize => Convert.ToInt32(AppSettings("DefaultPageSize"));

    }
}