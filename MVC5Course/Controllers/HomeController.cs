using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Unknown()
        {
            return View();
        }

        public ActionResult VT()
        {
            ViewBag.IsEnabled = false;
            return View();
        }

        public ActionResult RazorTest()
        {
            int[] data=new int[]{ 1, 2, 3, 4, 5 };
            return PartialView(data);
        }

        [SharedViewBag]
        public ActionResult About()
        {
            // ViewBag.Message = "Your application description page.";
            //Thread.Sleep(1500);
            throw new AggregateException("Error");
            return View();
        }

        public ActionResult PartialAbout()
        {
            ViewBag.Message = "Your application description page.";
            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            { 
                return View("About");
            }
        }

        public ActionResult SomeAction()
        {
            return PartialView("SuccessRedirect", "/");
        }

        public ActionResult GetFile()
        {
            return File(Server.MapPath("~/Content/wannaCry.png"), "image/png","Test.Png");
        }

        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return Json(db.Product.Take(5), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}