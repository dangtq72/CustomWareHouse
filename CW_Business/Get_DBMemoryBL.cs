using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CW_Business
{
    public class Get_DBMemoryBL
    {
        public Hashtable DBMem_GetDataRealTime(string strHashName)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.DBMem_Get_HashInfo(strHashName);
                Hashtable _hash = new Hashtable();

                if (byteRecive != null)
                {
                    _hash = (Hashtable)Util.ByteArrayToObject(byteRecive);
                }
                return _hash;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Hashtable();
            }
        }

        /// <summary>
        /// Dangtq lấy object ở dưới service lên, chưa test :))
        /// </summary>
        /// <typeparam name="T">kiểu object cần lên</typeparam>
        /// <param name="p_name">tên cần lấy</param>
        public T DBMem_GetDataRealTime_Object<T>(string p_name)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.DBMem_Get_HashInfo(p_name);

                if (byteRecive != null)
                {
                    return (T)Util.ByteArrayToObject(byteRecive);
                }
                return default(T);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return default(T);
            }
        }
     }
}
