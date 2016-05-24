using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Business;
using CW_Info;

namespace CustomWarehouse.Controllers
{
    public class SettingController : Controller
    {
        public ActionResult Setting()
        {
            try
            {
                if (SessionData.CurrentUser == null)
                {
                    return Redirect("~/Home/admin");
                }

                User_Info currentUser = (User_Info)SessionData.CurrentUser;
                ViewBag.user = currentUser;
                if (currentUser.User_Type == (decimal)NaviCommon.Enum_User_Type.Kho)
                {
                    int fromRow = 0;
                    string htmlPaging = "";

                    List<WareHouse_Info> _lst = WareHouse_BL.WareHouse_GetAll();
                    List<WareHouse_Info> _lstResult = new List<WareHouse_Info>();

                    Dictionary<decimal, string> _dic_WareHose_byUser = DataMemory.Get_ListWareHouse_Auz();
                    foreach (WareHouse_Info item in _lst)
                    {
                        if (_dic_WareHose_byUser.ContainsKey(item.WareHouse_Id))
                            _lstResult.Add(item);
                    }

                    SessionData.SetDataSession("List_WareHose_PhanQuyen", _lstResult);

                    List<WareHouse_Info> lstCustomBreakPage = NaviCommon.CommonFuc.GetPaging<WareHouse_Info>(_lstResult, 1, ref fromRow, ref htmlPaging);
                    ViewBag.lstWareHouse = lstCustomBreakPage;
                    ViewBag.Paging = htmlPaging;
                    ViewBag.FromNow = fromRow;
                }
                return View("~/Views/Setting/Setting.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View("~/Views/Setting/Setting.cshtml");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult WareHouseSearch(int pCurrentPage, string p_column, string p_type_sort) {
            try
            {
                int fromRow = 0;
                string htmlPaging = "";
                List<User_WareHose_Info> _lstResult = (List<User_WareHose_Info>)SessionData.GetDataSession("List_WareHose_PhanQuyen");
                var ketQua = from em in _lstResult orderby em.WareHouse_Name descending select em;
                if (p_type_sort == "DESC")
                {
                    ketQua = from em in _lstResult orderby em.WareHouse_Name descending select em;
                } else {
                    ketQua =from em in _lstResult orderby em.WareHouse_Name ascending select em;
                }

                List<User_WareHose_Info> lstVotingBreakPage = NaviCommon.CommonFuc.GetPaging<User_WareHose_Info>(ketQua.ToList(), pCurrentPage, ref fromRow, ref htmlPaging);
                ViewBag.lstWareHouse = lstVotingBreakPage;
                ViewBag.Paging = htmlPaging;
                ViewBag.FromNow = fromRow;
                return PartialView("~/Views/Setting/P_TablelistCustom_WareHouse.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("~/Views/Setting/P_TablelistCustom_WareHouse.cshtml");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPassword(string p_old_pass, string p_new_pass)
        {
            try
            {
                User_Info currentUser = (User_Info)SessionData.CurrentUser;
                string old_pass_in = NaviCommon.CommonFuc.Encrypt(currentUser.User_Name.ToUpper() + p_old_pass);
                if (old_pass_in != currentUser.Password)
                {
                    return Json(new { success = -1 });
                }
                UserInfo_BL _UserBl = new UserInfo_BL();
                string new_pass = NaviCommon.CommonFuc.Encrypt(currentUser.User_Name.ToUpper() + p_new_pass);
                decimal resultEdit = _UserBl.UserInfo_Update_Pass(currentUser.User_Id, new_pass,DateTime.Now.Date);
                if (resultEdit < 0)
                {
                    return Json(new { success = -2 });
                }
                currentUser.Last_Update_Pass = DateTime.Now;
                return Json(new { success = 1 });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -2 });
            }
        }
 
    }
}
