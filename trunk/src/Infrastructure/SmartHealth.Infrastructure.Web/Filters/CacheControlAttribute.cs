using System;
using System.Web;
using System.Web.Mvc;

namespace SmartHealth.Infrastructure.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class CacheControlAttribute : ActionFilterAttribute
    {
        private readonly HttpCacheability cacheability;

        public CacheControlAttribute(HttpCacheability cacheability)
        {
            this.cacheability = cacheability;
        }

        public HttpCacheability Cacheability
        {
            get { return cacheability; }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            cache.SetCacheability(cacheability);
        }
    }
}
