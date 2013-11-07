using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SmartHealth.Core.ApplicationServices.Caching;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure;
using SmartHealth.Infrastructure.Caching;
using SmartHealth.Infrastructure.Domain.DataInterfaces;


namespace SmartHealth.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserCache currentUser;

        public BaseController()
        {
            if (System.Web.HttpContext.Current.User != null &&
                System.Web.HttpContext.Current.User.Identity != null &&
                System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var userRepository = DependencyResolver.Current.GetService<IRepository<User>>();

                int currentUserId = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);
                currentUser = CacheManager.Get<UserCache>(
                    CacheKeys.UserBasic(currentUserId),
                    () => UserCache.Create(userRepository.Get(currentUserId)),
                     CacheDuration.Long);

                ViewBag.CurrentUser = currentUser;
            }
        }

       
        #region Error Pages
        public ActionResult NotFound()
        {
            return RedirectToAction("Error404", "Errors");
        }

        public ActionResult GenericError(string message)
        {
            TempData["message"] = message;
            return RedirectToAction("Error400", "Errors");
        }
        #endregion

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is NotFoundException)
            {
                filterContext.Result = NotFound();
                filterContext.ExceptionHandled = true;
                return;
            }
            base.OnException(filterContext);
        }

        /// <summary>
        /// Renders the partial view to string.
        /// </summary>
        /// <returns></returns>
        protected string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        /// <summary>
        /// Renders the partial view to string.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        /// Renders the partial view to string.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        /// <summary>
        /// Renders the partial view to string.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected override void Initialize(RequestContext rc)
        {
            if (rc.RouteData.Values["lang"] != null &&
                !string.IsNullOrWhiteSpace(rc.RouteData.Values["lang"].ToString()))
            {
                // set the culture from the route data (url)
                var lang = rc.RouteData.Values["lang"].ToString();
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);
            }
            else
            {
                // load the culture info from the cookie
                var cookie = rc.HttpContext.Request.Cookies["SH.CurrentUICulture"];
                var langHeader = string.Empty;
                if (cookie != null)
                {
                    // set the culture by the cookie content
                    langHeader = cookie.Value;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                else
                {
                    // set the culture by the location if not speicified
                    langHeader = rc.HttpContext.Request.UserLanguages[0];
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                // set the lang value into route data
                rc.RouteData.Values["lang"] = langHeader;
            }

            // save the location into cookie
            HttpCookie _cookie = new HttpCookie("SH.CurrentUICulture", Thread.CurrentThread.CurrentUICulture.Name);
            _cookie.Expires = DateTime.Now.AddYears(1);
            rc.HttpContext.Response.SetCookie(_cookie);
            base.Initialize(rc);
        }
    }
}