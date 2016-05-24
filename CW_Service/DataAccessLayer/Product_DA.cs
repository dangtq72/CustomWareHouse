using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CW_Service
{
    public class Product_DA
    {
        public DataSet Product_GetAll()
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[0];
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Product_GetAll", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Product_GetById(decimal Product_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@Product_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Product_Id;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Product_GetById", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Product_GetByCode(string Product_Code)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@Product_Code", SqlDbType.NVarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Product_Code;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Product_GetById", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Product_Search(string p_name)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@p_name", SqlDbType.NVarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_name;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Product_Search", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }


        public decimal Product_Delete(decimal Product_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[2];
                spParameter[0] = new SqlParameter("@Product_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Product_Id;

                decimal _id = -1;
                spParameter[1] = new SqlParameter("@P_RETURN", SqlDbType.Decimal);
                spParameter[1].Direction = ParameterDirection.Output;
                spParameter[1].Value = _id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Product_Delete", spParameter);

                return Convert.ToDecimal(spParameter[1].Value);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool Product_Insert(string Product_Code, string Product_Name, decimal Category_Id,string Unit)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters);
                spParameter[0].Value = Product_Code;
                spParameter[1].Value = Product_Name;
                spParameter[2].Value = Category_Id;
                spParameter[3].Value = Unit;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Product_Insert", spParameter);

                return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Product_Update(decimal Product_Id, string Product_Name, decimal Category_Id, string Unit)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters);
                spParameter[0].Value = Product_Id;
                spParameter[1].Value = Product_Name;
                spParameter[2].Value = Category_Id;
                spParameter[3].Value = Unit;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Product_Update", spParameter);

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
