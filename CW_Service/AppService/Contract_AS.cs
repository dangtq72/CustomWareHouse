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
        byte[] Contract_GetAll();

        [OperationContract()]
        byte[] Contract_GetByCode(string p_Contract_Code);

        [OperationContract()]
        byte[] Contract_GetById(decimal p_Contract_Id);

        [OperationContract()]
        byte[] Contract_GetBy_WareHouse(decimal WareHouse_Id);

        [OperationContract()]
        byte[] Contract_GetBy_User(string User_Name);

        [OperationContract()]
        decimal Contract_Delete(decimal p_Contract_Id);

        [OperationContract()]
        bool Contract_Update_Status(decimal Contract_Id, decimal Status, string Reason, string Approve_By);

        [OperationContract()]
        bool Contract_Insert(string CONTRACT_CODE, DateTime REGISTER_DATE, decimal PERIOD,
            decimal RECEIVE_YEAR, decimal WAREHOUSE_ID, string WAREHOUSE_NAME, decimal BUSINESS_ID,
             decimal MONEY_TYPE, decimal STATUS, string CREATED_BY, DateTime CREATED_DATE, DateTime DUE_DATE);

        [OperationContract()]
        bool Contract_Update(decimal CONTRACT_ID, DateTime REGISTER_DATE, decimal PERIOD,
            decimal RECEIVE_YEAR, decimal WAREHOUSE_ID, string WAREHOUSE_NAME, decimal BUSINESS_ID,
            decimal MONEY_TYPE, string MODIFIED_BY, DateTime MODIFIED_DATE, decimal STATUS, DateTime DUE_DATE);

        [OperationContract()]
        byte[] TableSearch(string CONTRACT_CODE, DateTime FR0MDATE, int STATUS, decimal User_Type, string User_Id, string Created_By,
            decimal Receive_Number, decimal Receive_Year, decimal WareHouse_Id, decimal Business_Id,
            string p_order_by, int p_start, int p_end, ref int p_total);

        [OperationContract()]
        byte[] Contract_Search_ThanhKhoan(string CONTRACT_CODE, DateTime FR0MDATE, int STATUS, decimal User_Type, string User_Id, string Created_By,
            decimal Receive_Number, decimal Receive_Year, decimal WareHouse_Id, decimal Business_Id,
            string p_order_by, int p_start, int p_end, ref int p_total);

        [OperationContract()]
        byte[] Contract_GetByStatus(int p_status);
    }

    public partial class CWService : CWContractService
    {
        public byte[] Contract_GetAll()
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                DataSet _ds = _objDA.Contract_GetAll();
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Contract_GetByStatus(int p_status)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                DataSet _ds = _objDA.Contract_GetByStatus(p_status);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Contract_GetByCode(string p_Contract_Code)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                DataSet _ds = _objDA.Contract_GetByCode(p_Contract_Code);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] TableSearch(string CONTRACT_CODE, DateTime FR0MDATE, int STATUS, decimal User_Type, string User_Id, string Created_By,
            decimal Receive_Number, decimal Receive_Year, decimal WareHouse_Id, decimal Business_Id,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                DataSet _ds = _objDA.TableSearch(CONTRACT_CODE, FR0MDATE, STATUS, User_Type, User_Id, Created_By,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    p_order_by, p_start, p_end, ref p_total);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Contract_Search_ThanhKhoan(string CONTRACT_CODE, DateTime FR0MDATE, int STATUS, decimal User_Type, string User_Id, string Created_By,
            decimal Receive_Number, decimal Receive_Year, decimal WareHouse_Id, decimal Business_Id,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                DataSet _ds = _objDA.CONTRACTS_SEARCH_THANHKHOAN(CONTRACT_CODE, FR0MDATE, STATUS, User_Type, User_Id, Created_By,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    p_order_by, p_start, p_end, ref p_total);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        

        public byte[] Contract_GetById(decimal p_Contract_Id)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                DataSet _ds = _objDA.Contract_GetById(p_Contract_Id);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }


        public byte[] Contract_GetBy_WareHouse(decimal WareHouse_Id)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                DataSet _ds = _objDA.Contract_GetBy_WareHouse(WareHouse_Id);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Contract_GetBy_User(string User_Name)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                DataSet _ds = _objDA.Contract_GetBy_User(User_Name);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public decimal Contract_Delete(decimal p_Contract_Id)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                return _objDA.Contract_Delete(p_Contract_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool Contract_Update_Status(decimal Contract_Id, decimal Status, string Reason, string Approve_By)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                return _objDA.Contract_Update_Status(Contract_Id, Status, Reason, Approve_By);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Contract_Insert(string CONTRACT_CODE, DateTime REGISTER_DATE, decimal PERIOD,
            decimal RECEIVE_YEAR, decimal WAREHOUSE_ID, string WAREHOUSE_NAME, decimal BUSINESS_ID,
             decimal MONEY_TYPE, decimal STATUS, string CREATED_BY, DateTime CREATED_DATE, DateTime DUE_DATE)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                return _objDA.Contract_Insert(CONTRACT_CODE, REGISTER_DATE, PERIOD,
                    RECEIVE_YEAR,DBMemory.Contract_Get_NextNumber_Receive(), WAREHOUSE_ID, WAREHOUSE_NAME, BUSINESS_ID,
                    MONEY_TYPE, STATUS, CREATED_BY, CREATED_DATE, DUE_DATE);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Contract_Update(decimal CONTRACT_ID, DateTime REGISTER_DATE, decimal PERIOD,
            decimal RECEIVE_YEAR, decimal WAREHOUSE_ID, string WAREHOUSE_NAME, decimal BUSINESS_ID,
            decimal MONEY_TYPE, string MODIFIED_BY, DateTime MODIFIED_DATE, decimal STATUS, DateTime DUE_DATE)
        {
            try
            {
                Contract_DA _objDA = new Contract_DA();
                return _objDA.Contract_Update(CONTRACT_ID, REGISTER_DATE, PERIOD,
                    RECEIVE_YEAR, WAREHOUSE_ID, WAREHOUSE_NAME, BUSINESS_ID,
                    MONEY_TYPE, MODIFIED_BY, MODIFIED_DATE,STATUS, DUE_DATE);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
