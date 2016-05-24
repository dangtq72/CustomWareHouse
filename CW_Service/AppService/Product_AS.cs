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
        byte[] Product_GetAll();

        [OperationContract()]
        byte[] Product_GetById(decimal Product_Id);

        [OperationContract()]
        byte[] Product_GetByCode(string Product_Code);

        [OperationContract()]
        decimal Product_Delete(decimal Product_Id);

        [OperationContract()]
        byte[] Product_Search(string p_name);

        [OperationContract()]
        bool Product_Insert(string Product_Code, string Product_Name, decimal Category_Id, string Unit);

        [OperationContract()]
        bool Product_Update(decimal Product_Id, string Product_Name, decimal Category_Id, string Unit);
    }

    public partial class CWService : CWContractService
    {
        public byte[] Product_GetAll()
        {
            try
            {
                Product_DA _objDA = new Product_DA();
                DataSet _ds = _objDA.Product_GetAll();
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Product_GetById(decimal Product_Id)
        {
            try
            {
                Product_DA _objDA = new Product_DA();
                DataSet _ds = _objDA.Product_GetById(Product_Id);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Product_GetByCode(string Product_Code)
        {
            try
            {
                Product_DA _objDA = new Product_DA();
                DataSet _ds = _objDA.Product_GetByCode(Product_Code);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Product_Search(string p_name)
        {
            try
            {
                Product_DA _objDA = new Product_DA();
                DataSet _ds = _objDA.Product_Search(p_name);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        
        public decimal Product_Delete(decimal Product_Id)
        {
            try
            {
                Product_DA _objDA = new Product_DA();
                decimal _ck = _objDA.Product_Delete(Product_Id);
                if (_ck == 1)
                    DBMemory.GetProduct();
                return _ck;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool Product_Insert(string Product_Code, string Product_Name, decimal Category_Id, string Unit)
        {
            try
            {
                Product_DA _objDA = new Product_DA();
                bool _ck = _objDA.Product_Insert(Product_Code, Product_Name, Category_Id, Unit);
                if (_ck)
                    DBMemory.GetProduct();

                return _ck;
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
                Product_DA _objDA = new Product_DA();
                bool _ck = _objDA.Product_Update(Product_Id, Product_Name, Category_Id, Unit);
                if (_ck)
                    DBMemory.GetProduct();

                return _ck;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

    }
}
