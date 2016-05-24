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
        byte[] Product_Declaration_GetById(decimal Product_Declaration_Id);

        [OperationContract()]
        bool Delete_Product_Declaration_ById(decimal Product_Declaration_Id);

        [OperationContract()]
        bool Delete_Product_Declaration_ByDeclaration_Id(decimal Declaration_Id);

        [OperationContract()]
        bool Product_Declaration_Insert(decimal Product_Id, decimal Declaration_Id, decimal Type, string Unit,
            string Made_In, decimal Wrong_Number, decimal Package_Quantity, decimal Quantity, decimal Value,
            decimal Package_Quantity_Delivery, decimal Quantity_Delivery, decimal Declaration_Reference_Id);

        [OperationContract()]
        bool UpdateQuantity_Delivery(decimal Product_Declaration_Id, decimal Package_Quantity_Delivery, decimal Quantity_Delivery);
    }

    public partial class CWService : CWContractService
    {
        public byte[] Product_Declaration_GetById(decimal Product_Declaration_Id)
        {
            try
            {
                Product_Declaration_DA _objDA = new Product_Declaration_DA();
                DataSet _ds = _objDA.Product_Declaration_GetById(Product_Declaration_Id);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public bool Delete_Product_Declaration_ById(decimal Product_Declaration_Id)
        {
            try
            {
                Product_Declaration_DA _objDA = new Product_Declaration_DA();
                return _objDA.Delete_Product_Declaration_ById(Product_Declaration_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Delete_Product_Declaration_ByDeclaration_Id(decimal Declaration_Id)
        {
            try
            {
                Product_Declaration_DA _objDA = new Product_Declaration_DA();
                return _objDA.Delete_Product_Declaration_ByDeclaration_Id(Declaration_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Product_Declaration_Insert(decimal Product_Id, decimal Declaration_Id, decimal Type, string Unit,
            string Made_In, decimal Wrong_Number, decimal Package_Quantity, decimal Quantity, decimal Value,
            decimal Package_Quantity_Delivery, decimal Quantity_Delivery, decimal Declaration_Reference_Id)
        {
            try
            {
                Product_Declaration_DA _objDA = new Product_Declaration_DA();
                return _objDA.Product_Declaration_Insert(Product_Id, Declaration_Id, Type, Unit,
                    Made_In, Wrong_Number, Package_Quantity, Quantity, Value,
                    Package_Quantity_Delivery, Quantity_Delivery, Declaration_Reference_Id);
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
                Product_Declaration_DA _objDA = new Product_Declaration_DA();
                return _objDA.UpdateQuantity_Delivery(Product_Declaration_Id, Package_Quantity_Delivery, Quantity_Delivery);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
