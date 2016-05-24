using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Info;
using CW_Business;

namespace CustomWarehouse.Areas.ModuleUser.Controllers
{
    public class GroupsUserController : Controller
    {

        /// <summary>
        /// DANH SACH NHÓM QUYỀN
        /// </summary>
        /// <returns></returns>
        public ActionResult GroupUserList()
        {
            try
            {
                //string _url = "/VSD/USER/USERLIST";
                string _url = Request.RawUrl;
                string _ip = Request.UserHostAddress;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url, _ip);
                if (_ok != "")
                    return Redirect(_ok);

                ViewBag.tabFocus = 2;
                GroupsBL _group = new GroupsBL();
                CommonData.gLstGroups = _group.GroupsGetAll();
                CommonData.GetlstUserOfGroupAll();
                Session["lstGroups"] = CommonData.gLstGroups;
                return View(CommonData.gLstGroups);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View(new List<Groups_Info>());
            }
        }

        [HttpPost]
        public ActionResult SearchGroups(string p_name)
        {
            try
            {
                //ViewBag.tabFocus = 2;
                GroupsBL _group = new GroupsBL();
                if (p_name == "")
                {
                    return PartialView("PartialTableListGroups", CommonData.gLstGroups);
                }
                List<Groups_Info> _lst_result_search = new List<Groups_Info>();
                if (p_name != "")
                {
                    foreach (Groups_Info item in CommonData.gLstGroups)
                    {
                        if (item.Group_Name.ToLower().Contains(p_name.ToLower()))
                        {
                            _lst_result_search.Add(item);
                        }
                    }
                }

                return PartialView("PartialTableListGroups", _lst_result_search);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false, status = -3 });
            }
        }

        public ActionResult GroupUserAddNew()
        {
            try
            {
                string _url = Request.RawUrl;
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
        public ActionResult GroupUserAddNew(string p_name, string p_note)
        {
            try
            {
                GroupsBL _group = new GroupsBL();
                decimal pretun = _group.GroupsInsert(p_name, p_note, SessionData.CurrentUser.User_Name, DateTime.Now, SessionData.CurrentUser.User_Id);
                return Json(new { success = pretun });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }


        public ActionResult GroupUserEdit()
        {
            try
            {
                ViewBag.tabFocus = 2;
                if (SessionData.CurrentUser == null)
                {
                    return Redirect("~/Home/Voting");
                }
                if (!RouteData.Values.ContainsKey("id"))
                {
                    return View(new Groups_Info());
                }
                int idGroups = Convert.ToInt32(RouteData.Values["id"].ToString());
                List<Groups_Info> lstGroups = new List<Groups_Info>();
                if (Session["lstGroups"] == null)
                {
                    GroupsBL _group = new GroupsBL();
                    lstGroups = _group.GroupsGetAll();
                    Session["lstGroups"] = lstGroups;
                }
                else
                {
                    lstGroups = (List<Groups_Info>)Session["lstGroups"];
                }
                foreach (var item in lstGroups)
                {
                    if (item.Group_Id == idGroups)
                    {
                        return View(item);
                    }
                }
                return View(new Groups_Info());
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GroupUserEdit(int p_id, string p_name, string p_note)
        {
            try
            {
                //ViewBag.tabFocus = 2; 
                GroupsBL _group = new GroupsBL();
                decimal preturn = _group.GroupsUpdate(p_id, p_name, p_note, SessionData.CurrentUser.User_Name, DateTime.Now);
                return Json(new { success = preturn });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GroupsDelete(int pId)
        {
            try
            {
                ViewBag.tabFocus = 2;
                GroupsBL _groups = new GroupsBL();
                decimal preturn = _groups.GroupsDelete(pId);
                string purl = "/ModuleUser/GroupsUser/GroupUserList";
                return Json(new { success = preturn, url = purl });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }


        [HttpPost]
        public ActionResult GroupUserDetail(int pIDGroup)
        {
            try
            {

                Groups_Info pGroupInfo = new Groups_Info();
                if (pGroupInfo.lstUserOfGroupInfo != null)
                {
                    pGroupInfo.lstUserOfGroupInfo.Clear();
                }
                foreach (Groups_Info item in CommonData.GetLstGroups())
                {
                    if (item.Group_Id == pIDGroup)
                    {
                        item.lstUserOfGroupInfo.Clear();
                        pGroupInfo = item;
                        break;
                    }
                }


                //LUC LAY LEN TRONG THU TUC GAN ID_GOUP=ID ROI 
                GroupUserBL _groups = new GroupUserBL();
                pGroupInfo.lstUserOfGroupInfo = _groups.GetUserOfGroupByIDGroup(pIDGroup);
                ViewData["pInfo"] = pGroupInfo;
                return PartialView("PartialViewGroupUserDetail", ViewData["pInfo"]);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("PartialViewGroupUserDetail");
            }
        }


    }
}
