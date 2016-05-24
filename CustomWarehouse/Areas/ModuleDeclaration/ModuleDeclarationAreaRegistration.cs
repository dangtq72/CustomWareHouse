using System.Web.Mvc;

namespace CustomWarehouse.Areas.ModuleDeclaration
{
    public class ModuleDeclarationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ModuleDeclaration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ModuleDeclaration_default",
                "ModuleDeclaration/{controller}/{action}/{id}/{id1}",
                new { action = "Index", id = UrlParameter.Optional, id1 = UrlParameter.Optional}
            );
        }
    }
}
