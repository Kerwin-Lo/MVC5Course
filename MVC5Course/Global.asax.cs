﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC5Course
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //減少搜尋 View 檔案的時間 不找WebFormView(.aspx 與 .ascx)
            //只找RazorView (.cshtml 與 .vbhtml)
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

        }
    }
}
