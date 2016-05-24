using System.Web.Mvc;

namespace CustomWarehouse.Areas.ModuleProduct
{
    public class ModuleProductAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ModuleProduct";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ModuleProduct_default",
                "ModuleProduct/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
