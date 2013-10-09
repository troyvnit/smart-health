using System.Web.Mvc;

namespace VinaSale.Web.Areas.SampleModule
{
    public class SampleModuleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SampleModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SampleModule_default",
                "SampleModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
