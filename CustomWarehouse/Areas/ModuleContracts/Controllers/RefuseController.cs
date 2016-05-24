using Administrator.Helpers;
using CW_Business;
using CW_Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomWarehouse.Areas.ModuleContracts.Controllers
{
    public class RefuseController : Controller
    {
        //
        // GET: /ModuleContracts/Refuse/
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
                lst_data = _ObjBL.TableSearch("", DateTime.MinValue, Convert.ToInt16(NaviCommon.Enum_Contract_Status.Huy_Duyet), 
                    currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
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
                List<Contracts_Info> lst_data = new List<Contracts_Info>();
                ContractBL _ObjBL = new ContractBL();

                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;

                if (!string.IsNullOrEmpty(p_keysearch))
                {
                    string[] arr_keysearch = p_keysearch.Split('|');
                    if (arr_keysearch.Length == 2)
                    {
                        lst_data = _ObjBL.TableSearch(arr_keysearch[0],
                            string.IsNullOrEmpty(arr_keysearch[1]) ? DateTime.MinValue : Convert.ToDateTime(arr_keysearch[1]),
                            Convert.ToInt16(NaviCommon.Enum_Contract_Status.Huy_Duyet), currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
                            Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                            p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                    }
                }
                else// TRƯỜNG HỢP KO CÓ GÌ THÌ LẤY TẤT
                {
                    lst_data = _ObjBL.TableSearch("", DateTime.MinValue, Convert.ToInt16(NaviCommon.Enum_Contract_Status.Huy_Duyet), 
                        currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
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

        [HttpPost]
        public ActionResult DeleteContract(decimal P_ID)
        {
            try
            {
                ContractBL _ContractBL = new ContractBL();
                decimal _rel = _ContractBL.Contract_Delete(P_ID);
                return Json(new { success = _rel });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false });
            }
        }

        public ActionResult ContractDetails()
        {
            try
            {
                string _url = "/ModuleContracts/Refuse/List";
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                    return Redirect(_ok);

                decimal _id = 0;
                if (RouteData.Values["id"] != null)
                {
                    _id = Convert.ToDecimal(RouteData.Values["id"]);
                }
                if (RouteData.Values["id1"] != null)
                {
                    ViewBag.CurrTab = Convert.ToInt16(RouteData.Values["id1"]);
                }
                else
                {
                    ViewBag.CurrTab = 1;
                }

                ContractBL _ObjBL = new ContractBL();
                Contracts_Info _objInfo = _ObjBL.Contract_GetById(_id);
                Declaration_BL _Declaration_BL = new Declaration_BL();
                List<Declaration_Info> _lstDeclare = _Declaration_BL.Declaration_GetByContract(_id);
                ViewBag.LstDeclaration = _lstDeclare;
                return View(_objInfo);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }


    }
}
