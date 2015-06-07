using System.Web.Mvc;

namespace SWF.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Admin_default",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { action = "AsignUser", id = UrlParameter.Optional },
                namespaces: new [] { "SWF.Areas.Admin.Controllers" }
            ); 
        }
    }
}
