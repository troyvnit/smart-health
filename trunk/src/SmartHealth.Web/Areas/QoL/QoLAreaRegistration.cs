using System.Web.Mvc;

namespace SmartHealth.Web.Areas.Box
{
    public class BoxAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Box";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Box_default",
                "Box/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
