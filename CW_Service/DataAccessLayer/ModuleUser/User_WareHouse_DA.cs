using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace CW_Service
{
    public class User_WareHouse_DA
    {
        public DataSet User_WareHouse_GetByUser(decimal p_user_id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_user_id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_user_id;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_User_WareHouse_GetByUser", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public bool User_WareHouse_Insert(decimal p_user_id, decimal p_warehouse_id, string p_created_by, DateTime p_created_date)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters);
                spParameter[0].Value = p_user_id;
                spParameter[1].Value = p_warehouse_id;
                spParameter[2].Value = p_created_by;
                spParameter[3].Value = p_created_date;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Insert_WareHouse_byUserId", spParameter);

                return true;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public decimal WareHouse_DeleteByUser(decimal p_user_id)
        {
            try
            {

                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_user_id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_user_id;

                SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_delete_WareHouse_by_user", spParameter);
                return 1;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return 0;
            }
        }
    }
}
