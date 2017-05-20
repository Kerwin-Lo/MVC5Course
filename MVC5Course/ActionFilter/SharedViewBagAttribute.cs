using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class SharedViewBagAttribute : ActionFilterAttribute
    {
        public string MyProperty { get; set; }

        private DateTime dt { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            dt = DateTime.Now;
            filterContext.Controller.ViewBag.Message = "Your application description page.";
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //base.OnActionExecuted(filterContext);
            filterContext.Controller.ViewBag.Message +="S:"+ dt.ToString("mm:ss")
                + " E:" + DateTime.Now.ToString("mm:ss");
        }
    }

}
 