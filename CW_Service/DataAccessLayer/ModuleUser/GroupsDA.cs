using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Service
{
    public class GroupsDA
    {
        public DataSet GroupInfoGetAll()
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[0];
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GROUP_GETALL", spParameter);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        /// <summary>
        /// INSERT NHÓM 
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns>-2 NHÓM ĐÃ TỒN TẠI, ID BẢN GHI INSERT THÀNH CÔNG</returns>
        public decimal GroupInfoInsert(string p_name, string p_note, string p_createby, DateTime p_createdate, decimal pIdUser)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[6];
                spParameter[i] = new SqlParameter("@p_iduser", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = pIdUser;

                i++;
                spParameter[i] = new SqlParameter("@p_name", SqlDbType.NVarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_name;

                i++;
                spParameter[i] = new SqlParameter("@p_note", SqlDbType.NVarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_note;

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

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GROUP_INSERT", spParameter);

                return Convert.ToDecimal(spParameter[5].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        /// <summary>
        /// UPDATE NHÓM
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns>-2 NẾU NHÓM CÓ RỒI DỰA THEO TÊN MAP , ID BẢN GHI CẬP NHẬT THÀNH CÔNG </returns>
        public decimal GroupInfoUpdate(decimal p_group_id, string @p_group_name, string p_note, string p_modifiedby, DateTime p_modifieddate)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[6];
                spParameter[i] = new SqlParameter("@p_group_id", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_group_id;

                i++;
                spParameter[i] = new SqlParameter("@p_group_name", SqlDbType.NVarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_group_name;

                i++;
                spParameter[i] = new SqlParameter("@p_note", SqlDbType.NVarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_note;

                i++;
                spParameter[i] = new SqlParameter("@p_modifiedby", SqlDbType.VarChar);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_modifiedby;

                i++;
                spParameter[i] = new SqlParameter("@p_modifieddate", SqlDbType.Date);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = p_modifieddate;

                i++;
                spParameter[i] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Output;
                spParameter[i].Value = -1;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GROUP_UPDATE", spParameter);

                return Convert.ToDecimal(spParameter[5].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        /// <summary>
        /// Xóa Nhóm CHức Năng 
        /// </summary>
        /// <param name="pId"></param>
        /// <returns> (>0)ID ban ghi đã xóa -2 đang có trong bảng group_user ko được phép xóa </returns>
        public decimal GroupInfoDelete(decimal pId)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[2];
                spParameter[i] = new SqlParameter("@pId", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Input;
                spParameter[i].Value = pId;

                i++;
                spParameter[i] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[i].Direction = ParameterDirection.Output;
                spParameter[i].Value = -1;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_GROUP_DELETE", spParameter);
                return Convert.ToDecimal(spParameter[1].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

    }
}
