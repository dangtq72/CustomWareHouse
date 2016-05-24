using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Business;
using CW_Info;

namespace CustomWarehouse.Areas.ModuleDeclaration.Controllers
{
    public class ExportListController : Controller
    {
        //
        // GET: /ModuleDeclaration/ExportList/

        public ActionResult List()
        {
            try
            {
                string _url = Request.RawUrl;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                    return Redirect(_ok);

                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;

                int p_totalrecord = 0;
                int p_start = Common.cNumberRecordOnPage * (1 - 1) + 1;
                int p_end = Common.cNumberRecordOnPage * 1;
                List<Declaration_Info> lst_data = new List<Declaration_Info>();
                Declaration_BL _ObjBL = new Declaration_BL();
                lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name,
                    "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Xuat),
                  -1, "01/01/0001", -1, "01/01/0001", "",
                  "DECLARATION_CODE ASC",
                    p_start, p_end, ref p_totalrecord);
                ViewBag.Declaration = lst_data;
                List<Contracts_Info> lstContract = new List<Contracts_Info>();
                ContractBL _ContractjBL = new ContractBL();

                lstContract = _ContractjBL.TableSearch("", DateTime.MinValue, Convert.ToInt16(NaviCommon.Enum_Contract_Status.Da_Duyet),
                    SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Name,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    "CONTRACT_CODE ASC", 0, 0, ref p_totalrecord);
                ViewBag.ListContract = lstContract;

                p_totalrecord = 0;
                List<Declaration_Info> lst_Dec = new List<Declaration_Info>();
                lst_Dec = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                 (Int16)NaviCommon.Enum_Contract_Status.Da_Duyet,
                 "01/01/0001", -1, "01/01/0001", "", "DECLARATION_CODE ASC",
                    0, 0, ref p_totalrecord);
                ViewBag.DeclarationSearch = lst_Dec;
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ListSearchImport_Datas(int p_issearch, string p_keysearch, string p_orderby, string p_ordertype, int p_currentpage)
        {
            try
            {
                int p_start = Common.cNumberRecordOnPage * (p_currentpage - 1) + 1;
                int p_end = Common.cNumberRecordOnPage * p_currentpage;
                int p_totalrecord = 0;
                List<Declaration_Info> lst_data = new List<Declaration_Info>();
                Declaration_BL _ObjBL = new Declaration_BL();
                if (!string.IsNullOrEmpty(p_keysearch))
                {
                    string[] arr_keysearch = p_keysearch.Split('|');
                    if (arr_keysearch.Length == 10)
                    {
                        lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, arr_keysearch[0], arr_keysearch[1],
                            arr_keysearch[2], arr_keysearch[3], arr_keysearch[4], Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Xuat),
                             Convert.ToInt16(arr_keysearch[9]), arr_keysearch[5],
                            Convert.ToDecimal(arr_keysearch[6]), arr_keysearch[7], arr_keysearch[8],
                            p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                    }
                }
                else// TRƯỜNG HỢP KO CÓ GÌ THÌ LẤY TẤT
                {
                    lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Xuat),
                             Convert.ToInt16(NaviCommon.Enum_Contract_Status.ChoDuyet), "01/01/0001",
                            -1, "01/01/0001", "", p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                }
                ViewBag.Paging = HtmlControllHelpers.PagingData(p_currentpage, Common.cNumberRecordOnPage, p_totalrecord);
                ViewBag.RecordOnPage = Common.cNumberRecordOnPage;
                ViewBag.IsSearch = p_issearch;
                ViewBag.Declaration = lst_data;
                return PartialView("ListDataTable");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("ListImportDataTable");
            }
        }

        [HttpPost]
        public ActionResult ChangeStatus(decimal P_ID, string P_REASON_REJECT, int P_STATUS)
        {
            try
            {
                Declaration_BL _objBL = new Declaration_BL();
                bool _rel = _objBL.Declaration_Update_Status(P_ID, P_STATUS, P_REASON_REJECT, SessionData.CurrentUser.User_Name);
                return Json(new { success = _rel });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false });
            }
        }


    }
}
