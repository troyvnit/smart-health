using System.Web;
using System.Web.Mvc;
using SmartHealth.Web.Helpers;

namespace SmartHealth.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           filters.Add(new HandleErrorAttribute());
           filters.Add(new AuthorizeAttribute());
           filters.Add(new EnableCompressionAttribute());
        }
    }
}