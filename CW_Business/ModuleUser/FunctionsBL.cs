using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_Info;
using ZetaCompressionLibrary;

namespace CW_Business
{
    public class FunctionsBL
    {
        /// <summary>
        /// LAY TOAN BO DANH SACH CAC CHUC NANG LEN 
        /// </summary>
        /// <returns></returns>
        public List<Functions_Info> FunctionsGetAll()
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.FunctionsGetAll();
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Functions_Info> lstFunctions = NaviCommon.CBO<Functions_Info>.FillCollectionFromDataSet(_ds);
                return lstFunctions;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Functions_Info>();
            }
        }
     
        /// <summary>
        /// LAY DANH SACH CAC CHUC NANG THEO USER ĐĂNG NHẬP 
        /// HÀM NÀY GỌI KHI ĐĂNG NHẬP THÀNH CÔNG VÀO HỆ THỐNG THÌ LẤY LÊN 
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <returns></returns>
        public Dictionary<string, Functions_Info> GetUserFuncByUserID(decimal pIdUser)
        {
            try
            {
                Dictionary<string, Functions_Info> hshFunctionOfUser = new Dictionary<string, Functions_Info>();

                byte[] byteRecive = CommonData.c_serviceWCF.GetUserFuncByUserID(pIdUser);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Functions_Info> lstFunctions = NaviCommon.CBO<Functions_Info>.FillCollectionFromDataSet(_ds);
                foreach (Functions_Info info in lstFunctions)
                {
                    if (!string.IsNullOrEmpty(info.Href))
                    {
                        hshFunctionOfUser[info.Href] = info;
                    }
                }
                return hshFunctionOfUser;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Dictionary<string, Functions_Info>();
            }
        }

 
        /// <summary>
        /// Lấy danh sách function by function type
        /// </summary>
        public List<Functions_Info> FunctionsGetBy_Type(string p_func_type)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.FunctionsGetBy_Type(p_func_type);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Functions_Info> lstFunctions = NaviCommon.CBO<Functions_Info>.FillCollectionFromDataSet(_ds);
                return lstFunctions;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Functions_Info>();
            }
        }
    }
}
