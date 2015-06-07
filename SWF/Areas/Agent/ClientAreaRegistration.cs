using System.Web.Mvc;

namespace SWF.Areas.Agent
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Agent";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Agent_default",
                url: "Agent/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "SWF.Areas.Agent.Controllers" }
            );
        }
    }
}
