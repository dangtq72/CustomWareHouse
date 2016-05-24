using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CW_Info;

namespace CW_Service
{
    public class DBMemory
    {
        public static bool c_isRunThread = true;
        public static DateTime c_trading_date_Core = new DateTime();                                               // ngày giao dịch, đồng bộ từ CORE

        static decimal Contract_Receive_Number = 0;
        static object c_contractLockObject = new object();

        static decimal Declare_Receive_Number = 0;
        static object c_DeclareLockObject = new object();

        public static List<User_Info> c_lst_UserDoanhNghiep = new List<User_Info>();
        public static List<WareHouse_Info> c_lst_WareHouse = new List<WareHouse_Info>();
        public static List<Product_Info> c_lst_Product = new List<Product_Info>();

        public int CheckConnect_Database(ref string p_error)
        {
            try
            {
                if (Check_Connect(Config_DB_Info.GConnectionString) == false)
                {
                    p_error = "Lỗi kết nối DB Customs Warehouse";
                    return 1;
                }
                else return 0;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        bool Check_Connect(string p_ConnectionString)
        {
            try
            {
                AllCodeDA objAllCodeDA = new AllCodeDA();
                DataSet ds = objAllCodeDA.Allcode_GetAll();

                if (ds != null && ds.Tables.Count > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public static decimal Contract_Get_NextNumber_Receive()
        {
            try
            {
                lock (c_contractLockObject)
                    return Contract_Receive_Number + 1;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Contract_Receive_Number + 1;
            }
        }

        public static decimal Declare_Get_NextNumber_Receive()
        {
            try
            {
                lock (c_DeclareLockObject)
                    return Declare_Receive_Number + 1;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Declare_Receive_Number + 1;
            }
        }

        public static void Get_First_NumberReceive()
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[2];
                spParameter[0] = new SqlParameter("@Contract_Receive_Number", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Output;
                spParameter[0].Value = -1;

                spParameter[1] = new SqlParameter("@Declare_Receive_Number", SqlDbType.Decimal);
                spParameter[1].Direction = ParameterDirection.Output;
                spParameter[1].Value = -1;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Get_First_NumberReceive", spParameter);

                if (spParameter[0].Value != System.DBNull.Value)
                    Contract_Receive_Number = Convert.ToDecimal(spParameter[0].Value);

                if (spParameter[1].Value != System.DBNull.Value)
                    Declare_Receive_Number = Convert.ToDecimal(spParameter[1].Value);
            }
            catch (Exception ex)
            {
                Contract_Receive_Number = 0;
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public static void GetUser_DoanhNghiep()
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                DataSet _ds = _UserInfoDA.UserInfo_GetByType((decimal)NaviCommon.Enum_User_Type.DoanhNghiep);
                c_lst_UserDoanhNghiep = NaviCommon.CBO<User_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public static void GetWareHouse()
        {
            try
            {
                WareHouse_DA _objDa = new WareHouse_DA();
                DataSet _ds = _objDa.WAREHOUSE_GET_ALL();
                c_lst_WareHouse = NaviCommon.CBO<WareHouse_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public static void GetProduct()
        {
            try
            {
                Product_DA _objDa = new Product_DA();
                DataSet _ds = _objDa.Product_GetAll();
                c_lst_Product = NaviCommon.CBO<Product_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }
    }
}
