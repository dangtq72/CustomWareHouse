using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_Info;
using System.Data;
using System.Data.SqlClient;

namespace CW_Service
{
    public class UserFunctionDA
    {
        public decimal UserFunctionInsert(decimal p_id_user, decimal p_func_id, string p_lst_function, decimal p_id_group, string p_created_by)
        {
            try
            {

                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[6];
                spParameter[i] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_user;

                i++;
                spParameter[i] = new SqlParameter("@p_func_id", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_func_id;

                i++;
                spParameter[i] = new SqlParameter("@p_lst_function", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_lst_function;

                i++;
                spParameter[i] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_group;

                i++;
                spParameter[i] = new SqlParameter("@p_created_by", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_created_by;

                i++;
                spParameter[i] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Output;
                spParameter[i].Value = -1;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_USER_FUNCION_INSERT", spParameter);

                return Convert.ToDecimal(spParameter[5].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        public decimal UserFunctionDeleted(decimal p_id_user, decimal p_func_id, decimal p_id_group)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[4];
                spParameter[i] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_user;

                i++;
                spParameter[i] = new SqlParameter("@p_func_id", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_func_id;

                i++;
                spParameter[i] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_group;

                i++;
                spParameter[i] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Output;
                spParameter[i].Value = -1;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_USER_FUNCION_DELETE", spParameter);

                return Convert.ToDecimal(spParameter[3].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        public DataSet UserFunctionGetAll(decimal p_id_user, decimal p_id_group)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[2];
                spParameter[i] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_user;

                i++;
                spParameter[i] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_group;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_USER_GROUP_FUNCION_GETALL", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

    }
}
