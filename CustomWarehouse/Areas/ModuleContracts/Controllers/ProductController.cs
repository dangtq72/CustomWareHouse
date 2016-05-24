using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Business;
using CW_Info;

namespace CustomWarehouse.Areas.ModuleContracts.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /ModuleContracts/Product/

        public ActionResult ProductList()
        {
            try
            {
                int fromRow = 0;
                string htmlPaging = "";

                Product_BL _BL = new Product_BL();
                List<Product_Info> _lst_Product = _BL.Product_GetAll();

                List<Product_Info> lstContractBreakPage = NaviCommon.CommonFuc.GetPaging<Product_Info>(_lst_Product, 1, ref fromRow, ref htmlPaging, "Sản phẩm");
                ViewBag.Paging = htmlPaging;
                ViewBag.FromRow = fromRow;
                ViewBag.Obj = lstContractBreakPage;
                ViewBag.SumRecord = _lst_Product.Count;

                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());

                ViewBag.FromRow = 0;
                return View(new List<Product_Info>());
            }
        }

        public ActionResult ShowViewProduct(decimal p_id)
        {
            try
            {
                Product_BL _BL = new Product_BL();

                Product_Info _Product_Info = _BL.Product_GetById(p_id);
                ViewBag.obj = _Product_Info;
                return PartialView("~/Areas/ModuleContracts/Views/Product/P_View_Product.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("PartialViewTableslistVoting");
            }
        }
    }
}
