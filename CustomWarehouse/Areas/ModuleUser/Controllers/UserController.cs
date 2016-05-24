using CW_Business;
using NaviCommon;
using CW_Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.IO;

namespace CustomWarehouse.Areas.ModuleUser.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 1: Thành công
        /// 0: ID không tồn tại hoặc bị xóa / create user: trùng tên
        /// -1: Lỗi / hết session
        /// -2: SĐT bị trùng
        /// -3: Email bị trùng
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            try
            {
                //string _url_PhanQuyenDL = "/ModuleUser/User/UserSymbolList";
                string _url = Request.RawUrl;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                {
                    return Redirect(_ok);
                }

                int p_start = 1;
                int p_end = NaviCommon.Common.RecordOnpage;
                decimal p_totalrecord = 0;

                UserInfo_BL _UserInfo_BL = new UserInfo_BL();

                List<User_Info> _lst = _UserInfo_BL.UserInfo_Search("-1", "-1", "-1", "-1", "user_name", "ASC", p_start.ToString(), p_end.ToString(), ref p_totalrecord);

                ViewBag.Paging = HtmlHelpers.PagingData(1, NaviCommon.Common.RecordOnpage, (int)p_totalrecord, "Tài khoản");

                ViewBag.SumRecord = p_totalrecord;
                ViewBag.FromRow = p_start;
                ViewBag.Obj = _lst;

                return View(_lst);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                ViewBag.FromRow = 0;
                return View(new List<User_Info>());
            }
        }

        [HttpPost]
        public ActionResult ShowViewUser(int p_id)
        {
            try
            {
                var objUser = SessionData.CurrentUser as User_Info;
                if (objUser == null)
                    return null;

                UserInfo_BL _UserInfo_BL = new UserInfo_BL();

                User_Info _UserInfo = _UserInfo_BL.UserInfo_GetById(p_id);

                return PartialView("~/Areas/ModuleUser/Views/User/PartialView_User_Information.cshtml", _UserInfo);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("PartialViewTableslistUser");
            }
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Search_User(string p_keysearch, int p_CurrentPage, string p_column, string p_type_sort)
        {
            try
            {
                string p_name = "";
                string p_type = "-1";
                string p_status = "-1";
                string p_Custom_Id = "-1";

                int p_start = NaviCommon.Common.RecordOnpage * (p_CurrentPage - 1) + 1;
                int p_end = NaviCommon.Common.RecordOnpage * p_CurrentPage;

                string[] arrKey = p_keysearch.Split('|');
                if (arrKey.Length > 0)
                {
                    p_name = arrKey[0];
                    p_type = arrKey[1];
                    p_status = arrKey[2];
                    p_Custom_Id = arrKey[3];
                }

                if (String.IsNullOrEmpty(p_name))
                    p_name = "-1";

                UserInfo_BL _UserInfo_BL = new UserInfo_BL();
                decimal p_totalrecord = 0;
                List<User_Info> _lst = _UserInfo_BL.UserInfo_Search(p_name, p_type, p_status, p_Custom_Id, p_column, p_type_sort, p_start.ToString(), p_end.ToString(), ref p_totalrecord);
                ViewBag.Paging = HtmlHelpers.PagingData(p_CurrentPage, NaviCommon.Common.RecordOnpage, (int)p_totalrecord, "Tài khoản");
                ViewBag.SumRecord = p_totalrecord;
                ViewBag.FromRow = p_start;
                ViewBag.Obj = _lst;

                return PartialView("PartialViewTableslistUser");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("PartialViewTableslistUser");
            }
        }

        [HttpPost]
        public ActionResult SetPassword_User(int p_user_id, string p_user_name, string p_password)
        {
            try
            {   // Kiểm tra có phiên làm việc không


                if (Session["TempUser"] == null)
                {
                    return Json(new { success = -10 }); // hết session
                }

                UserInfo_BL _UserInfo_BL = new UserInfo_BL();
                var _Details = _UserInfo_BL.UserInfo_GetById(p_user_id);
                if (_Details == null)
                {
                    return Json(new { success = 0 }); // id không tồn tài hoặc bị xóa
                }
                string p_password_encrypt = NaviCommon.CommonFuc.Encrypt(p_user_name.ToUpper() + p_password);
                if (p_password_encrypt == _Details.Password)
                {
                    return Json(new { success = -3 }); // trùng mật khẩu cũ
                }
                decimal result = _UserInfo_BL.UserInfo_SetPassword(p_user_id, (int)NaviCommon.Enum_User_Status.Confrim, p_password_encrypt);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -2 }); // lỗi 
            }
        }

        [HttpGet]
        public ActionResult Create_User()
        {
            try
            {
                string _url = Request.RawUrl;
                string _ip = Request.UserHostAddress;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url, _ip);
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

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create_User(string p_username, string p_password, string p_fullname, decimal p_type, decimal p_Custom_Id, string p_phone,string p_email)
        {
            try
            {
                // Kiểm tra có phiên làm việc không
                var objUser = SessionData.CurrentUser as User_Info;
                if (objUser == null)
                    return Redirect("~/Home/Voting");

                UserInfo_BL _UserInfo_BL = new UserInfo_BL();
                User_Info checkUserName = _UserInfo_BL.UserInfo_GetByName(p_username);
                if (checkUserName != null)
                {
                    return Json(new { success = 0 });
                }

                decimal _type = Convert.ToDecimal(p_type);

                string p_password_encrypt = NaviCommon.CommonFuc.Encrypt(p_username.ToUpper() + p_password);
                decimal result = _UserInfo_BL.UserInfo_Insert(p_username, p_password_encrypt, p_fullname, p_Custom_Id, _type, 0, p_phone, p_email);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -5 });
            }
        }

        public decimal Delete_User(int p_User_Id)
        {
            try
            {
                // Kiểm tra có phiên làm việc không
                var objUser = SessionData.CurrentUser as User_Info;

                if (objUser == null)
                {
                    return -1; // thoát đăng nhập
                }
                if (objUser.User_Id == p_User_Id)
                {
                    return -1;  // không thể xóa chính nó
                }
                UserInfo_BL _UserInfo_BL = new UserInfo_BL();
                var _Details = _UserInfo_BL.UserInfo_GetById(p_User_Id);
                if (_Details == null)
                {
                    return 0; // tài khoản đã bị xóa
                }
                return _UserInfo_BL.UserInfo_Delete(p_User_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        #region WareHouse Users

        public ActionResult User_WareHouse_List()
        {
            try
            {
                string _url = "/ModuleUser/User/UserList";
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                    return Redirect(_ok);

                decimal _user_id = 0;
                if (RouteData.Values["id"] != null)
                {
                    _user_id = Convert.ToDecimal(RouteData.Values["id"].ToString());
                }
                User_Info _userinfo = new User_Info();

                // lấy toàn bộ danh sách kho lên
                List<WareHouse_Info> _lstWareHouse = WareHouse_BL.WareHouse_GetAll();

                UserInfo_BL _UserInfo_BL = new UserInfo_BL();
                _userinfo = _UserInfo_BL.UserInfo_GetById(_user_id);

                // lấy các kho của user
                User_WareHose_BL _User_WareHose_BL = new User_WareHose_BL();
                List<WareHouse_Info> _lst_UsWareHouse = _User_WareHose_BL.WareHouse_GetByUser_Auz(_userinfo.User_Id);

                Hashtable _hs = new Hashtable();
                foreach (WareHouse_Info item in _lst_UsWareHouse)
                {
                    _hs[item.WareHouse_Id] = item;
                }

                // tạm thời để _KeySession = trống, sau này phải sinh ra để test trên các tab cùng trình duyệt
                string _KeySession = "";
                ViewBag.KeySessionOnTab = _KeySession;
                Session["SessionListPackage" + _KeySession] = _lstWareHouse;//Toàn bộ Kho
                Session["SessionListPackageException" + _KeySession] = _hs;
                ViewBag.UserName = _userinfo.User_Name;
                ViewBag.User_id = _userinfo.User_Id;
                ViewBag.List = _lstWareHouse;
                ViewBag.HsException = _hs;
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }

        [HttpPost]
        public ActionResult UserWareHouseInsert(decimal p_user_id, string p_list_warehouse)
        {
            try
            {
                decimal _rel = 1;
                User_WareHose_BL _objbl = new User_WareHose_BL();
                string[] _ListIdpackWH = p_list_warehouse.Split('|');
                List<User_WareHose_Info> _lstUserCustom = new List<User_WareHose_Info>();
                foreach (string _item in _ListIdpackWH)
                {
                    if (!string.IsNullOrEmpty(_item))
                    {
                        User_WareHose_Info _temp = new User_WareHose_Info();
                        _temp.User_Id = p_user_id;
                        _temp.WareHouse_Id = Convert.ToDecimal(_item);
                        _temp.Created_By = SessionData.CurrentUser.User_Name;
                        _temp.Created_Date = DateTime.Now;
                        _lstUserCustom.Add(_temp);
                    }
                }
                //xóa trước
                _rel = _objbl.WareHouse_DeleteByUser(p_user_id);
                if (_rel > 0)// xóa thnahf công mới insert
                {
                    _objbl.User_WareHouse_Insert_Batch(_lstUserCustom);
                    //string _KeySession = SessionData.InnitSession();
                    string _KeySession = "";// tạm thời để _KeySession = trống, sau này phải sinh ra để test trên các tab cùng trình duyệt
                    Session["SessionListPackage" + _KeySession] = null;
                    Session["SessionListPackageException" + _KeySession] = null;
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false });
            }
        }

        public ActionResult RemoveWareHouse(string p_WareHouse, string p_key_session)
        {
            try
            {
                List<WareHouse_Info> _List = new List<WareHouse_Info>();
                string[] arrCustom = p_WareHouse.Split('|');

                Hashtable _hsException = new Hashtable();
                if (Session["SessionListPackage" + p_key_session] != null)
                {
                    _List = (List<WareHouse_Info>)Session["SessionListPackage" + p_key_session];
                }
                if (Session["SessionListPackageException" + p_key_session] != null)
                {
                    _hsException = (Hashtable)Session["SessionListPackageException" + p_key_session];
                }
                foreach (string item in arrCustom)
                {
                    if (_hsException.ContainsKey(Convert.ToDecimal(item)))
                    {
                        _hsException.Remove(Convert.ToDecimal(item));
                    }
                }

                Session["SessionListPackage" + p_key_session] = _List;
                Session["SessionListPackageException" + p_key_session] = _hsException;
                ViewBag.List = _List;
                ViewBag.HsException = _hsException;
                return PartialView("_PartialWareHouseData");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        public ActionResult AddWareHouse(string p_WareHouse, string p_key_session, int is_All = 0)
        {
            try
            {
                if (is_All == 0)
                {
                    List<WareHouse_Info> _List = new List<WareHouse_Info>();
                    string[] arrSymbol = p_WareHouse.Split('|');

                    Hashtable _hsException = new Hashtable();
                    if (Session["SessionListPackage" + p_key_session] != null)
                    {
                        _List = (List<WareHouse_Info>)Session["SessionListPackage" + p_key_session];
                    }
                    if (Session["SessionListPackageException" + p_key_session] != null)
                    {
                        _hsException = (Hashtable)Session["SessionListPackageException" + p_key_session];
                    }
                    Dictionary<decimal, WareHouse_Info> listAll = new Dictionary<decimal, WareHouse_Info>();

                    foreach (WareHouse_Info item in _List)
                    {
                        listAll[item.WareHouse_Id] = item;
                    }

                    foreach (string item in arrSymbol)
                    {
                        if (listAll.ContainsKey(Convert.ToDecimal(item)))
                        {
                            if (!_hsException.ContainsKey(Convert.ToDecimal(item)))
                            {
                                _hsException[Convert.ToDecimal(item)] = listAll[Convert.ToDecimal(item)];
                            }
                        }
                    }

                    Session["SessionListPackage" + p_key_session] = _List;
                    Session["SessionListPackageException" + p_key_session] = _hsException;
                    ViewBag.List = _List;
                    ViewBag.HsException = _hsException;
                    return PartialView("_PartialWareHouseData");
                }
                else
                {
                    List<WareHouse_Info> _List = new List<WareHouse_Info>();
                    Hashtable _hsException = new Hashtable();
                    if (Session["SessionListPackage" + p_key_session] != null)
                    {
                        _List = (List<WareHouse_Info>)Session["SessionListPackage" + p_key_session];
                    }
                    foreach (WareHouse_Info _item in _List)
                    {
                        _hsException[_item.WareHouse_Id] = _item;
                    }
                    Session["SessionListPackage" + p_key_session] = _List;
                    Session["SessionListPackageException" + p_key_session] = _hsException;
                    ViewBag.List = _List;
                    ViewBag.HsException = _hsException;
                    return PartialView("_PartialWareHouseData");
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }
        #endregion


        public ActionResult ChangFirstTime()
        {
            try
            {
                if (Session["TempUser"] == null)
                {
                    return Redirect("/home/admin");
                }
                return View();
            }
            catch (Exception ex)
            {
                 NaviCommon.Common.log.Error(ex.ToString());
                 return View();
            }
        }

    }
}
