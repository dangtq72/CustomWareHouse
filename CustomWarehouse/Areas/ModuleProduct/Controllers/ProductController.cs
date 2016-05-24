using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Info;
using CW_Business;

namespace CustomWarehouse.Areas.ModuleProduct.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /ModuleProduct/Product/

        public ActionResult ProductsList()
        {
            try
            {
                string _url = Request.RawUrl;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                {
                    return Redirect(_ok);
                }
                int fromRow = 0;
                string htmlPaging = "";

                Product_BL _Product_BL = new Product_BL();
                List<Product_Info> _lst_data = _Product_BL.Product_GetAll();

                List<Product_Info> lstDataBreakPage = NaviCommon.CommonFuc.GetPaging<Product_Info>(_lst_data, 1, ref fromRow, ref htmlPaging, "Sản phẩm");
                ViewBag.Paging = htmlPaging;
                ViewBag.FromRow = fromRow;
                ViewBag.Obj = lstDataBreakPage;
                ViewBag.SumRecord = _lst_data.Count;

                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());

                ViewBag.FromRow = 0;
                return View(new List<Product_Info>());
            }
        }

        [HttpPost]
        public ActionResult SearchProduct(string p_name)
        {
            try
            {
                int fromRow = 0;
                string htmlPaging = "";

                Product_BL _Product_BL = new Product_BL();
                List<Product_Info> _lst_data = _Product_BL.Product_Search(p_name);

                List<Product_Info> lstDataBreakPage = NaviCommon.CommonFuc.GetPaging<Product_Info>(_lst_data, 1, ref fromRow, ref htmlPaging, "Sản phẩm");
                ViewBag.Paging = htmlPaging;
                ViewBag.FromRow = fromRow;
                ViewBag.Obj = lstDataBreakPage;
                ViewBag.SumRecord = _lst_data.Count;

                return PartialView("PartialViewTableslistProduct");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("PartialViewTableslistProduct");
            }
        }

        [HttpPost]
        public ActionResult ShowViewProduct(decimal p_id)
        {
            try
            {
                Product_BL _Product_BL = new Product_BL();

                Product_Info _Product_Info = _Product_BL.Product_GetById(p_id);
                ViewBag.obj = _Product_Info;
                return PartialView("~/Areas/ModuleProduct/Views/Product/P_View_Product.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("P_View_Product");
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(decimal p_product_id)
        {
            try
            {
                // Kiểm tra có phiên làm việc không
                var objUser = SessionData.CurrentUser as User_Info;
                if (objUser == null)
                {
                    // thoát đăng nhập
                    return Json(new { success = -1 });
                }

                Product_BL _Product_BL = new Product_BL();

                decimal _kq = _Product_BL.Product_Delete(p_product_id);
                return Json(new { success = _kq });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }

        [HttpPost]
        public ActionResult ShowInsertProduct()
        {
            try
            {
                Category_BL _Category_BL = new Category_BL();
                List<Categories_Info> _lst = _Category_BL.Category_GetAll();
                ViewBag.LstCategory = _lst;

                return PartialView("~/Areas/ModuleProduct/Views/Product/_Partial_Insert_Product.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        [HttpPost]
        public ActionResult Product_Insert(string p_product_code,string p_product_name,decimal p_category_id,string p_unit)
        {
            try
            {
                // Kiểm tra có phiên làm việc không
                var objUser = SessionData.CurrentUser as User_Info;
                if (objUser == null)
                {
                    // thoát đăng nhập
                    return Json(new { success = -1 });
                }

                Product_BL _Product_BL = new Product_BL();
                if (_Product_BL.Product_GetByCode(p_product_code) != null)
                    return Json(new { success = -2 });
                else
                {
                    bool _kq = _Product_BL.Product_Insert(p_product_code, p_product_name, p_category_id,p_unit);
                    if (_kq)
                        return Json(new { success = 0 });
                    else
                        return Json(new { success = -1 });
                }

            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }
    }
}
