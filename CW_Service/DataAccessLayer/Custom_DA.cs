using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CW_Service
{
    public class Custom_DA
    {
        public DataSet Custom_GetAll()
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[0];
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Custom_GetAll", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public decimal Custom_Insert(string Custom_Name, string Location, string Note)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[4];
                spParameter[0] = new SqlParameter("@Custom_Name", SqlDbType.VarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Custom_Name;

                i++;
                spParameter[0] = new SqlParameter("@Location", SqlDbType.VarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Location;

                i++;
                spParameter[0] = new SqlParameter("@Note", SqlDbType.VarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Note;

                i++;
                decimal _id = -1;
                spParameter[1] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[1].Direction = ParameterDirection.Output;
                spParameter[1].Value = _id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Custom_Insert", spParameter);

                return Convert.ToDecimal(spParameter[3].Value);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal Custom_Delete(decimal Custom_Id)
        {
            try
            {
                int i = 0;
                SqlParameter[] spParameter = new SqlParameter[2];
                spParameter[0] = new SqlParameter("@Custom_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Custom_Id;

                i++;
                decimal _id = -1;
                spParameter[1] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[1].Direction = ParameterDirection.Output;
                spParameter[1].Value = _id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Custom_Delete", spParameter);

                return Convert.ToDecimal(spParameter[1].Value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
