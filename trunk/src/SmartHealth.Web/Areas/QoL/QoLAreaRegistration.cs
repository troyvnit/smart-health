using System.Web.Mvc;

namespace VinaSale.Web.Areas.QoL
{
    public class QoLAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "QoL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "QoL_default",
                "QoL/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
