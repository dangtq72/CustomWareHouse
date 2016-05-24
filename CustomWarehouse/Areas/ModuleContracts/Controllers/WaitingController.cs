using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Info;
using CW_Business;
using Administrator.Helpers;


namespace CustomWarehouse.Areas.ModuleContracts.Controllers
{
    public class WaitingController : Controller
    {
        public ActionResult List()
        {
            try
            {
                string _url = Request.RawUrl;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                {
                    return Redirect(_ok);
                }

                User_Info currentUser = (User_Info)SessionData.CurrentUser;

                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;

                int p_start = Common.cNumberRecordOnPage * (1 - 1) + 1;
                int p_end = Common.cNumberRecordOnPage * 1;
                int p_totalrecord = 0;
                List<Contracts_Info> lst_data = new List<Contracts_Info>();
                ContractBL _ObjBL = new ContractBL();
                lst_data = _ObjBL.TableSearch("", DateTime.MinValue, Convert.ToInt16(NaviCommon.Enum_Contract_Status.ChoDuyet), currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    "CONTRACT_CODE ASC", p_start, p_end, ref p_totalrecord);
                ViewBag.Paging = HtmlControllHelpers.PagingData(1, Common.cNumberRecordOnPage, p_totalrecord);
                ViewBag.RecordOnPage = Common.cNumberRecordOnPage;
                ViewBag.IsSearch = 0;
                ViewBag.List = lst_data;
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ListSearch_Datas(int p_issearch, string p_keysearch, string p_orderby, string p_ordertype, int p_currentpage)
        {
            try
            {
                User_Info currentUser = (User_Info)SessionData.CurrentUser;

                int p_start = Common.cNumberRecordOnPage * (p_currentpage - 1) + 1;
                int p_end = Common.cNumberRecordOnPage * p_currentpage;
                int p_totalrecord = 0;

                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;

                List<Contracts_Info> lst_data = new List<Contracts_Info>();
                ContractBL _ObjBL = new ContractBL();
                if (!string.IsNullOrEmpty(p_keysearch))
                {
                    string[] arr_keysearch = p_keysearch.Split('|');
                    if (arr_keysearch.Length == 2)
                    {
                        lst_data = _ObjBL.TableSearch(arr_keysearch[0],
                            string.IsNullOrEmpty(arr_keysearch[1])?DateTime.MinValue:Convert.ToDateTime(arr_keysearch[1]),
                            Convert.ToInt16(NaviCommon.Enum_Contract_Status.ChoDuyet), currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
                            Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                            p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                    }
                }
                else// TRƯỜNG HỢP KO CÓ GÌ THÌ LẤY TẤT
                {
                    lst_data = _ObjBL.TableSearch("", DateTime.MinValue, Convert.ToInt16(NaviCommon.Enum_Contract_Status.ChoDuyet), currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
                        Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                        p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                }
                ViewBag.Paging = HtmlControllHelpers.PagingData(p_currentpage, Common.cNumberRecordOnPage, p_totalrecord);
                ViewBag.RecordOnPage = Common.cNumberRecordOnPage;
                ViewBag.IsSearch = p_issearch;
                ViewBag.List = lst_data;
                return PartialView("ListDataTable");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("ListDataTable");
            }
        }

        public ActionResult Edit()
        {
            try
            {
                string _url = "/ModuleContracts/Refuse/List";
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                    return Redirect(_ok);
                
                decimal _id = 0;
                if(RouteData.Values["id"] != null)
                {
                    _id = Convert.ToDecimal(RouteData.Values["id"] );
                }
                if (RouteData.Values["id1"] != null)
                {
                    ViewBag.CurrTab = Convert.ToInt16(RouteData.Values["id1"]);
                }
                else
                {
                    ViewBag.CurrTab = 3;
                }

                ContractBL _ObjBL = new ContractBL();
                Contracts_Info _objInfo = _ObjBL.Contract_GetById(_id);
                return View(_objInfo);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }

        [HttpPost]
        public ActionResult SaveEditData(Contracts_Info _ObjInfo)
        {
            try
            {
                ContractBL _ContractBL = new ContractBL();
                _ObjInfo.Modified_By = SessionData.CurrentUser.User_Name;
                _ObjInfo.Modified_Date = NaviCommon.CommonFuc.CurrentDate();
                _ObjInfo.Due_Date = _ObjInfo.Register_Date.AddDays((double)_ObjInfo.Period);
                bool _rel = _ContractBL.Contract_Update(_ObjInfo);
                return Json(new { success = _rel });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult ChangeStatusContract(decimal P_ID, string P_REASON_REJECT, int P_STATUS)
        {
            try
            {
                ContractBL _ContractBL = new ContractBL();
                bool _rel = _ContractBL.Contract_Update_Status(P_ID, P_STATUS, P_REASON_REJECT, SessionData.CurrentUser.User_Name);
                return Json(new { success = _rel });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult ViewDetails(decimal p_id)
        {
            try
            {
                ContractBL _ObjBL = new ContractBL();
                Contracts_Info _objInfo = _ObjBL.Contract_GetById(p_id);
                //Declaration_BL _Declaration_BL = new Declaration_BL();

                //List<Declaration_Info> _lstDeclare = _Declaration_BL.Declaration_GetByContract(p_id);
                //ViewBag.LstDeclaration = _lstDeclare;

                return PartialView("ViewDetails", _objInfo);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }

        [HttpPost]
        public ActionResult ViewApprove(decimal p_id)
        {
            try
            {
                ContractBL _ObjBL = new ContractBL();
                Contracts_Info _objInfo = _ObjBL.Contract_GetById(p_id);
                //Declaration_BL _Declaration_BL = new Declaration_BL();

                //List<Declaration_Info> _lstDeclare = _Declaration_BL.Declaration_GetByContract(p_id);
                //ViewBag.LstDeclaration = _lstDeclare;
                ViewBag.IsApprove = 1;

                return PartialView("ViewDetails", _objInfo);

                
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }
    }
}
