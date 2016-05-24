using CW_Business;
using CW_Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomWarehouse.Areas.ModuleUser.Controllers
{
    public class UserOfGroupsController : Controller
    {

        /// <summary>
        /// DANH SACH NHOM THUOC USER 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListUserOfGroups()
        {
            try
            {
                //KIỂM TRA QUYỀN TRUY CẬP CHỈ ĐẢY VÀO CÁC HÀM GET KO ĐẨY VÀO HAM POST
                string _url = "/ModuleUser/USER/USERLIST";
                string _ip = Request.UserHostAddress;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url, _ip);
                if (_ok != "")
                    return Redirect(_ok);

                //LAY TU URL HOAC LAY TU SESSION DEU DUOC 
                if (!RouteData.Values.ContainsKey("id"))
                {
                    return View(new List<GroupUser_Info>());
                }
                int idUser = Convert.ToInt32(RouteData.Values["id"]);
                GroupUserBL _groupuser = new GroupUserBL();
                //LAY RA TEN TAI KHOAN THEM VAO NHOM 

                UserInfo_BL _UserInfo_BL = new UserInfo_BL();
                User_Info _user = _UserInfo_BL.UserInfo_GetById(idUser);


                ViewBag.UserName = _user.User_Name;
                ViewBag.IDUserName = idUser;

                ViewBag.lstGroupUsers = _groupuser.GroupUserGetByUserID(idUser);
                ViewBag.lstGroupNotInUsers = _groupuser.GroupUserGetNotInUserID(idUser, (int)SessionData.CurrentUser.User_Id);
                
                //DAY VAO SESSION KEY THEO USERID TRANH TH 2 TAB USER KHACH NHAU NHAP NHANG 
                //QUYEN CUA NHAU 
                ViewBag.CurrentUser = 0;
                if (SessionData.CurrentUser.User_Id == idUser)
                {
                    ViewBag.CurrentUser = 1;
                }
                string keyGroupInUser = "GroupInUser" + idUser.ToString();
                string keyGroupNotInUser = "GroupNotInUser" + idUser.ToString();
                Session[keyGroupInUser] = ViewBag.lstGroupUsers;
                Session[keyGroupNotInUser] = ViewBag.lstGroupNotInUsers;
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View(new List<GroupUser_Info>());
            }
        }


        /// <summary>
        /// Ham THUC HIEN ADD USER VAO NHOM 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUserInGroup(int pIdGroup)
        {
            try
            {
                int isAdd = 0;
                GroupUserBL _groupUser = new GroupUserBL();
                GroupUser_Info info = new GroupUser_Info();
                if (!RouteData.Values.ContainsKey("id"))
                {
                    return View(new List<GroupUser_Info>());
                }
                int idUser = Convert.ToInt32(RouteData.Values["id"]);
                string keyGroupInUser = "GroupInUser" + idUser.ToString();
                string keyGroupNotInUser = "GroupNotInUser" + idUser.ToString();

                List<GroupUser_Info> lstGroupUser = (List<GroupUser_Info>)Session[keyGroupInUser];
                List<Groups_Info> lstGroup = (List<Groups_Info>)Session[keyGroupNotInUser];
                ViewBag.IDUserName = idUser;
                foreach (Groups_Info item in lstGroup)
                {
                    if (item.Group_Id == pIdGroup)
                    {
                        info.User_Id = idUser;
                        info.Group_Id = item.Group_Id;
                        info.Group_Name = item.Group_Name;
                        //add vao  list hien tai 
                        info.CreateBy = SessionData.CurrentUser.User_Name;
                        lstGroupUser.Add(info);
                        //xoa khoi danh sach cac nhom ko thuoc user
                        lstGroup.Remove(item);
                        isAdd = 1;
                        break;
                    }
                }

                Session[keyGroupNotInUser] = lstGroup;
                Session[keyGroupInUser] = lstGroupUser;

                if (isAdd == 1)
                {
                    decimal pretunr = _groupUser.GroupUserInsert(info);
                    if (pretunr > 0)
                    {
                        //them thanh cong thi load lai danh sach user thuoc group 
                        CommonData.GetlstUserOfGroupAll();
                        return PartialView("PartialTableAllGroups");
                    }
                    else
                    {
                        return Json(new { success = false, status = pretunr });
                    }
                }
                return PartialView("PartialTableAllGroups");

            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false, status = -3 });
            }
        }

        /// <summary>
        /// HAM THUC HIEN KHI REMOVE USER RA NGOAI NHOM 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveUserOutGroup(int pIdGroup, int pIdUser)
        {
            try
            {
                int isAdd = 0;
                GroupUserBL _groupUser = new GroupUserBL();
                if (!RouteData.Values.ContainsKey("id"))
                {
                    return View(new List<GroupUser_Info>());
                }
                string keyGroupInUser = "GroupInUser" + pIdUser.ToString();
                string keyGroupNotInUser = "GroupNotInUser" + pIdUser.ToString();
                List<GroupUser_Info> lstGroupUser = (List<GroupUser_Info>)Session[keyGroupInUser];
                List<Groups_Info> lstGroup = (List<Groups_Info>)Session[keyGroupNotInUser];
                Groups_Info info = new Groups_Info();
                foreach (GroupUser_Info item in lstGroupUser)
                {
                    if (item.User_Id == pIdUser && item.Group_Id == pIdGroup)
                    {
                        lstGroupUser.Remove(item);
                        isAdd = 1;
                        info.Group_Id = item.Group_Id;
                        info.Group_Name = item.Group_Name;
                        lstGroup.Add(info);
                        break;
                    }
                }
                Session[keyGroupInUser] = lstGroupUser;
                Session[keyGroupNotInUser] = lstGroup;
                ViewBag.IDUserName = pIdUser;
                if (isAdd == 1)
                {
                    decimal pretunr = _groupUser.GroupUserDeleted(pIdUser, pIdGroup);
                    if (pretunr > 0)
                    {
                        //them thanh cong thi load lai danh sach user thuoc group 
                        CommonData.GetlstUserOfGroupAll();
                        return PartialView("PartialTableAllGroups");
                    }
                    else
                    {
                        return Json(new { success = false, status = pretunr });
                    }
                }

                return PartialView("PartialTableAllGroups");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false, status = -3 });
            }
        }

    }
}
