using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Service
{
    public class FunctionsDA
    {

        /// <summary>
        /// DANH SACH CHỨC NĂNG LOAD HẾT LÊN LUÔN FULL CHẮC TẦM 1000 BẢN GHI 
        /// </summary>
        /// <returns></returns>
        public DataSet FunctionsGetAll()
        {
            try
            {
                try
                {
                    SqlParameter[] spParameter = new SqlParameter[0];
                    return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_FUNCTIONS_GETALL", spParameter);
                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                    return new DataSet();
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }
 

        /// <summary>
        /// LAY DANH SACH CAC CHUC NANG THEO USER DANG NHAP 
        /// LAY LEN KHI ĐANG NHẬP USER 
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <returns></returns>
        public DataSet GetUserFuncByUserID(decimal p_id_user)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_id_user;
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GET_FUNC_BY_USER", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet FunctionsGetBy_Type(string p_func_type)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_func_type", SqlDbType.NVarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_func_type;
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_FUNCTIONS_GetType", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

    }
}
