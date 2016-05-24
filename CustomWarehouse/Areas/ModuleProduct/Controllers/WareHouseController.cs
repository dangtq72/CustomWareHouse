using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Info;
using CW_Business;

namespace CustomWarehouse.Areas.ModuleProduct.Controllers
{
    public class WareHouseController : Controller
    {
        //
        // GET: /ModuleProduct/Product/

        public ActionResult WareHousesList()
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

                List<WareHouse_Info> _lst_data = WareHouse_BL.WareHouse_GetAll();
                List<WareHouse_Info> lstDataBreakPage = NaviCommon.CommonFuc.GetPaging<WareHouse_Info>(_lst_data, 1, ref fromRow, ref htmlPaging, "Kho");
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
                return View(new List<WareHouse_Info>());
            }
        }

        [HttpPost]
        public ActionResult SearchWareHouse(string p_name)
        {
            try
            {
                int fromRow = 0;
                string htmlPaging = "";

                WareHouse_BL _WareHouse_BL = new WareHouse_BL();
                List<WareHouse_Info> _lst_data = _WareHouse_BL.WareHouse_Search(p_name);

                List<WareHouse_Info> lstDataBreakPage = NaviCommon.CommonFuc.GetPaging<WareHouse_Info>(_lst_data, 1, ref fromRow, ref htmlPaging, "Kho");
                ViewBag.Paging = htmlPaging;
                ViewBag.FromRow = fromRow;
                ViewBag.Obj = lstDataBreakPage;
                ViewBag.SumRecord = _lst_data.Count;

                return PartialView("PartialViewTableslistWareHouse");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("PartialViewTableslistWareHouse");
            }
        }

        [HttpPost]
        public ActionResult ShowViewWareHouse(decimal p_id)
        {
            try
            {
                WareHouse_BL _WareHouse_BL = new WareHouse_BL();
                WareHouse_Info _WareHouse_Info = _WareHouse_BL.WareHouse_GetById(p_id);
                ViewBag.obj = _WareHouse_Info;
                return PartialView("~/Areas/ModuleProduct/Views/WareHouse/P_View_WareHouse.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("P_View_WareHouse");
            }
        }

        [HttpPost]
        public ActionResult DeleteWareHouse(decimal p_WareHouse_id)
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

                WareHouse_BL _WareHouse_BL = new WareHouse_BL();

                decimal _kq = _WareHouse_BL.WareHouse_Delete(p_WareHouse_id);
                return Json(new { success = _kq });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }

        [HttpPost]
        public ActionResult ShowInsert_WareHouse()
        {
            try
            {
                return PartialView("~/Areas/ModuleProduct/Views/WareHouse/_Partial_Insert_WareHouse.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        [HttpPost]
        public ActionResult WareHouse_Insert(string p_WareHouse_code, string p_WareHouse_name, string p_Location)
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

                WareHouse_BL _WareHouse_BL = new WareHouse_BL();
                decimal _kq = _WareHouse_BL.WareHouse_Insert(p_WareHouse_code, p_WareHouse_name, p_Location);
                return Json(new { success = _kq });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }
    }
}
