using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CW_Service
{
    public class Product_Declaration_DA
    {
        public DataSet Product_Declaration_GetById(decimal Product_Declaration_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@Product_Declaration_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Product_Declaration_Id;

                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Product_Declaration_GetById", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public bool Delete_Product_Declaration_ById(decimal Product_Declaration_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@Product_Declaration_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Product_Declaration_Id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_DeleteProduct_Declaration_ById", spParameter);

                return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Delete_Product_Declaration_ByDeclaration_Id(decimal Declaration_Id)
        {
            try
            {
                SqlParameter[] spParameter = new SqlParameter[1];
                spParameter[0] = new SqlParameter("@Declaration_Id", SqlDbType.Decimal);
                spParameter[0].Direction = ParameterDirection.Input;
                spParameter[0].Value = Declaration_Id;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_DeleteProduct_Declaration_ByDeclarationId", spParameter);

                return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Product_Declaration_Insert(decimal Product_Id, decimal Declaration_Id, decimal Type, string Unit,
            string Made_In, decimal Wrong_Number, decimal Package_Quantity, decimal Quantity, decimal Value,
            decimal Package_Quantity_Delivery, decimal Quantity_Delivery, decimal Declaration_Reference_Id)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters);
                spParameter[0].Value = Product_Id;
                spParameter[1].Value = Declaration_Id;
                spParameter[2].Value = Type;
                spParameter[3].Value = Unit;
                spParameter[4].Value = Made_In;
                spParameter[5].Value = Wrong_Number;
                spParameter[6].Value = Package_Quantity;
                spParameter[7].Value = Quantity;
                spParameter[8].Value = Value;
                spParameter[9].Value = Package_Quantity_Delivery;
                spParameter[10].Value = Quantity_Delivery;
                spParameter[11].Value = Declaration_Reference_Id;
                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Product_Declaration_Insert", spParameter);

                return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool UpdateQuantity_Delivery(decimal Product_Declaration_Id, decimal Package_Quantity_Delivery, decimal Quantity_Delivery)
        {
            try
            {
                ParameterInfo[] _parameters = MethodBase.GetCurrentMethod().GetParameters();
                SqlParameter[] spParameter = Master.Create_SqlParameter(_parameters);
                spParameter[0].Value = Product_Declaration_Id;
                spParameter[1].Value = Package_Quantity_Delivery;
                spParameter[2].Value = Quantity_Delivery;

                SqlHelper.ExecuteNonQuery(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_Update_Quantity_Delivery", spParameter);

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
