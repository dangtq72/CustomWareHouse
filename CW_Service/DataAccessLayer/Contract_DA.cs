using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CW_Service
{
    public class Contract_DA
    {
        public DataSet Contract_GetAll()
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[0];
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Contract_GetAll", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Contract_GetByStatus(int p_status)
        {
            try
            {
                SqlConnection _cnn = new SqlConnection();
                _cnn.ConnectionString = Config_DB_Info.GConnectionString;
                DataSet _ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "PROC_GET_CONTRACT_BY_STATUS";
                ClSQLTool.addParamater(_cmd, "@STATUS", p_status, SqlDbType.Int);
                SqlDataAdapter _da = new SqlDataAdapter();
                _da.SelectCommand = _cmd;
                _cmd.Connection = _cnn;
                try
                {
                    _cnn.Open();
                    _da.Fill(_ds);
                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                }
                finally
                {
                    _cnn.Close();
                }
                return _ds;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet TableSearch(string CONTRACT_CODE, DateTime FR0MDATE, int STATUS, decimal User_Type,string User_Id,string Created_By,
            decimal Receive_Number,decimal Receive_Year,decimal WareHouse_Id,decimal Business_Id,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                SqlConnection _cnn = new SqlConnection();
                _cnn.ConnectionString = Config_DB_Info.GConnectionString;
                DataSet _ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "PROC_CONTRACTS_SEARCH";
                ClSQLTool.addParamater(_cmd, "@CONTRACT_CODE", CONTRACT_CODE, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Register_Date", FR0MDATE.ToString("MM/dd/yyyy"), SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@STATUS", STATUS, SqlDbType.Int);

                ClSQLTool.addParamater(_cmd, "@User_Type", User_Type, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@User_Id", User_Id, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Created_By", Created_By, SqlDbType.NVarChar);

                ClSQLTool.addParamater(_cmd, "@Receive_Number", Receive_Number, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Receive_Year", Receive_Year, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@WareHouse_Id", WareHouse_Id, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Business_Id", Business_Id, SqlDbType.Decimal);

                ClSQLTool.addParamater(_cmd, "@P_ORDER_BY", p_order_by, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@P_START", p_start.ToString(), SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@P_END", p_end.ToString(), SqlDbType.NVarChar);
                SqlParameter _Total = new SqlParameter("@P_TOTAL", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
                _cmd.Parameters.Add(_Total);
                SqlDataAdapter _da = new SqlDataAdapter();
                _da.SelectCommand = _cmd;
                _cmd.Connection = _cnn;
                try
                {
                    _cnn.Open();
                    _da.Fill(_ds);
                    p_total = Convert.ToInt32(_Total.Value.ToString());
                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                }
                finally
                {
                    _cnn.Close();
                }
                 
                return _ds;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet CONTRACTS_SEARCH_THANHKHOAN(string CONTRACT_CODE, DateTime FR0MDATE, int STATUS, decimal User_Type, string User_Id, string Created_By,
            decimal Receive_Number, decimal Receive_Year, decimal WareHouse_Id, decimal Business_Id,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                SqlConnection _cnn = new SqlConnection();
                _cnn.ConnectionString = Config_DB_Info.GConnectionString;
                DataSet _ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "PROC_CONTRACTS_SEARCH_THANHKHOAN";
                ClSQLTool.addParamater(_cmd, "@CONTRACT_CODE", CONTRACT_CODE, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Register_Date", FR0MDATE.ToString("MM/dd/yyyy"), SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@STATUS", STATUS, SqlDbType.Int);

                ClSQLTool.addParamater(_cmd, "@User_Type", User_Type, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@User_Id", User_Id, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Created_By", Created_By, SqlDbType.NVarChar);

                ClSQLTool.addParamater(_cmd, "@Receive_Number", Receive_Number, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Receive_Year", Receive_Year, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@WareHouse_Id", WareHouse_Id, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Business_Id", Business_Id, SqlDbType.Decimal);

                ClSQLTool.addParamater(_cmd, "@P_ORDER_BY", p_order_by, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@P_START", p_start.ToString(), SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@P_END", p_end.ToString(), SqlDbType.NVarChar);
                SqlParameter _Total = new SqlParameter("@P_TOTAL", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
                _cmd.Parameters.Add(_Total);
                SqlDataAdapter _da = new SqlDataAdapter();
                _da.SelectCommand = _cmd;
                _cmd.Connection = _cnn;
                try
                {
                    _cnn.Open();
                    _da.Fill(_ds);
                    p_total = Convert.ToInt32(_Total.Value.ToString());
                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                }
                finally
                {
                    _cnn.Close();
                }

                return _ds;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Contract_GetByCode(string p_Contract_Code)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@Contract_Code", SqlDbType.VarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_Contract_Code;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Contract_GetByCode", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Contract_GetById(decimal p_Contract_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@CONTRACT_ID", SqlDbType.VarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_Contract_Id;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_CONTRACT_GET_BY_ID", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        /// <summary>
        /// Lay hop dong theo kho
        /// </summary>
        /// <param name="WareHouse_Id"></param>
        /// <returns></returns>
        public DataSet Contract_GetBy_WareHouse(decimal WareHouse_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@WareHouse_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = WareHouse_Id;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Contract_GetByWareHouse", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        /// <summary>
        /// Lay hop dong theo user
        /// </summary>
        /// <param name="WareHouse_Id"></param>
        /// <returns></returns>
        public DataSet Contract_GetBy_User(string User_Name)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@User_Name", SqlDbType.VarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = User_Name;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Contract_GetByUserName", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public decimal Contract_Delete(decimal p_Contract_Id)
        {
            try
            {

                SqlParameter[] spParameter = new SqlParameter[2];
                spParameter[0] = new SqlParameter("@CONTRACT_ID", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_Contract_Id;

                decimal _id = -1;
                spParameter[1] = new SqlParameter("@P_RETURN", SqlDbType.Decimal);
                spParameter[1].Direction = ParameterDirection.Output;
                spParameter[1].Value = _id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_CONTRACT_DELETE", spParameter);

                return Convert.ToDecimal(spParameter[0].Value);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool Contract_Insert(string CONTRACT_CODE, DateTime REGISTER_DATE, decimal PERIOD,
            decimal RECEIVE_YEAR, decimal RECEIVE_NUMBER, decimal WAREHOUSE_ID, string WAREHOUSE_NAME, decimal BUSINESS_ID,
             decimal MONEY_TYPE, decimal STATUS, string CREATED_BY, DateTime CREATED_DATE,DateTime DUE_DATE)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters);
                spParameter[0].Value = CONTRACT_CODE;
                spParameter[1].Value = REGISTER_DATE;
                spParameter[2].Value = PERIOD;
                spParameter[3].Value = RECEIVE_NUMBER;
                spParameter[4].Value = RECEIVE_YEAR;
                spParameter[5].Value = WAREHOUSE_ID;
                spParameter[6].Value = WAREHOUSE_NAME;
                spParameter[7].Value = BUSINESS_ID;
               
                spParameter[8].Value = MONEY_TYPE;
                spParameter[9].Value = STATUS;

                spParameter[10].Value = CREATED_BY;
                spParameter[11].Value = CREATED_DATE;
                spParameter[12].Value = DUE_DATE;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Contract_Insert", spParameter);

                return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Contract_Update(decimal CONTRACT_ID, DateTime REGISTER_DATE, decimal PERIOD,
            decimal RECEIVE_YEAR, decimal WAREHOUSE_ID, string WAREHOUSE_NAME, decimal BUSINESS_ID,
            decimal MONEY_TYPE, string MODIFIED_BY, DateTime MODIFIED_DATE, decimal STATUS,DateTime DUE_DATE)
        {
            try
            {
                SqlConnection _cnn = new SqlConnection();
                _cnn.ConnectionString = Config_DB_Info.GConnectionString;
                DataSet _ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "PROC_CONTRACT_UPDATE";
                ClSQLTool.addParamater(_cmd, "@CONTRACT_ID", CONTRACT_ID, SqlDbType.Decimal); 
                ClSQLTool.addParamater(_cmd, "@REGISTER_DATE", REGISTER_DATE, SqlDbType.Date);
                ClSQLTool.addParamater(_cmd, "@PERIOD", PERIOD, SqlDbType.Int);
                ClSQLTool.addParamater(_cmd, "@RECEIVE_YEAR", RECEIVE_YEAR, SqlDbType.Int);
                ClSQLTool.addParamater(_cmd, "@WAREHOUSE_ID", WAREHOUSE_ID, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@WAREHOUSE_NAME", WAREHOUSE_NAME, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@BUSINESS_ID", BUSINESS_ID, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@MONEY_TYPE", MONEY_TYPE, SqlDbType.Int);
                ClSQLTool.addParamater(_cmd, "@MODIFIED_BY", MODIFIED_BY, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@MODIFIED_DATE", MODIFIED_DATE, SqlDbType.Date);
                ClSQLTool.addParamater(_cmd, "@STATUS", STATUS, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@DUE_DATE", DUE_DATE, SqlDbType.Date);
                _cmd.Connection = _cnn;
                try
                {
                    _cnn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                    return false;
                }
                finally
                {
                    _cnn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Contract_Update_Status(decimal Contract_Id, decimal Status, string Reason,string Approve_By)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters);
                spParameter[0].Value = Contract_Id;
                spParameter[1].Value = Status;
                spParameter[2].Value = Reason;
                spParameter[3].Value = Approve_By;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Update_Contract_Status", spParameter);

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
