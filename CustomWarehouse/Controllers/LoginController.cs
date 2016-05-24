using CW_Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomWarehouse.Controllers
{
    public class LoginController : Controller
    {
        public class MsgReportServerInfo
        {
            public string Code { get; set; }
            public string Msg { get; set; }
        }

        public ActionResult Login()
        {
            try
            {
                Session.RemoveAll();
                Session.Abandon();
                SessionData.CurrentUser = null;
                return View();

            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());

            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(FormCollection coleection)
        {
            int _Success = 1;
            User_Info _UserInfo = new User_Info();
           // UserInfo_BL _UserBL = new UserInfo_BL();
            if (ModelState.IsValid)
            {
                try
                {
                    // Không có nó lấy session thằng đăng nhập trước mà chưa timeout
                    SessionData.CurrentUser = null;
                    string strUrl = Request.Url.Query.ToString();
                    string user = Request.Form["_txtUserName"].ToString();
                    string pass = Request.Form["_txtPassword"].ToString();
                    _UserInfo.User_Name = user;
                    // pass = NaviCommon.CommonFuc.Encrypt(pass);
                    //Administrator
                    //tt8administrator123!@# <=>ef5d0d3e091eeeca2d6e785ec48687be
                    //User toàn quyền sử dụng không cần check trong database 
                    if (user == "Administrator" && pass == "tt8administrator123!@#")
                    {
                        //_UserInfo.USER_TYPE = Convert.ToDecimal(NaviCommon.Enum.UserType.Admin);
                        //_UserInfo.USER_NAME = "Administrator";
                        //_UserInfo.FULL_NAME = "Administrator";
                        //_UserInfo.PHONE = "00000";
                        //_UserInfo.EMAIL = "Administrator@gmail.com";
                        SessionData.CurrentUser = _UserInfo;

                        // ghi log đăng nhập
                        if (!string.IsNullOrEmpty(strUrl))
                        {
                            return RedirectToLocal(strUrl);
                        }
                        _Success = 1;
                        //  return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                    //    _UserInfo = _UserBL.GetUserLogin(user, pass);

                        if (_UserInfo != null) //dang nhap thanh cong
                        {
                            SessionData.CurrentUser = _UserInfo;
                            _Success = 1;
                            //  return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            _Success = -1;
                            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _Success = -1;
                    Common.log.Error(ex.ToString());
                    ModelState.AddModelError("", "Có lỗi xảy ra khi đăng nhập!");
                }
            }
            if (_Success == 1)
            {
                //if (_UserInfo.USER_TYPE == (decimal)NaviCommon.Enum.UserType.Admin)
                //{
                //    return Redirect("/Home/index");
                //}
                //else if (_UserInfo.USER_TYPE == (decimal)NaviCommon.Enum.UserType.ThiTruong)
                //{
                //    return Redirect("/ModuleUserView/UserView/ListDotVanChuyen");
                //}
                //else if (_UserInfo.USER_TYPE == (decimal)NaviCommon.Enum.UserType.KhachHang
                //    || _UserInfo.USER_TYPE == (decimal)NaviCommon.Enum.UserType.Marketing)
                //{
                //    return Redirect("/ModuleUserView/UserView/ListDotVanChuyen");
                //}
                //else
                //{
                //    return Redirect("/Home/index");
                //}
                return Redirect("/Home/index");
            }
            else
            {
                // lỗi thì return view
                return View();
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    if (returnUrl.Length > 4)
                    {
                        returnUrl = returnUrl.Substring(4, returnUrl.Length - 4);
                    }
                }
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return View();
            }
        }



        /// <summary>
        /// Sangdd 
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
