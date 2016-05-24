using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Service
{
    public class GroupUserDA
    {
        /// <summary>
        /// Lay danh sach cac nhom theo user dang nhap 
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <returns></returns>
        public DataSet GroupUserGetByUserID(decimal p_id_user)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_id_user;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GROUPUSER_GET_BYUSERID", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }


        public DataSet GroupUserGetNotInUserID(decimal p_id_user)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_id_user;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GROUPUSER_NOT_IN_USERID", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }


        public decimal GroupUserInsert(decimal p_id_user, decimal p_id_group, string p_createby, DateTime p_createdate)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[5];
                spParameter[i] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_user;

                i++;
                spParameter[i] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_group;

                i++;
                spParameter[i] = new SqlParameter("@p_createby", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_createby;

                i++;
                spParameter[i] = new SqlParameter("@p_createdate", SqlDbType.Date);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_createdate;

                i++;
                spParameter[i] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Output;
                spParameter[i].Value = -1;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GROUP_USER_INSERT", spParameter);

                return Convert.ToDecimal(spParameter[4].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        public decimal GroupUserDeleted(decimal p_id_user, decimal p_id_group)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[3];
                spParameter[i] = new SqlParameter("@p_id_user", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_user;

                i++;
                spParameter[i] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_id_group;

                i++;
                spParameter[i] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Output;
                spParameter[i].Value = -1;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GROUP_USER_DELETE", spParameter);

                return Convert.ToDecimal(spParameter[2].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        /// <summary>
        /// Lấy danh sach user thuộc nhóm 
        /// </summary>
        /// <param name="pIDGroup"></param>
        /// <returns></returns>
        public DataSet GetUserOfGroupByIDGroup(decimal p_id_group)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_id_group", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_id_group;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_LST_USER_OF_GROUP", spParameter);

            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

    }
}
