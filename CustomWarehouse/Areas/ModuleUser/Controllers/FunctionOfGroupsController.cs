using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using CW_Business;
using CW_Info;

namespace CustomWarehouse.Areas.ModuleUser.Controllers
{
    public class FunctionOfGroupsController : Controller
    {
        //suwar ham FunctionInGroupsGetAll 10.28.08
        public ActionResult FunctionOfGroupList()
        {
            try
            {
                ViewBag.tabFocus = 2;
                GroupFunctionBL _funcg = new GroupFunctionBL();
                GroupsBL _groups = new GroupsBL();

                //KIỂM TRA QUYỀN TRUY CẬP VÀ SESSION
                string _ip = Request.UserHostAddress;
                string _url = "/ModuleUser/GroupsUser/GroupUserList";
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url, _ip);
                if (_ok != "")
                    return Redirect(_ok);

                if (!RouteData.Values.ContainsKey("id"))
                {
                    return View(new List<Functions_Info>());
                }
                int idGroup = Convert.ToInt32(RouteData.Values["id"]);

                CommonData.gLstGroups = _groups.GroupsGetAll();
                foreach (var item in CommonData.gLstGroups)
                {
                    if (item.Group_Id == idGroup)
                    {
                        ViewBag.GroupInfo = item;
                        break;
                    }
                }
                //Kieemr tra xem nếu là nhóm nó được phân thì chỉ có quyền xem kod dc sửa nếu nó tạo thì đc phép sửa
                ViewBag.Created = true;
                int CreateOwner = 1; //nếu là nhóm nó tạo thì hiển thị hết lên để hiệu chỉnh phan quyền 
                //còn ko phải thì chỉ lấy danh sách các nhóm thuộc nó đc phân thôi

                Hashtable _HSFuncInGroup = new Hashtable();
                _HSFuncInGroup = _funcg.FunctionInGroupsGetAll(idGroup);
                ViewBag.FuncInGroup = _HSFuncInGroup;
                SessionData.CurrentUser.User_Id = -99;
                //hungtd-12/11/2015:lấy ứng dụng
                ViewBag.FuncNotInGroup = _funcg.FunctionNotInGroupsByAppGetAll(idGroup, SessionData.CurrentUser.User_Id, CreateOwner);

                //end HUNGTD
                ViewBag.IDGroup = idGroup;
                string keyFuncInGroup = "FuncInGroup" + idGroup.ToString();
                string keyFuncNotInGroup = "FuncNotInGroup" + idGroup.ToString();

                Session[keyFuncNotInGroup] = ViewBag.FuncNotInGroup;
                Session[keyFuncInGroup] = ViewBag.FuncInGroup;

                return View();

            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View(new List<GroupFunction_Info>());
            }
        }

