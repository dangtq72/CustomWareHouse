using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CW_Service
{
    public class Declaration_DA
    {
        public DataSet Declaration_GetAll()
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[0];
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Declaration_GetAll", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Declaration_GetByCode(string p_Declaration_Code)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@Declaration_Code", SqlDbType.VarChar);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_Declaration_Code;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Declaration_GetByCode", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Declaration_GetByContract(decimal p_Contract_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@Contract_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_Contract_Id;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Declaration_GetByContract", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public bool Declaration_Delete(decimal p_Declaration_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@Declaration_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = p_Declaration_Id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Declaration_Delete", spParameter);

                return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public DataSet Declaration_GetExpireDate()
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[0];
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Get_Contract_ExpireDate", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }


        public decimal Declaration_Insert(string Declaration_Code, decimal Contract_Id, string Contract_Code, decimal Type, decimal Declaration_Type,
            DateTime Register_Date, decimal Money_Type, decimal Total_Value, decimal WareHouse_Id, string WareHouse_Name, string WareHouse_Location, string Gate,
            decimal Receive_Number, decimal Receive_Year, decimal VanDon_Number, DateTime VanDon_Date,
            decimal Status, string Source, decimal Declaration_Refence_Id, string Declaration_Refence_Code, string Custom_Register,
            string CREATED_BY, DateTime CREATED_DATE)
        {
            try
            {
                SqlConnection _cnn = new SqlConnection();
                _cnn.ConnectionString = Config_DB_Info.GConnectionString;
                DataSet _ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "proc_Declaration_Insert";
                ClSQLTool.addParamater(_cmd, "@Declaration_Code", Declaration_Code, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Contract_Id", Contract_Id, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Contract_Code", Contract_Code, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Type", Type, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Declaration_Type", Declaration_Type, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Register_Date", Register_Date, SqlDbType.Date);
                ClSQLTool.addParamater(_cmd, "@Money_Type", Money_Type, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Total_Value", Total_Value, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@WareHouse_Id", WareHouse_Id, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@WareHouse_Name", WareHouse_Name, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@WareHouse_Location", WareHouse_Location, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Gate", Gate, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Receive_Number", Receive_Number, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Receive_Year", Receive_Year, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@VanDon_Number", VanDon_Number, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@VanDon_Date", VanDon_Date, SqlDbType.Date);
                ClSQLTool.addParamater(_cmd, "@Status", Status, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Source", Source, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Declaration_Refence_Id", Declaration_Refence_Id, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Declaration_Refence_Code", Declaration_Refence_Code, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Custom_Register", Custom_Register, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Created_By", CREATED_BY, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Created_Date", CREATED_DATE, SqlDbType.Date);
                SqlParameter p_return = new SqlParameter("@p_return", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
                _cmd.Parameters.Add(p_return);
                _cmd.Connection = _cnn;
                decimal _id = 0;
                try
                {
                    _cnn.Open();
                    _cmd.ExecuteNonQuery();
                    _id = Convert.ToDecimal(p_return.Value.ToString());
                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                }
                finally
                {
                    _cnn.Close();
                }
                return _id;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public int Declaration_Update(decimal Declaration_Id, decimal Declaration_Type,
            DateTime Register_Date, decimal Money_Type, decimal Total_Value, decimal WareHouse_Id, string WareHouse_Name, string WareHouse_Location, string Gate,
            decimal Receive_Number, decimal Receive_Year, decimal VanDon_Number, DateTime VanDon_Date, string Source, string Custom_Register,
            string MODIFIED_BY, DateTime MODIFIED_DATE, decimal Status)
        {
            try
            {

                int _rel = 1;
                SqlConnection _cnn = new SqlConnection();
                _cnn.ConnectionString = Config_DB_Info.GConnectionString;
                DataSet _ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "proc_Declaration_Update";
                ClSQLTool.addParamater(_cmd, "@Declaration_ID", Declaration_Id, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Declaration_Type", Declaration_Type, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Register_Date", Register_Date, SqlDbType.Date);
                ClSQLTool.addParamater(_cmd, "@Money_Type", Money_Type, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Total_Value", Total_Value, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@WareHouse_Id", WareHouse_Id, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@WareHouse_Name", WareHouse_Name, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@WareHouse_Location", WareHouse_Location, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Gate", Gate, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Receive_Number", Receive_Number, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Receive_Year", Receive_Year, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@VanDon_Number", VanDon_Number, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@VanDon_Date", VanDon_Date, SqlDbType.Date);
                ClSQLTool.addParamater(_cmd, "@Status", Status, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@Source", Source, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Custom_Register", Custom_Register, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Modified_By", MODIFIED_BY, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@Modified_Date", MODIFIED_DATE, SqlDbType.Date);
                _cmd.Connection = _cnn;
                try
                {
                    _cnn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                    _rel = -1;
                }
                finally
                {
                    _cnn.Close();
                }
                return _rel;

            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool Declaration_Update_Status(decimal Declaration_Id, decimal Status, string Reason, string Approve_By)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters);
                spParameter[0].Value = Declaration_Id;
                spParameter[1].Value = Status;
                spParameter[2].Value = Reason;
                spParameter[3].Value = Approve_By;
                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Update_Declaration_Status", spParameter);

                return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public DataSet DeclarationTableSearch(string USER_ID, decimal USER_TYPE, string USER_NAME, string DECLARATION_CODE, string CONTRACT_CODE, string RECEIVE_NUMBER,
             string RECEIVE_YEAR, string SO_VAN_DON,
            int TYPE, int STATUS, string REGISTER_DATE, decimal WAREHOUSE_ID, string NGAY_VAN_DON,
            string DECLARATION_REFENCE_CODE,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                SqlConnection _cnn = new SqlConnection();
                _cnn.ConnectionString = Config_DB_Info.GConnectionString;
                DataSet _ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "PROC_DECLARATION_SEARCH";
                ClSQLTool.addParamater(_cmd, "@USER_ID", USER_ID, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@USER_TYPE", USER_TYPE, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@USER_NAME", USER_NAME, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@DECLARATION_CODE", DECLARATION_CODE, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@CONTRACT_CODE", CONTRACT_CODE, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@RECEIVE_NUMBER", RECEIVE_NUMBER, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@RECEIVE_YEAR", RECEIVE_YEAR, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@SO_VAN_DON", SO_VAN_DON, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@TYPE", TYPE, SqlDbType.Int);
                ClSQLTool.addParamater(_cmd, "@STATUS", STATUS, SqlDbType.Int);
                ClSQLTool.addParamater(_cmd, "@REGISTER_DATE", string.IsNullOrEmpty(REGISTER_DATE) ? "01/01/0001" : REGISTER_DATE, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@WAREHOUSE_ID", WAREHOUSE_ID, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@NGAY_VAN_DON", string.IsNullOrEmpty(NGAY_VAN_DON) ? "01/01/0001" : NGAY_VAN_DON, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@DECLARATION_REFENCE_CODE", DECLARATION_REFENCE_CODE, SqlDbType.NVarChar);
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

        public DataSet DeclarationGetById(decimal p_id)
        {
            try
            {
                SqlConnection _cnn = new SqlConnection();
                _cnn.ConnectionString = Config_DB_Info.GConnectionString;
                DataSet _ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "PROC_DECLARATION_GET_BY_ID";
                ClSQLTool.addParamater(_cmd, "@P_ID", p_id, SqlDbType.Decimal);
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
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet DeclarationLiquidationSearch(string USER_ID, decimal USER_TYPE, string USER_NAME, string DECLARATION_CODE, string CONTRACT_CODE, string RECEIVE_NUMBER,
           string RECEIVE_YEAR, string SO_VAN_DON,
          int TYPE, int STATUS, string REGISTER_DATE, decimal WAREHOUSE_ID, string NGAY_VAN_DON,
          string DECLARATION_REFENCE_CODE,
          string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                SqlConnection _cnn = new SqlConnection();
                _cnn.ConnectionString = Config_DB_Info.GConnectionString;
                DataSet _ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "PROC_DECLARATION_LIQUIDATION_SEARCH";
                ClSQLTool.addParamater(_cmd, "@USER_ID", USER_ID, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@USER_TYPE", USER_TYPE, SqlDbType.Decimal);
                ClSQLTool.addParamater(_cmd, "@USER_NAME", USER_NAME, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@DECLARATION_CODE", DECLARATION_CODE, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@CONTRACT_CODE", CONTRACT_CODE, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@RECEIVE_NUMBER", RECEIVE_NUMBER, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@RECEIVE_YEAR", RECEIVE_YEAR, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@SO_VAN_DON", SO_VAN_DON, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@TYPE", TYPE, SqlDbType.Int);
                ClSQLTool.addParamater(_cmd, "@STATUS", STATUS, SqlDbType.Int);
                ClSQLTool.addParamater(_cmd, "@REGISTER_DATE", string.IsNullOrEmpty(REGISTER_DATE) ? "01/01/0001" : REGISTER_DATE, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@WAREHOUSE_ID", WAREHOUSE_ID, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@NGAY_VAN_DON", string.IsNullOrEmpty(NGAY_VAN_DON) ? "01/01/0001" : NGAY_VAN_DON, SqlDbType.NVarChar);
                ClSQLTool.addParamater(_cmd, "@DECLARATION_REFENCE_CODE", DECLARATION_REFENCE_CODE, SqlDbType.NVarChar);
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

    }
}
