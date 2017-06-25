using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EventManager.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var descriptor = filterContext.ActionDescriptor;
            string actionName = descriptor.ActionName;
            string controllerName = descriptor.ControllerDescriptor.ControllerName;

            if (Session != null && Session["UserID"] != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                if(!(actionName == "Login" && controllerName == "Users"))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        controller = "Users",
                        action = "Login"
                    }));
                }                
            }
        }
    }
}