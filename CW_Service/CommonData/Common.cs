using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Net;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CW_Service
{
    public class Common
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }

    public class SMS_Config_Info
    {
        // ESMS
        public static decimal Is_Use_ESMS = 0;
        public static string ConfigSMS = "";
        public static string APIKey = "";
        public static string SecretKey = "";
        public static string SMS_TYPE = "";
        public static string BranhchName = "";

        // gapit

        public static void Load_Sms_Config()
        {
            try
            {
                // chung
                BranhchName = System.Configuration.ConfigurationManager.AppSettings["Brandname"];

                // tạm thời ko dùng của ESMS
                Is_Use_ESMS = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["Is_Use_ESMS"]);
                APIKey = System.Configuration.ConfigurationManager.AppSettings["APIKey"];
                SecretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"];
                SMS_TYPE = System.Configuration.ConfigurationManager.AppSettings["SMS_TYPE"];
                ConfigSMS = System.Configuration.ConfigurationManager.AppSettings["SMSCONFIG"];

            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
            }
        }
    }

    public class Config_DB_Info
    {

        /// <summary>
        /// biến lưu chuối kết nối database  V_Voting
        /// </summary>
        public static string GConnectionString = "";

        public static void GetAppSettings()
        {
            try
            {
                GConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
            }
        }
    }

    public class Master
    {
        static bool c_HadRunOne = false; //dánh dấu đã gọi lần nào chưa? nếu gọi 1 lần rồi thì sẽ cập nhật thành true

        /// <summary>
        /// Khoi tao luc dau.
        /// return = 0 la ko co loi gi ca,
        /// return = 1 lỗi kết nối db qr, 2-dbcore, 3-dbMSDS
        /// </summary>
        public static int RunStart(ref string p_error)
        {
            try
            {
                if (c_HadRunOne == false)
                {
                    // check connect database
                    DBMemory _DBMemory = new DBMemory();
                    int _db = _DBMemory.CheckConnect_Database(ref p_error);
                    if (_db != 0)
                    {
                        return _db;
                    }

                    DBMemory.Get_First_NumberReceive();

                    DBMemory.GetUser_DoanhNghiep();
                    DBMemory.GetWareHouse();
                    DBMemory.GetProduct();

                    Operator _Operator = new Operator();
                    Thread _thr_auto_SendAlert = new Thread(_Operator.ThreadSendAlert_Expiredate);
                    _thr_auto_SendAlert.IsBackground = true;
                    //_thr_auto_SendAlert.Start();

                    //chua chay thì chay và đánh dấu là đã chạy để biét và không chạy lần nữa
                    c_HadRunOne = true;
                }

                return 0;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public static string[] GetParamName(MethodInfo p_method)
        {
            try
            {
                if (p_method == null) return null;
                int _length = p_method.GetParameters().Length;
                string[] retVal = new string[_length];
                string _name = "";
                string _type;

                for (int i = 0; i < _length; i++)
                {
                    _name = p_method.GetParameters()[i].Name;
                    _type = p_method.GetParameters()[i].ParameterType.Name;
                    retVal[i] = _name + "|" + _type;
                }


                return retVal;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        public static SqlParameter[] Create_SqlParameter(ParameterInfo[] parameters, bool p_isOut = false)
        {
            try
            {
                //MethodInfo _method = typeof(Product_DA).GetMethod("Product_Insert");
                int _length = parameters.Length;
                string[] retVal = new string[_length];

                SqlParameter[] spParameter;
                if (p_isOut == false)
                    spParameter = new SqlParameter[_length];
                else
                    spParameter = new SqlParameter[_length + 1];

                string _name = "";
                string _type;

                for (int i = 0; i < _length; i++)
                {
                    _name = parameters[i].Name;
                    _type = parameters[i].ParameterType.Name;
                    string _parameterName = "@" + _name;
                    if (_type == "String")
                    {
                        spParameter[i] = new SqlParameter(_parameterName, SqlDbType.NVarChar);
                    }
                    else if (_type == "Int32" || _type == "Int64" || _type == "Decimal")
                    {
                        spParameter[i] = new SqlParameter(_parameterName, SqlDbType.Decimal);
                    }
                    else if (_type == "DateTime")
                    {
                        spParameter[i] = new SqlParameter(_parameterName, SqlDbType.Date);
                    }
                    spParameter[i].Direction = ParameterDirection.Input;
                    spParameter[i].Value = _name;
                }

                return spParameter;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }
    }
}
