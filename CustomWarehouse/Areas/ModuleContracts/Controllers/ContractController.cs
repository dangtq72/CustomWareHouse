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
    public class ContractController : Controller
    {
        //
        // GET: /ModuleContracts/Contract/SaveAddData

        public ActionResult ContractList()
        {
            try
            {
                // test thử cái
                string _url = Request.RawUrl;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                {
                    return Redirect(_ok);
                }
                User_Info currentUser = (User_Info)SessionData.CurrentUser;

                int p_start = Common.cNumberRecordOnPage * (1 - 1) + 1;
                int p_end = Common.cNumberRecordOnPage * 1;

                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;

                int p_totalrecord = 0;
                List<Contracts_Info> lst_data = new List<Contracts_Info>();
                ContractBL _ObjBL = new ContractBL();
                lst_data = _ObjBL.TableSearch("", DateTime.MinValue, -1, currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
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

        public ActionResult ContractNoList()
        {
            try
            {
                var objUser = SessionData.CurrentUser as User_Info;
                if (objUser == null)
                    return Redirect("~/home/admin");

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

                string _name = "";
                DateTime _Register_Date = DateTime.MinValue;
                int _Status = -1;

                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;

                int p_totalrecord = 0;
                List<Contracts_Info> lst_data = new List<Contracts_Info>();
                ContractBL _ObjBL = new ContractBL();
                if (!string.IsNullOrEmpty(p_keysearch))
                {
                    string[] arr_keysearch = p_keysearch.Split('|');
                    if (arr_keysearch.Length == 7)
                    {
                        _name = arr_keysearch[0];
                        if (arr_keysearch[1] != "")
                            _Register_Date = Convert.ToDateTime(arr_keysearch[1]);
                        _Status = Convert.ToInt16(arr_keysearch[2]);

                        Receive_Number = string.IsNullOrEmpty(arr_keysearch[3]) ? -1 : Convert.ToDecimal(arr_keysearch[3]);  
                        Receive_Year = string.IsNullOrEmpty(arr_keysearch[4]) ? -1 : Convert.ToDecimal(arr_keysearch[4]);
                        WareHouse_Id = Convert.ToDecimal(arr_keysearch[5]);
                        Business_Id = Convert.ToDecimal(arr_keysearch[6]);

                        lst_data = _ObjBL.TableSearch(_name, _Register_Date, _Status,
                            currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
                            Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                            p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                    }
                }
                else// TRƯỜNG HỢP KO CÓ GÌ THÌ LẤY TẤT
                {
                    lst_data = _ObjBL.TableSearch(_name, _Register_Date, _Status,
                        currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
                        Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                        p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                }
                ViewBag.Paging = HtmlControllHelpers.PagingData(p_currentpage, Common.cNumberRecordOnPage, p_totalrecord);
                ViewBag.RecordOnPage = Common.cNumberRecordOnPage;
                ViewBag.IsSearch = p_issearch;
                ViewBag.List = lst_data;
                return PartialView("PartialViewTableslistContract");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("PartialViewTableslistContract");
            }
        }


        public ActionResult Add()
        {
            try
            {
                string _url = Request.RawUrl;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                    return Redirect(_ok);

                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }

        [HttpPost]
        public ActionResult SaveAddData(Contracts_Info _ObjInfo)
        {
            try
            {
                ContractBL _ContractBL = new ContractBL();
                _ObjInfo.Created_By = SessionData.CurrentUser.User_Name;
                _ObjInfo.Created_Date = NaviCommon.CommonFuc.CurrentDate();
                _ObjInfo.Due_Date = _ObjInfo.Register_Date.AddDays((double)_ObjInfo.Period);
                bool _rel = _ContractBL.Contract_Insert(_ObjInfo);
                return Json(new { success = _rel });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false });
            }
        }

        public ActionResult LoadDataWarning()
        {
            try
            {
                User_Info currentUser = (User_Info)SessionData.CurrentUser;
                if (currentUser == null)
                    return Redirect("~/home/admin");

                ContractBL _ObjBL = new ContractBL();

                List<Contracts_Info> lst_data = new List<Contracts_Info>();
                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;
                int p_totalrecord = 0;

                lst_data = _ObjBL.TableSearch("", DateTime.MinValue, -1, currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    "CONTRACT_CODE ASC", 1, 0, ref p_totalrecord);

                decimal _count_ChoDuyet = 0;
                decimal _count_DaDuyet = 0;
                decimal _count_HuyDuyet = 0;

                foreach (Contracts_Info item in lst_data)
                {
                    if (item.Status == (decimal)NaviCommon.Enum_Contract_Status.ChoDuyet)
                    {
                        _count_ChoDuyet++;
                    }
                    else if (item.Status == (decimal)NaviCommon.Enum_Contract_Status.Da_Duyet)
                    {
                        _count_DaDuyet++;
                    }
                    else if (item.Status == (decimal)NaviCommon.Enum_Contract_Status.Huy_Duyet)
                    {
                        _count_HuyDuyet++;
                    }
                }

                p_totalrecord = 0;
                lst_data = _ObjBL.Contract_Search_ThanhKhoan("", DateTime.MinValue, -1, currentUser.User_Type, currentUser.User_Id.ToString(), currentUser.User_Name,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    "A.CONTRACT_CODE ASC", 1, 0, ref p_totalrecord);

                string str_result = _count_ChoDuyet + "|" + _count_HuyDuyet + "|" + lst_data.Count.ToString();

                return Json(new { value = str_result });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { value = "" });
            }
        }
    }
}
