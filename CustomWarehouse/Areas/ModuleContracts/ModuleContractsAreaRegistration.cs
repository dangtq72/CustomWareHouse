using System.Web.Mvc;

namespace CustomWarehouse.Areas.ModuleContracts
{
    public class ModuleContractsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ModuleContracts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ModuleContracts_default",
                "ModuleContracts/{controller}/{action}/{id}/{id1}",
                new { action = "Index", id = UrlParameter.Optional, id1 = UrlParameter.Optional }
            );
        }
    }
}
