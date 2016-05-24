using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Service
{
    public class GroupFunctionDA
    {
        /// <summary>
        /// DANH SACH CAC CHUC NANG THUOC NHOM 
        /// </summary>
        /// <param name="pIdGroup"></param>
        /// <returns></returns>
        public DataSet FunctionInGroupsGetAll(decimal pIdGroup)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = pIdGroup;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_FUNCTION_IN_GROUP_GETALL", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }


        /// <summary>
        /// laays toan bo danh sach chuc nang ko dung vi thu tuc dang lay sai
        /// </summary>
        /// <returns></returns>
        public DataSet FunctionGroupsGetAll()
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[0];
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_FUNCTION_GROUP_GETALL", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }


        /// <summary>
        /// DANH SACH CAC CHUC NANG KHONG THUOC NHOM 
        /// </summary>
        /// <param name="pIdGroup"></param>
        /// <returns></returns>
        public DataSet FunctionNotInGroupsGetAll(decimal pIdGroup, decimal pIdUser, decimal pCreateOwner)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[3];
                spParameter[0] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = pIdGroup;

                spParameter[1] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[1].Direction = ParameterDirection.Input;
                spParameter[1].Value = pIdUser;

                spParameter[2] = new SqlParameter("@p_createowner", SqlDbType.Decimal);
                spParameter[2].Direction = ParameterDirection.Input;
                spParameter[2].Value = pCreateOwner;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_FUNC_NOT_IN_GROUP_GETALL", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        /// <summary>
        /// DANH SACH CAC CHUC NANG KHONG THUOC NHOM LỌC THEO MÃ ỨNG DỤNG
        /// </summary>
        /// <param name="pIdGroup"></param>
        /// <returns></returns>
        public DataSet FunctionNotInGroupsByAppGetAll(decimal pIdGroup, decimal pIdUser, decimal pCreateOwner)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[3];
                spParameter[0] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = pIdGroup;

                spParameter[1] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[1].Direction = ParameterDirection.Input;
                spParameter[1].Value = pIdUser;

                spParameter[2] = new SqlParameter("@p_createowner", SqlDbType.Decimal);
                spParameter[2].Direction = ParameterDirection.Input;
                spParameter[2].Value = pCreateOwner;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GETFUNC_NOT_IN_GR_BY_APP", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }


        public decimal GroupFunctionInsert(decimal p_id_group, string p_lst_function, string p_createby, DateTime p_createdate)
        {
            try
            {
                int i = 0;
                decimal _id = -1;

                SqlParameter[] spParameter = new SqlParameter[5];
                spParameter[i] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_group;

                i++;
                spParameter[i] = new SqlParameter("@p_lst_function", SqlDbType.NVarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_lst_function;

                i++;
                spParameter[i] = new SqlParameter("@p_created_by", SqlDbType.NVarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_createby;

                i++;
                spParameter[i] = new SqlParameter("@p_created_date", SqlDbType.Date);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_createdate;

                i++;
                spParameter[i] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Output;
                spParameter[i].Value = _id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GROUP_FUNCTION_INSERT", spParameter);

                return Convert.ToDecimal(spParameter[2].Value);
             
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }
    }
}
