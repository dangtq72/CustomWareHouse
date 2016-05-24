using CW_Business;
using CW_Info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomWarehouse
{
    public class DataMemory
    {

        public static Hashtable c_hs_Allcode = new Hashtable();                                     //luu AllcodeInfo, key: cdname|cdval
        public static List<Custom_Info> c_lst_Custom = new List<Custom_Info>();

        public static decimal c_is_Custom = 1;
        public static string c_str_admin = "ADMIN";
        public static DateTime c_date_now = DateTime.Now;

        public static Dictionary<decimal, Functions_Info> c_dic_FunctionVsd = new Dictionary<decimal, Functions_Info>();                               //danh sách function ứng với vsd
        public static Dictionary<string, Functions_Info> c_dic_FunctionVsd_Href = new Dictionary<string, Functions_Info>();                               //danh sách function ứng với vsd

        public static List<AllcodeInfo> AllCode_GetBy_CdNameCdType(string p_cdname, string p_cdtype)
        {
            try
            {
                if (c_hs_Allcode.ContainsKey(p_cdname + "|" + p_cdtype))
                {
                    List<AllcodeInfo> _lst = new List<AllcodeInfo>();

                    List<AllcodeInfo> _lst_tem = (List<AllcodeInfo>)c_hs_Allcode[p_cdname + "|" + p_cdtype];

                    foreach (AllcodeInfo item in _lst_tem)
                        _lst.Add(item);
                    return _lst;
                }
                else return new List<AllcodeInfo>();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<AllcodeInfo>();
            }
        }

        public static void Get_ListCustom()
        {
            try
            {
                Custom_BL _Custom_BL = new Custom_BL();
                DataMemory.c_lst_Custom = _Custom_BL.Custom_GetAll();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public static void Set_ListWareHouse_AuzByUser(decimal p_user_id)
        {
            try
            {
                User_WareHose_BL _User_Custom_BL = new User_WareHose_BL();
                List<User_WareHose_Info> _lst = _User_Custom_BL.User_WareHouse_GetByUser_Auz(p_user_id);
                Dictionary<decimal, string> _dic_custom_byUser = new Dictionary<decimal, string>();
                foreach (User_WareHose_Info item in _lst)
                    _dic_custom_byUser[item.WareHouse_Id] = item.WareHouse_Name;

                SessionData.SetDataSession(NaviCommon.SystemParaKey.c_dic_USER_WAREHOUSE + "_" + p_user_id.ToString(), _dic_custom_byUser);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public static void Set_ListWareHouse_ByUser(decimal p_user_id, decimal User_Type)
        {
            try
            {
                User_WareHose_BL _User_Custom_BL = new User_WareHose_BL();
                List<WareHouse_Info> _lst_W = _User_Custom_BL.WareHouse_GetByUser(p_user_id, User_Type);
                SessionData.SetDataSession(NaviCommon.SystemParaKey.c_dic_USER_OF_WAREHOUSE + "_" + p_user_id.ToString(), _lst_W);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public static List<WareHouse_Info> Get_ListWareHouse_ByUser(bool p_is_add_None = false)
        {
            try
            {
                List<WareHouse_Info> _lst_re = new List<WareHouse_Info>();
                User_Info currentUser = (User_Info)SessionData.CurrentUser;
                string _key = NaviCommon.SystemParaKey.c_dic_USER_OF_WAREHOUSE + "_" + currentUser.User_Id.ToString();

                if (SessionData.GetDataSession(_key) != null)
                {
                    List<WareHouse_Info> _lst = (List<WareHouse_Info>)SessionData.GetDataSession(_key);
                    if (p_is_add_None)
                    {
                        WareHouse_Info _WareHouse_Info = new WareHouse_Info();
                        _WareHouse_Info.WareHouse_Id = -1;
                        _WareHouse_Info.WareHouse_Name = "-- Chọn kho -- ";
                        _lst_re.Add(_WareHouse_Info);
                    }
                    foreach (WareHouse_Info item in _lst)
                    {
                        _lst_re.Add(item);
                    }
                }

                return _lst_re;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<WareHouse_Info>();
            }
        }


        /// <summary>
        /// Lấy danh sách ck phân quyền theo user (trong session)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<decimal, string> Get_ListWareHouse_Auz()
        {
            try
            {
                Dictionary<decimal, string> _dic_WareHouse_byUser = new Dictionary<decimal, string>();
                if (SessionData.CurrentUser == null)
                    return _dic_WareHouse_byUser;

                User_Info currentUser = (User_Info)SessionData.CurrentUser;
                string _key = NaviCommon.SystemParaKey.c_dic_USER_WAREHOUSE + "_" + currentUser.User_Id.ToString();

                if (SessionData.GetDataSession(_key) != null)
                    _dic_WareHouse_byUser = (Dictionary<decimal, string>)SessionData.GetDataSession(_key);

                return _dic_WareHouse_byUser;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Dictionary<decimal, string>();
            }
        }

        public static void Get_Function_ByUser()
        {
            try
            {
                c_dic_FunctionVsd = new Dictionary<decimal, Functions_Info>();
                c_dic_FunctionVsd_Href = new Dictionary<string, Functions_Info>();

                FunctionsBL _FunctionsBL = new FunctionsBL();
                c_is_Custom = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["Is_Custom"]);


                if (c_is_Custom == 0)
                    c_str_admin = "PORTAL";
                else
                    c_str_admin = "CUSTOM";

                List<Functions_Info> _lst = _FunctionsBL.FunctionsGetBy_Type(c_str_admin);
                foreach (Functions_Info item in _lst)
                {
                    c_dic_FunctionVsd[item.Func_Id] = item;
                    c_dic_FunctionVsd_Href[item.Href.ToUpper()] = item;
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public static List<Product_Info> Get_ListProduct_Service()
        {
            try
            {
                Get_DBMemoryBL _Get_DBMemoryBL = new Get_DBMemoryBL();
                List<Product_Info> _lst = _Get_DBMemoryBL.DBMem_GetDataRealTime_Object<List<Product_Info>>("c_lst_Product");
                return _lst;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Product_Info>();
            }
        }

        public static List<WareHouse_Info> Get_All_ListWareHouse_Service()
        {
            try
            {
                Get_DBMemoryBL _Get_DBMemoryBL = new Get_DBMemoryBL();
                List<WareHouse_Info> _lst = _Get_DBMemoryBL.DBMem_GetDataRealTime_Object<List<WareHouse_Info>>("c_lst_WareHouse");
                foreach (WareHouse_Info item in _lst)
                {
                    NaviCommon.Common.log.Error(item.WareHouse_Name);
                }
                return _lst;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<WareHouse_Info>();
            }
        }

        public static List<User_Info> Get_LisUserDoanhNghiep(bool p_isAdd_None = true)
        {
            try
            {
                Get_DBMemoryBL _Get_DBMemoryBL = new Get_DBMemoryBL();
                List<User_Info> _lst_re = new List<User_Info>();
                List<User_Info> _lst = _Get_DBMemoryBL.DBMem_GetDataRealTime_Object<List<User_Info>>("c_lst_UserDoanhNghiep");
                if (p_isAdd_None)
                {
                    User_Info _User_Info = new User_Info();
                    _User_Info.User_Id = -1;
                    _User_Info.User_Name = "-- Chọn Doanh Nghiệp -- ";
                    _lst_re.Add(_User_Info);
                }

                foreach (User_Info item in _lst)
                {
                    _lst_re.Add(item);
                }
                return _lst_re;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<User_Info>();
            }
        }

        /// <summary>
        /// Lấy ngày hiện tại của server
        /// </summary>
        public static DateTime Get_Current_Date()
        {
            try
            {
                c_date_now = DateTime.Now;
                return c_date_now;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                c_date_now = DateTime.Now;
                return c_date_now;
            }
        }
    }
}