        [HttpPost]
        public ActionResult AddRemoveFunctionInGroups(string pStrFunction, int pIdGroups)
        {
            try
            {
                ViewBag.tabFocus = 2;
                string keyFuncInGroup = "FuncInGroup" + pIdGroups.ToString();
                string keyFuncNotInGroup = "FuncNotInGroup" + pIdGroups.ToString();
                GroupFunction_Info pinfo = new GroupFunction_Info();
                GroupFunctionBL _groupfun = new GroupFunctionBL();
                string[] arrFunction = new string[0];
                decimal preturn = 0;
                if (pStrFunction != null)
                {
                    pinfo.lstFunction = pStrFunction.Trim(',');
                    pinfo.Group_Id = pIdGroups;
                    pinfo.CreateBy = SessionData.CurrentUser.User_Name;
                    preturn = _groupfun.GroupFunctionInsert(pinfo);
                }

                if (preturn > 0)
                {
                    Session[keyFuncInGroup] = _groupfun.FunctionInGroupsGetAll(pIdGroups);
                    Session[keyFuncNotInGroup] = _groupfun.FunctionNotInGroupsGetAll(pIdGroups, SessionData.CurrentUser.User_Id);
                }
                ViewBag.IDGroup = pIdGroups;
                return Json(new { success = true, status = preturn });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false, status = -3 });
            }
        }

        /// <summary>
        /// Hiệu chỉnh danh Danh sách chức năng của user 
        /// </summary>
        /// <returns></returns>
        public ActionResult FunctionGroupOfUserList()
        {
            try
            {
                ViewBag.tabFocus = 2;
                GroupsBL _groups = new GroupsBL();
                GroupFunctionBL _funcg = new GroupFunctionBL();
                UserFunctionBL _userFunc = new UserFunctionBL();
                string _ip = Request.UserHostAddress;
                //string _url = "/ModuleUserRoles/FunctionOfGroups/FunctionOfGroupList";

                string _url = "/ModuleUser/GroupsUser/GroupUserList";
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url, _ip);
                if (_ok != "")
                    return Redirect(_ok);

                if (!RouteData.Values.ContainsKey("id"))
                {
                    return View(new List<Functions_Info>());
                }
                int idUserFun = 0; int idGroup = 0;
                if (RouteData.Values["id"] != null)
                {
                    string pidgroupiduser = RouteData.Values["id"].ToString();

                    if (pidgroupiduser.Contains('-'))
                    {
                        string[] arr = pidgroupiduser.Split('-');
                        if (arr.Length == 2)
                        {
                            idUserFun = Convert.ToInt32(arr[0]);
                            idGroup = Convert.ToInt32(arr[1]);

                        }
                        else
                        {
                            return View(new List<Functions_Info>());
                        }
                    }
                    else
                    {
                        return View(new List<Functions_Info>());
                    }

                }
                ViewBag.IDGroup = idGroup;
                ViewBag.CreateGroupOwner = true; //nếu nhóm chính người dùng tạo thì chuyển về true 
                if (SessionData.CurrentUser.User_Id < 0)
                {
                    ViewBag.CreateGroupOwner = false;
                }

                List<Groups_Info> lstGroup = _groups.GroupsGetAll();
                CommonData.gLstGroups = lstGroup;
                foreach (var item in lstGroup)
                {
                    if (item.Group_Id == idGroup)
                    {
                        //nếu là nhóm người khác tạo xong phân quyền thì Có tổng 46/58 quyền được chọn.
                        //hiển thị số đc chọn / tổng là = nhau để người đc phân ko biết rõ là các quyền đc phân thiếu
                        if (item.Created_By.ToUpper() == SessionData.CurrentUser.User_Name.ToUpper())
                        {
                            ViewBag.CreateGroupOwner = true;
                        }
                        else
                        {
                            ViewBag.CreateGroupOwner = false;
                        }
                        ViewBag.GroupInfoUser = item;
                        break;
                    }
                }
                ViewBag.IdUserFunc = idUserFun;
                //danh sach chuc nang thuoc user
                ViewBag.FunctionInUser = _userFunc.UserFunctionGetAll(idUserFun, idGroup);

                ViewBag.FunctionInUserMans = _userFunc.UserFunctionGetAll(SessionData.CurrentUser.User_Id, idGroup);
                //dang xem chinh no 
                ViewBag.CurrentUser = 0;
                if (idUserFun == SessionData.CurrentUser.User_Id)
                {
                    ViewBag.CurrentUser = 1;
                }
                //danh sach chuc nang thuoc nhom 10:28.08.2015
                ViewBag.FuncInGroup = _funcg.FunctionInGroupsGetAllToList(idGroup);

                ViewBag.IDGroupUser = idGroup.ToString() + idUserFun.ToString();
                string FunctionInUser = "FunctionInUser" + ViewBag.IDGroupUser.ToString();
                string FuncInGroup = "FuncInGroup" + ViewBag.IDGroupUser.ToString();
                Session[FunctionInUser] = ViewBag.FunctionInUser;
                Session[FuncInGroup] = ViewBag.FuncInGroup;
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }


        [HttpPost]
        public ActionResult AddFunctionGroupsUser(string pStrFunction, int pIdGroups, int pIdUser)
        {
            try
            {
                ViewBag.tabFocus = 2;
                string FunctionInUser = "FunctionInUser" + pIdGroups.ToString() + pIdUser.ToString();
                string FuncInGroup = "FuncInGroup" + pIdGroups.ToString() + pIdUser.ToString();
                GroupFunctionBL _funcg = new GroupFunctionBL();
                UserFunctionBL _userfunc = new UserFunctionBL();
                decimal preturn = 0;
                if (pStrFunction != null)
                {
                    // xoa di r insert tung thang vao
                    if (_userfunc.Delete_UserFunction_ByUser(pIdUser))
                    {
                        string[] _arr_func = pStrFunction.Split(',');
                        foreach (string item in _arr_func)
                        {
                            UserFunction_Info pinfo = new UserFunction_Info();

                            pinfo.lstFunction = pStrFunction.Trim(',');
                            pinfo.User_Id = pIdUser;
                            pinfo.Group_Id = pIdGroups;
                            pinfo.Created_By = SessionData.CurrentUser.User_Name;
                            preturn = _userfunc.UserFunctionInsert(pinfo);
                        }
                    }
                }

                if (preturn > 0)
                {
                    //danh sachs chuc nang thuoc nhom dc phan quen cho user 
                    Session[FunctionInUser] = _userfunc.UserFunctionGetAll(pIdUser, pIdGroups);

                    //danh sach chuc nang thuoc nhom
                    Session[FuncInGroup] = _funcg.FunctionInGroupsGetAllToList(pIdGroups);
                }
                ViewBag.IDGroup = pIdGroups;
                return Json(new { success = true, status = preturn });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false, status = -3 });
            }
        }


        public ActionResult FunctionOfGroupListByUser()
        {
            try
            {
                ViewBag.tabFocus = 2;
                GroupFunctionBL _funcg = new GroupFunctionBL();
                GroupsBL _groups = new GroupsBL();
                //KIỂM TRA QUYỀN TRUY CẬP CHỈ ĐẢY VÀO CÁC HÀM GET KO ĐẨY VÀO HAM POST
                string _ip = Request.UserHostAddress;
                string _url = "/ModuleUser/FunctionOfGroups/FunctionOfGroupList";
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url, _ip);
                if (_ok != "")
                    return Redirect(_ok);

                if (!RouteData.Values.ContainsKey("id"))
                {
                    return View(new List<Functions_Info>());
                }
                int idGroup = Convert.ToInt32(RouteData.Values["id"]);
                foreach (var item in CommonData.GetLstGroups())
                {
                    if (item.Group_Id == idGroup)
                    {
                        ViewBag.GroupInfo = item;
                        break;
                    }
                }
                //danh sach chuc nang thuoc nhom 10:28.08.2015
                ViewBag.LstFuncInGroup = _funcg.FunctionInGroupsGetAll(idGroup);
                ViewBag.IDGroup = idGroup;
                string keyFuncInGroup = "FuncInGroup" + idGroup.ToString();
                Session[keyFuncInGroup] = ViewBag.LstFuncInGroup;
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View(new List<GroupFunction_Info>());
            }
        }

    }
}
