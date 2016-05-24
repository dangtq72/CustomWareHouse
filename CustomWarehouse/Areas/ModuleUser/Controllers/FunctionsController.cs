using BussinessFacade;
using NaviObjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V_Voting;
using V_Voting.Helpers;

namespace CustomWarehouse.Areas.ModuleUser.Controllers
{
    public class FunctionsController : Controller
    {
        /// <summary>
        /// Danh sách chức năng 
        /// </summary>
        /// <returns></returns>
        public ActionResult FunctionsList()
        {
            try
            {
                //KIỂM TRA QUYỀN TRUY CẬP CHỈ ĐẢY VÀO CÁC HÀM GET KO ĐẨY VÀO HAM POST
                string _url = "/ModuleUser/Functions/FunctionsList";
                string _ip = Request.UserHostAddress;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url, _ip);
                if (_ok != "")
                    return Redirect(_ok);

                ViewBag.tabFocus = 2;
                FunctionsBL _func = new FunctionsBL();
                List<FunctionsInfo> lstFunction = _func.FunctionsGetAll();
                string pagign = "";
                List<FunctionsInfo> lstCurrent = HtmlControllHelpers.PagingData<FunctionsInfo>(lstFunction, 1, NaviCommon.Common.RecordOnpage, ref pagign);
                for (int i = 0; i < lstCurrent.Count; i++)
                {
                    lstCurrent[i].No = i + 1;
                }
                Session["lstFunctions"] = lstFunction;
                ViewBag.Paging = pagign;

                return View(lstCurrent);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }



        public ActionResult FunctionsAddNew()
        {
            try
            {
                //KIỂM TRA QUYỀN TRUY CẬP CHỈ ĐẢY VÀO CÁC HÀM GET KO ĐẨY VÀO HAM POST
                string _url = "/ModuleUser/Functions/FunctionsAddNew";
                string _ip = Request.UserHostAddress;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url, _ip);
                if (_ok != "")
                    return Redirect(_ok);

                ViewBag.tabFocus = 1;
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
        public ActionResult FunctionsAddNew(FunctionsInfo pInfo)
        {
            try
            {
                pInfo.CreateBy = SessionData.CurrentUser.User_Name;
                ViewBag.tabFocus = 1;
                FunctionsBL _func = new FunctionsBL();
                int pretun = _func.FunctionsInsert(pInfo);

                if (pretun > 0)
                {
                    FunctionsBL func_bl = new FunctionsBL();
                    lock (NaviCommon.Common.LockListFunctWhenChangeData)
                    {
                        V_Voting.CommonData.gLstFunction = func_bl.FunctionsSearch_String("");
                    }
                }
                ViewBag.ErrorCode = pretun;
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }


        public ActionResult FunctionsEdit()
        {
            try
            {
                ViewBag.tabFocus = 2;
                //KIỂM TRA QUYỀN TRUY CẬP CHỈ ĐẢY VÀO CÁC HÀM GET KO ĐẨY VÀO HAM POST
                string _url = "/ModuleUser/Functions/FunctionsEdit";
                string _ip = Request.UserHostAddress;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url, _ip);
                if (_ok != "")
                    return Redirect(_ok);

                if (!RouteData.Values.ContainsKey("id"))
                {
                    return View(new FunctionsInfo());
                }

                int idLstFunc = Convert.ToInt32(RouteData.Values["id"].ToString());
                List<FunctionsInfo> lstFunction = new List<FunctionsInfo>();

                if (Session["lstFunctions"] == null)
                {
                    FunctionsBL _func = new FunctionsBL();
                    lstFunction = _func.FunctionsGetAll();
                    Session["lstFunctions"] = lstFunction;
                }
                else
                {
                    lstFunction = (List<FunctionsInfo>)Session["lstFunctions"];
                }
                foreach (var item in lstFunction)
                {
                    if (item.Id == idLstFunc)
                    {
                        return View(item);
                    }
                }

                return View(new FunctionsInfo());
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View(new FunctionsInfo());
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FunctionsEdit(FunctionsInfo pInfo)
        {
            try
            {
                ViewBag.tabFocus = 2;
                pInfo.ModifiedBy = SessionData.CurrentUser.User_Name;
                FunctionsBL _func = new FunctionsBL();
                int preturn = _func.FunctionsUpdate(pInfo);
                if (preturn > 0)
                {
                    FunctionsBL func_bl = new FunctionsBL();
                    lock (NaviCommon.Common.LockListFunctWhenChangeData)
                    {
                        V_Voting.CommonData.gLstFunction = func_bl.FunctionsSearch_String("");
                    }
                    return RedirectToAction("FunctionsList");
                }
                else
                {
                    ViewBag.ErrorCode = preturn;
                }
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
        public ActionResult FunctionsDelete(int pId)
        {
            try
            {
                ViewBag.tabFocus = 2;
                FunctionsBL _func = new FunctionsBL();
                int preturn = _func.FunctionsDelete(pId);
                if (preturn > 0)
                {
                    FunctionsBL func_bl = new FunctionsBL();
                    lock (NaviCommon.Common.LockListFunctWhenChangeData)
                    {
                        V_Voting.CommonData.gLstFunction = func_bl.FunctionsSearch_String("");
                    }
                }
                string purl = "/ModuleUser/Functions/FunctionsList";
                return Json(new { success = preturn, url = purl });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }

        /// <summary>
        /// HAM XU LY KHI NEXTTRANG 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NextPage(int pCurrentPage)
        {
            try
            {
                List<FunctionsInfo> lstFuction = (List<FunctionsInfo>)Session["lstFunctions"];
                List<FunctionsInfo> lstCurrent = new List<FunctionsInfo>();
                string pagign = "";
                lstCurrent = HtmlControllHelpers.PagingData<FunctionsInfo>(lstFuction, pCurrentPage, NaviCommon.Common.RecordOnpage, ref pagign);
                ViewBag.Paging = pagign;
                int _count = (pCurrentPage - 1) * NaviCommon.Common.RecordOnpage;
                for (int i = 0; i < lstCurrent.Count; i++)
                {
                    _count++;
                    lstCurrent[i].No = _count;
                }
                return PartialView("PartialTableListFunctions", lstCurrent);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -3 });
            }
        }

        /// <summary>
        /// Ham XU LY KHI NHAN NUT TIM KIEM 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchFunctionsList(FunctionsInfo pInfo)
        {
            try
            {
                List<FunctionsInfo> lstALLSearch = new List<FunctionsInfo>();
                FunctionsBL _func = new FunctionsBL();
                lstALLSearch = _func.FunctionsSearch(pInfo);

                string pagign = "";
                List<FunctionsInfo> lstCurrent = HtmlControllHelpers.PagingData<FunctionsInfo>(lstALLSearch, 1, NaviCommon.Common.RecordOnpage, ref pagign);
                for (int i = 0; i < lstCurrent.Count; i++)
                {
                    lstCurrent[i].No = i + 1;
                }

                Session["lstFunctions"] = lstALLSearch;
                ViewBag.Paging = pagign;

                return PartialView("PartialTableListFunctions", lstCurrent);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -3 });
            }
        }
    }
}
