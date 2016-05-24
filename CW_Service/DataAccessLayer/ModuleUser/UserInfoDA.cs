using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CW_Service
{
    public class UserInfoDA
    {

        /// <summary>
        /// check đăng nhập hệ thống
        /// </summary>
        /// <param name="pUserName"> user name </param>
        /// <param name="pPassWord">password </param>
        /// <returns></returns>
        public decimal CheckUserLogin(string User_Name, string Password)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[3];
                spParameter[0] = new SqlParameter("@User_Name", SqlDbType.NVarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = User_Name;

                spParameter[1] = new SqlParameter("@User_Name", SqlDbType.NVarChar);
                spParameter[1].Direction = ParameterDirection.Input;
                spParameter[1].Value = User_Name;

                decimal _id = -1;
                spParameter[2] = new SqlParameter("@P_Return", SqlDbType.Decimal);
                spParameter[2].Direction = ParameterDirection.Output;
                spParameter[2].Value = _id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_check_login", spParameter);

                return Convert.ToDecimal(spParameter[2].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -2;
            }
        }

        public DataSet UserInfo_GetById(decimal User_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@User_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = User_Id;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_getby_id", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }


        public DataSet UserInfo_GetByType(decimal User_Type)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@User_Type", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = User_Type;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_getby_type", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet UserInfo_GetByName(string User_Name)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@User_Name", SqlDbType.VarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = User_Name;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_getby_name", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        /// <summary>
        /// Tìm kiếm user không có ngày
        /// </summary>
        /// <param name="p_user_name"></param>
        /// <param name="p_type"></param>
        /// <param name="p_status"></param>
        /// <param name="p_identity"></param>
        /// <param name="p_primary_code"></param>
        /// Debug: 	73769  - bổ sung tìm kiếm theo fullname
        /// <returns></returns>
        public DataSet UserInfo_Search(string name, string user_type, string status, string Custom_Id, string column,
            string type_sort, string start, string end, ref decimal P_Total)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[9];
                spParameter[i] = new SqlParameter("@name", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = name;

                i++;
                spParameter[i] = new SqlParameter("@user_type", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = user_type;

                i++;
                spParameter[i] = new SqlParameter("@status", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = status;

                i++;
                spParameter[i] = new SqlParameter("@Custom_Id", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = Custom_Id;

                i++;
                spParameter[i] = new SqlParameter("@column", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = column;

                i++;
                spParameter[i] = new SqlParameter("@type_sort", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = type_sort;

                i++;
                spParameter[i] = new SqlParameter("@start", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = start;

                i++;
                spParameter[i] = new SqlParameter("@end", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = end;

                i++;
                spParameter[i] = new SqlParameter("@P_Total", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Output;
                spParameter[i].Value = P_Total;

                DataSet _ds = SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_search", spParameter);

                P_Total = Convert.ToDecimal(spParameter[8].Value);

                return _ds;
            }
            catch (Exception ex)
            {

                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public decimal UserInfo_Insert(string user_name, string password, string fullname, decimal Custom_Id, decimal user_type, decimal status, string Phone,string Email)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[9];
                spParameter[i] = new SqlParameter("@user_name", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = user_name;

                i++;
                spParameter[i] = new SqlParameter("@password", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = password;

                i++;
                spParameter[i] = new SqlParameter("@fullname", SqlDbType.NVarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = fullname;

                i++;
                spParameter[i] = new SqlParameter("@Custom_Id", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = Custom_Id;

                i++;
                spParameter[i] = new SqlParameter("@user_type", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = user_type;

                i++;
                spParameter[i] = new SqlParameter("@status", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = status;

                i++;
                spParameter[i] = new SqlParameter("@Phone", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = Phone;

                i++;
                spParameter[i] = new SqlParameter("@Email", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = Email;

                i++;
                spParameter[i] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Output;
                spParameter[i].Value = -1;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_insert", spParameter);

                return Convert.ToDecimal(spParameter[8].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal UserInfo_SetPassword(decimal p_user_id, decimal p_status, string p_password)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[3];
                spParameter[i] = new SqlParameter("@p_user_id", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_user_id;

                i++;
                spParameter[i] = new SqlParameter("@p_status", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_status;

                i++;
                spParameter[i] = new SqlParameter("@p_password", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_password;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_setpassword", spParameter);

                return 1;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal UserInfo_SetStatus(int p_user_id, int p_status)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[2];
                spParameter[i] = new SqlParameter("@p_user_id", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_user_id;

                i++;
                spParameter[i] = new SqlParameter("@p_status", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_status;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_setstatus", spParameter);

                return 1;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal UserInfo_Delete(int p_user_id)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[i] = new SqlParameter("@p_user_id", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_user_id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_delete", spParameter);

                return 1;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal UserInfo_Update_Pass(decimal p_user_id, string p_pass, DateTime p_last_pass)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[3];
                spParameter[i] = new SqlParameter("@p_user_id", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_user_id;

                i++;
                spParameter[i] = new SqlParameter("@p_pass", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_pass;

                i++;
                spParameter[i] = new SqlParameter("@p_last_pass", SqlDbType.Date);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_last_pass;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_user_update_pass", spParameter);
                //return Convert.ToInt32(result.Value.ToString());

                return 1;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

    }
}
