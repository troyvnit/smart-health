using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SmartHealth.Infrastructure.Web.Bootstrap;
using SmartHealth.Infrastructure.Web.Nhibernate;
using SmartHealth.Web.Mappers;

namespace SmartHealth.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        private WebSessionStorage webSessionStorage;

        public override void Init()
        {
            base.Init();
            webSessionStorage = new WebSessionStorage(this);
        }       

        protected void Application_Start()
        {
            ComponentRegistrar.RegisterComponents("SmartHealth.Web");

            AreaRegistration.RegisterAllAreas();
            AutoMapperConfiguration.Configure(); 
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                var cultureInfo = (CultureInfo)this.Session["Culture"];
                if (cultureInfo == null)
                {
                    string langName = "vi";

                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);

                    cultureInfo = new CultureInfo(langName);
                    this.Session["Culture"] = cultureInfo;
                }

                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            }
        }
    }
}