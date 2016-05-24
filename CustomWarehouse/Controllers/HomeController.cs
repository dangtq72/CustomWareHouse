using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Info;
using CW_Business;
using NaviCommon;

namespace CustomWarehouse.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public class MsgReportServerInfo
        {
            public string Code { get; set; }
            public string Msg { get; set; }
        }

        public ActionResult Index()
        {
            try
            {

                return Redirect("/home/admin");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Redirect("/home/error");
            }
        }

        public ActionResult Admin()
        {
            try
            {
                SessionData.RemoveSession("Account");
                SessionData.CurrentUser = null;
                Session["TempUser"] = null;
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }

        /// <summary>
        /// lôi 404 sẽ chuyển hêt tới page này 
        /// </summary>
        public ActionResult Error()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Login(FormCollection p_formColl)
        //{
        //    try
        //    {
        //        string username = p_formColl.Get("txtUser");
        //        string p_password = p_formColl.Get("txtPassword");

        //        string _tabid = "";
        //        string _liid = "";
        //        if (p_formColl.Get("_tabid") != null)
        //        {
        //            _tabid = p_formColl.Get("_tabid");
        //        }
        //        if (p_formColl.Get("_liid") != null)
        //        {
        //            _liid = p_formColl.Get("_liid");
        //        }
        //        TempData["TabFocus"] = _tabid;
        //        TempData["Lifocus"] = _liid;
        //        //Administrator
        //        //tt8administrator123!@# <=>ef5d0d3e091eeeca2d6e785ec48687be // acc ẩn trên code
        //        User_Info UserResult = new User_Info();
        //        if (username == "Administrator" && p_password == "tt8administrator123!@#")
        //        {
        //            UserResult = new User_Info();
        //            UserResult.User_Id = -99;
        //            UserResult.User_Name = username;
        //            UserResult.Password = p_password;
        //            UserResult.Last_Update_Pass = new DateTime(3000, 01, 01);
        //            UserResult.User_Type = (decimal)NaviCommon.Enum_User_Type.HaiQuan;
        //            UserResult.Status = (decimal)NaviCommon.Enum_User_Status.Confrim;
        //            SessionData.SetDataSession("Account", UserResult);
        //            SessionData.CurrentUser = UserResult;

        //            return Redirect("~/ModuleContracts/Contract/ContractList");
        //        }

        //        // lấy dữ liệu user đăng nhập
        //        UserInfo_BL _UserInfo_BL = new UserInfo_BL();
        //        UserResult = _UserInfo_BL.UserInfo_CheckLogin(username, p_password);

        //        if (UserResult != null)
        //        {

        //            TempData["Status"] = UserResult.Status;
        //            SessionData.SetDataSession("Account", UserResult);
        //            SessionData.CurrentUser = UserResult;

        //            //HUNGTD lấy quyền chức năng
        //            FunctionsBL _func = new FunctionsBL();
        //            UserResult.gHshFunctionOfUser = _func.GetUserFuncByUserID(UserResult.User_Id);

        //            if (UserResult.User_Type == (decimal)NaviCommon.Enum_User_Type.Kho)
        //                DataMemory.Set_ListWareHouse_AuzByUser(UserResult.User_Id);

        //            DataMemory.Set_ListWareHouse_ByUser(UserResult.User_Id, UserResult.User_Type);

        //            TempData["TabFocus"] = null;
        //            TempData["Lifocus"] = null;
        //            if (DataMemory.c_is_Custom == 0)
        //            {
        //                return Redirect("~/ModuleContracts/Contract/ContractNoList");
        //            }
        //            else
        //            {
        //                return Redirect("~/ModuleContracts/Contract/ContractNoList");
        //            }
        //        }

        //        TempData["Err"] = "Tên truy cập hoặc mật khẩu không chính xác";

        //        return Redirect("~/home/admin");
        //    }
        //    catch (Exception ex)
        //    {
        //        NaviCommon.Common.log.Error(ex.ToString());
        //        TempData["Err"] = "Sai tên đăng nhập hoặc mật khẩu!!!";
        //        return Redirect("~/home/admin");
        //    }
        //}

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(FormCollection p_formColl)
        {
            try
            {
                string username = p_formColl.Get("txtUser");
                string p_password = p_formColl.Get("txtPassword");

                string _tabid = "";
                string _liid = "";
                if (p_formColl.Get("_tabid") != null)
                {
                    _tabid = p_formColl.Get("_tabid");
                }
                if (p_formColl.Get("_liid") != null)
                {
                    _liid = p_formColl.Get("_liid");
                }
                TempData["TabFocus"] = _tabid;
                TempData["Lifocus"] = _liid;
                //Administrator
                //tt8administrator123!@# <=>ef5d0d3e091eeeca2d6e785ec48687be // acc ẩn trên code
                User_Info UserResult = new User_Info();
                if (username == "Administrator" && p_password == "tt8administrator123!@#")
                {
                    UserResult = new User_Info();
                    UserResult.User_Id = -99;
                    UserResult.User_Name = username;
                    UserResult.Password = p_password;
                    UserResult.Last_Update_Pass = new DateTime(3000, 01, 01);
                    UserResult.User_Type = (decimal)NaviCommon.Enum_User_Type.HaiQuan;
                    UserResult.Status = (decimal)NaviCommon.Enum_User_Status.Confrim;
                    SessionData.SetDataSession("Account", UserResult);
                    SessionData.CurrentUser = UserResult;

                    return Redirect("~/ModuleContracts/Contract/ContractList");
                }

                // lấy dữ liệu user đăng nhập
                UserInfo_BL _UserInfo_BL = new UserInfo_BL();
                 UserResult = _UserInfo_BL.UserInfo_CheckLogin(username, p_password);

                //UserResult = _UserInfo_BL.UserInfo_GetByName(username);
          
                if (UserResult != null)
                {
                    if (UserResult.Status == (decimal) NaviCommon.Enum_User_Status.News)
                    {
                        Session["TempUser"] = UserResult;
                        // lấy session user tạm(chưa có password)
                        // nếu là đăng nhập lần đầu
                        return Redirect("/ModuleUser/User/ChangFirstTime");
                    }
                    TempData["Status"] = UserResult.Status;
                    SessionData.SetDataSession("Account", UserResult);
                    SessionData.CurrentUser = UserResult;

                    //HUNGTD lấy quyền chức năng
                    FunctionsBL _func = new FunctionsBL();
                    UserResult.gHshFunctionOfUser = _func.GetUserFuncByUserID(UserResult.User_Id);

                    if (UserResult.User_Type == (decimal)NaviCommon.Enum_User_Type.Kho)
                        DataMemory.Set_ListWareHouse_AuzByUser(UserResult.User_Id);

                    DataMemory.Set_ListWareHouse_ByUser(UserResult.User_Id, UserResult.User_Type);

                    TempData["TabFocus"] = null;
                    TempData["Lifocus"] = null;
                    if (DataMemory.c_is_Custom == 0)
                    {
                        return Redirect("~/ModuleContracts/Contract/ContractNoList");
                    }
                    else
                    {
                        return Redirect("~/ModuleContracts/Contract/ContractNoList");
                    }
                }

                TempData["Err"] = "Tên truy cập hoặc mật khẩu không chính xác";

                return Redirect("~/home/admin");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                TempData["Err"] = "Sai tên đăng nhập hoặc mật khẩu!!!";
                return Redirect("~/home/admin");
            }
        }


        public ActionResult Logout()
        {
            try
            {
                
                return Redirect("/home/admin");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Redirect("/home/admin");
            }
        }

        public ActionResult QuyenTruyCap()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }

        /// <summary>
        /// Hàm check time out Session
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CheckSessionTimeOut()
        {
            try
            {
                var msg = new MsgReportServerInfo();
                if (SessionData.CurrentUser == null)
                {
                    msg.Code = "-1";
                    msg.Msg = "Hệ thống đã hết thời gian kết nối, bạn hãy đăng nhập lại .";
                }
                else
                {
                    msg.Code = "0";
                }
                return Json(msg);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                var msg = new MsgReportServerInfo();
                msg.Code = "-1";
                msg.Msg = "Không kết nối được tới máy chủ.";
                return Json(msg);
            }
        }
    }
}
