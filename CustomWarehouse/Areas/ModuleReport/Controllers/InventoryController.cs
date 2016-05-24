using CW_Business;
using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomWarehouse.Areas.ModuleReport.Controllers
{
    public class InventoryController : Controller
    {
        //
        // GET: /ModuleReport/Inventory/

        public ActionResult Inventory()
        {
            try
            {
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

                return PartialView("P_Report_TableslistWareHouse");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("P_Report_TableslistWareHouse");
            }
        }

        [HttpPost]
        public ActionResult BaoCaoHangTonKho(decimal p_warehouse_id,string p_wareHouse_name)
        {
            try
            {
                Report_BL _Report_BL = new Report_BL();
                string c_err = "";
                int is_err = -1;

                FlexCel.Report.FlexCelReport flcReport = new FlexCel.Report.FlexCelReport();
                string _fileExcelName_Src = "/Content/FlexcelDesignFile/BaoCaoTonKho.xls";// tên mẫu 
                string _fileExcelName_Export = "/Content/FlexcelExportFile/Bao_Cao_Ton_Kho.xls"; // tên file đích 


                //DataSet _ds = _Report_BL.Get_TonKho(p_warehouse_id);
                List<TonKho_Info> _lst = _Report_BL.Get_TonKho(p_warehouse_id);
                foreach (TonKho_Info item in _lst)
                {
                    item.SoNgayTon = Math.Round( (decimal)DateTime.Now.Subtract(item.Register_Date.AddDays((double)item.Period)).TotalDays);
                     
                }

                DataSet _ds = NaviCommon.ConvertData.ConvertToDataSet<TonKho_Info>(_lst);
                //someDateTime.Subtract(otherDateTime), 
                if (_ds == null)
                    return Json(new { c_err = "Không có dữ liệu báo cáo", is_err = -1 });
                else
                {
                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        _ds.Tables[0].TableName = "Data";
                        // đẩy dataset vào temp
                        CommonFunc.SetValueExportByDataTable(ref flcReport, _ds);
                        CommonFunc.SetValueExportByString(ref flcReport,"TenKho", p_wareHouse_name);

                        is_err = CommonFunc.ExportExcel(flcReport, Server.MapPath(_fileExcelName_Src), Server.MapPath(_fileExcelName_Export), ref c_err);
                        if (is_err == 0)// ko có lỗi
                            c_err = _fileExcelName_Export;
                    }
                    else
                        c_err = "Không có dữ liệu để kết xuất!";
                }

                return Json(new { c_err = c_err, is_err = is_err });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }
    }
}
