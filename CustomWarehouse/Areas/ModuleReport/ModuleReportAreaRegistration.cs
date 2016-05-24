using System.Web.Mvc;

namespace CustomWarehouse.Areas.ModuleReport
{
    public class ModuleReportAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ModuleReport";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ModuleReport_default",
                "ModuleReport/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
