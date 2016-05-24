using CW_Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomWarehouse
{
    public class SessionData
    {
        public static User_Info CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["Account"] == null)
                {
                    return null;
                }
                else
                {
                    return (User_Info)HttpContext.Current.Session["Account"];
                }
            }
            set
            {
                HttpContext.Current.Session["Account"] = value;
            }
        }

        /// <summary>
        /// Lấy dữ liệu từ session
        /// </summary>
        /// <param name="pKey">key của session </param>
        /// <param name="pObj">Values</param>
        /// <returns></returns>
        public static void SetDataSession(string pKey, object pObj)
        {
            try
            {
                HttpContext.Current.Session[pKey] = pObj;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public static void RemoveSession(string pKey)
        {
            try
            {
                HttpContext.Current.Session.Remove(pKey);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Trả về Object lấy từ session 
        /// </summary>
        /// <param name="pKey">Key của session </param>
        /// <returns>Object Data</returns>
        public static object GetDataSession(string pKey)
        {
            try
            {
                return HttpContext.Current.Session[pKey];
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

    }
}