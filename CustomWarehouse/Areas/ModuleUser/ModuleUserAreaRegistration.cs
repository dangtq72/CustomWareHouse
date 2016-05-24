using System.Web.Mvc;

namespace CustomWarehouse.Areas.ModuleUser
{
    public class ModuleUserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ModuleUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ModuleUser_default",
                "ModuleUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
