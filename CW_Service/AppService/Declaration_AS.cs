using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using ZetaCompressionLibrary;

namespace CW_Service
{
    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] Declaration_GetAll();

        [OperationContract()]
        byte[] Declaration_GetByCode(string p_Declaration_Code);

        [OperationContract()]
        byte[] Declaration_GetByContract(decimal p_Contract_Id);

        [OperationContract()]
        bool Declaration_Delete(decimal p_Declaration_Id);

        [OperationContract()]
        bool Declaration_Update_Status(decimal Declaration_Id, decimal Status, string Reason, string Approve_By);

        [OperationContract()]
        decimal Declaration_Insert(string Declaration_Code, decimal Contract_Id, string Contract_Code, decimal Type, decimal Declaration_Type,
            DateTime Register_Date, decimal Money_Type, decimal Total_Value, decimal WareHouse_Id, string WareHouse_Name, string WareHouse_Location, string Gate,
            decimal Receive_Number, decimal Receive_Year, decimal VanDon_Number, DateTime VanDon_Date,
            decimal Status, string Source, decimal Declaration_Refence_Id, string Declaration_Refence_Code, string Custom_Register,
            string CREATED_BY, DateTime CREATED_DATE);

        [OperationContract()]
        int Declaration_Update(decimal Declaration_Id, decimal Declaration_Type,
            DateTime Register_Date, decimal Money_Type, decimal Total_Value, decimal WareHouse_Id, string WareHouse_Name, string WareHouse_Location, string Gate,
            decimal Receive_Number, decimal Receive_Year, decimal VanDon_Number, DateTime VanDon_Date, string Source, string Custom_Register,
            string MODIFIED_BY, DateTime MODIFIED_DATE, decimal Status);

        [OperationContract()]
        byte[] DeclarationTableSearch(string USER_ID, decimal USER_TYPE, string USER_NAME, string DECLARATION_CODE, string CONTRACT_CODE, string RECEIVE_NUMBER, string RECEIVE_YEAR, string SO_VAN_DON,
            int TYPE, int STATUS, string REGISTER_DATE, decimal WAREHOUSE_ID, string NGAY_VAN_DON, string DECLARATION_REFENCE_CODE,
            string p_order_by, int p_start, int p_end, ref int p_total);

       [OperationContract()]
         byte[] DeclarationLiquidationSearch(string USER_ID, decimal USER_TYPE, string USER_NAME, string DECLARATION_CODE, string CONTRACT_CODE, string RECEIVE_NUMBER, string RECEIVE_YEAR, string SO_VAN_DON,
            int TYPE, int STATUS, string REGISTER_DATE, decimal WAREHOUSE_ID, string NGAY_VAN_DON, string DECLARATION_REFENCE_CODE,
            string p_order_by, int p_start, int p_end, ref int p_total);

        

        [OperationContract()]
        byte[] DeclarationGetById(decimal p_id);
    }

    public partial class CWService : CWContractService
    {
        public byte[] Declaration_GetAll()
        {
            try
            {
                Declaration_DA _objDA = new Declaration_DA();
                DataSet _ds = _objDA.Declaration_GetAll();
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Declaration_GetByCode(string p_Declaration_Code)
        {
            try
            {
                Declaration_DA _objDA = new Declaration_DA();
                DataSet _ds = _objDA.Declaration_GetByCode(p_Declaration_Code);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Declaration_GetByContract(decimal p_Contract_Id)
        {
            try
            {
                Declaration_DA _objDA = new Declaration_DA();
                DataSet _ds = _objDA.Declaration_GetByContract(p_Contract_Id);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public bool Declaration_Delete(decimal p_Declaration_Id)
        {
            try
            {
                Declaration_DA _objDA = new Declaration_DA();
                return _objDA.Declaration_Delete(p_Declaration_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Declaration_Update_Status(decimal Declaration_Id, decimal Status, string Reason, string Approve_By)
        {
            try
            {
                Declaration_DA _objDA = new Declaration_DA();
                return _objDA.Declaration_Update_Status(Declaration_Id, Status, Reason, Approve_By);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
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
                Declaration_DA _objDA = new Declaration_DA();
                return _objDA.Declaration_Insert(Declaration_Code, Contract_Id, Contract_Code, Type, Declaration_Type,
                    Register_Date, Money_Type, Total_Value, WareHouse_Id, WareHouse_Name, WareHouse_Location, Gate,
                    DBMemory.Declare_Get_NextNumber_Receive(), Receive_Year, VanDon_Number, VanDon_Date,
                    Status, Source, Declaration_Refence_Id, Declaration_Refence_Code, Custom_Register, CREATED_BY, CREATED_DATE);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
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
                Declaration_DA _objDA = new Declaration_DA();
                return _objDA.Declaration_Update(Declaration_Id, Declaration_Type,
                    Register_Date, Money_Type, Total_Value, WareHouse_Id, WareHouse_Name, WareHouse_Location, Gate,
                    Receive_Number, Receive_Year, VanDon_Number, VanDon_Date,
                    Source, Custom_Register, MODIFIED_BY, MODIFIED_DATE, Status);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }
  
        public byte[] DeclarationTableSearch(string USER_ID, decimal USER_TYPE, string USER_NAME,string DECLARATION_CODE, string CONTRACT_CODE, string RECEIVE_NUMBER,
            string RECEIVE_YEAR,  string SO_VAN_DON,
            int TYPE, int STATUS, string REGISTER_DATE, decimal WAREHOUSE_ID, string NGAY_VAN_DON, string DECLARATION_REFENCE_CODE,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                Declaration_DA _objDA = new Declaration_DA();
                DataSet _ds = _objDA.DeclarationTableSearch(USER_ID, USER_TYPE,  USER_NAME, DECLARATION_CODE, CONTRACT_CODE, RECEIVE_NUMBER, RECEIVE_YEAR,
                SO_VAN_DON, TYPE,  STATUS, REGISTER_DATE, WAREHOUSE_ID,  NGAY_VAN_DON, DECLARATION_REFENCE_CODE,
                p_order_by,  p_start, p_end, ref p_total);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }


        public byte[] DeclarationLiquidationSearch(string USER_ID, decimal USER_TYPE, string USER_NAME, string DECLARATION_CODE, string CONTRACT_CODE, string RECEIVE_NUMBER,
            string RECEIVE_YEAR,  string SO_VAN_DON,
            int TYPE, int STATUS, string REGISTER_DATE, decimal WAREHOUSE_ID, string NGAY_VAN_DON, string DECLARATION_REFENCE_CODE,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                Declaration_DA _objDA = new Declaration_DA();
                DataSet _ds = _objDA.DeclarationLiquidationSearch(USER_ID, USER_TYPE, USER_NAME, DECLARATION_CODE, CONTRACT_CODE, RECEIVE_NUMBER, RECEIVE_YEAR,
                SO_VAN_DON, TYPE,  STATUS, REGISTER_DATE, WAREHOUSE_ID,  NGAY_VAN_DON, DECLARATION_REFENCE_CODE,
                p_order_by,  p_start, p_end, ref p_total);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] DeclarationGetById(decimal p_id)
        {
            try
            {
                Declaration_DA _objDA = new Declaration_DA();
                DataSet _ds = _objDA.DeclarationGetById(p_id);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }
    }
}
