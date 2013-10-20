using System.Web.Mvc;

namespace SmartHealth.Web.Areas.Mood
{
    public class MoodAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Mood";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Mood_default",
                "Mood/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
