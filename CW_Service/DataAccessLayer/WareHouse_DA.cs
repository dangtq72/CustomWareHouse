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
    public class WareHouse_DA
    {
        public DataSet WAREHOUSE_GET_ALL()
        {
            try
            {
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_WAREHOUSE_GET_ALL");
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet WareHouse_Search(string p_name)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_name", SqlDbType.NVarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_name;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_WareHouse_Search", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet WareHouse_GetById(decimal WareHouse_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@WareHouse_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = WareHouse_Id;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_WareHouse_GetById", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet WareHouse_GetByUser(decimal User_Id,decimal User_Type)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[2];
                spParameter[0] = new SqlParameter("@User_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = User_Id;

                spParameter[1] = new SqlParameter("@User_Type", SqlDbType.Decimal);
                spParameter[1].Direction = ParameterDirection.Input;
                spParameter[1].Value = User_Type;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_WareHose_GetByUser", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        

        public decimal WareHouse_Delete(decimal WareHouse_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[2];
                spParameter[0] = new SqlParameter("@WareHouse_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = WareHouse_Id;

                decimal _id = -1;
                spParameter[1] = new SqlParameter("@P_RETURN", SqlDbType.Decimal);
                spParameter[1].Direction = ParameterDirection.Output;
                spParameter[1].Value = _id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_WareHouse_Delete", spParameter);

                return Convert.ToDecimal(spParameter[1].Value);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal WareHouse_Insert(string WareHouse_Code, string WareHouse_Name, string Location)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters, true);
                spParameter[0].Value = WareHouse_Code;
                spParameter[1].Value = WareHouse_Name;
                spParameter[2].Value = Location;

                spParameter[3] = new SqlParameter("@p_return", SqlDbType.Decimal);
                spParameter[3].Direction = ParameterDirection.Output;
                spParameter[3].Value = -1;


                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_WareHouse_Insert", spParameter);

                return Convert.ToDecimal(spParameter[3].Value);

            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool WareHouse_Update(decimal WareHouse_Id, string WareHous_Name, string Location)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters);
                spParameter[0].Value = WareHouse_Id;
                spParameter[1].Value = WareHous_Name;
                spParameter[2].Value = Location;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_WareHouse_Update", spParameter);

                return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
