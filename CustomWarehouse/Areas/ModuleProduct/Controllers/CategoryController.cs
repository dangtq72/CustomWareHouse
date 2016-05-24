using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Info;
using CW_Business;

namespace CustomWarehouse.Areas.ModuleProduct.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /ModuleProduct/Category/

        public ActionResult CategorysList()
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

                Category_BL _Category_BL = new Category_BL();
                List<Categories_Info> _lst_data = _Category_BL.Category_GetAll();

                List<Categories_Info> lstDataBreakPage = NaviCommon.CommonFuc.GetPaging<Categories_Info>(_lst_data, 1, ref fromRow, ref htmlPaging, "Loại sản phẩm");
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
                return View(new List<Categories_Info>());
            }
        }

        [HttpPost]
        public ActionResult ShowViewCategory(decimal p_id)
        {
            try
            {
                Category_BL _Category_BL = new Category_BL();
                Categories_Info _Categories_Info = _Category_BL.Category_GetById(p_id);
                ViewBag.obj = _Categories_Info;
                return PartialView("~/Areas/ModuleProduct/Views/Category/P_View_Category.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("P_View_Category");
            }
        }

        [HttpPost]
        public ActionResult DeleteCategory(decimal p_category_id)
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

                Category_BL _Category_BL = new Category_BL();

                decimal _kq = _Category_BL.Category_Delete(p_category_id);
                return Json(new { success = _kq });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }


        [HttpPost]
        public ActionResult SearchCategory(string p_name)
        {
            try
            {
                int fromRow = 0;
                string htmlPaging = "";

                Category_BL _Category_BL = new Category_BL();
                List<Categories_Info> _lst_data = _Category_BL.Category_Search(p_name);

                List<Categories_Info> lstDataBreakPage = NaviCommon.CommonFuc.GetPaging<Categories_Info>(_lst_data, 1, ref fromRow, ref htmlPaging, "Loại sản phẩm");
                ViewBag.Paging = htmlPaging;
                ViewBag.FromRow = fromRow;
                ViewBag.Obj = lstDataBreakPage;
                ViewBag.SumRecord = _lst_data.Count;
                
                return PartialView("PartialViewTableslistCategory");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("PartialViewTableslistCategory");
            }
        }

        [HttpPost]
        public ActionResult ShowInsertCategory()
        {
            try
            {
                return PartialView("~/Areas/ModuleProduct/Views/Category/_Partial_Insert_Category.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        [HttpPost]
        public ActionResult Category_Insert(string p_Category_code, string p_Category_name, string p_unit)
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

                Category_BL _Category_BL = new Category_BL();
                if (_Category_BL.Category_GetByCode(p_Category_code) != null)
                    return Json(new { success = -2 });
                else
                {
                    bool _kq = _Category_BL.Category_Insert(p_Category_code, p_Category_name, p_unit);
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
