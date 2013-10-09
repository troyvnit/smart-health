using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VinaSale.Infrastructure.Web.Bootstrap;
using VinaSale.Infrastructure.Web.Nhibernate;

namespace VinaSale.Web
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
            ComponentRegistrar.RegisterComponents("VinaSale.Web");

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}