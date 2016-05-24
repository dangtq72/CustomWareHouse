using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using ZetaCompressionLibrary;

namespace CW_Business
{
    public class Product_Declaration_BL
    {
        public List<Product_Declaration_Info> Product_Declaration_GetById(decimal Product_Declaration_Id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Product_Declaration_GetById(Product_Declaration_Id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Product_Declaration_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Product_Declaration_Info>();
            }
        }

        public bool Delete_Product_Declaration_ById(decimal Product_Declaration_Id)
        {
            try
            {
                return CommonData.c_serviceWCF.Delete_Product_Declaration_ById(Product_Declaration_Id);
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
                return CommonData.c_serviceWCF.Delete_Product_Declaration_ByDeclaration_Id(Declaration_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Product_Declaration_Insert(Product_Declaration_Info Product_Declaration_Info)
        {
            try
            {
                return CommonData.c_serviceWCF.Product_Declaration_Insert(Product_Declaration_Info.Product_Id, Product_Declaration_Info.Declaration_Id, Product_Declaration_Info.Type, Product_Declaration_Info.Unit,
                    Product_Declaration_Info.Made_In, Product_Declaration_Info.Wrong_Number, Product_Declaration_Info.Package_Quantity, Product_Declaration_Info.Quantity, Product_Declaration_Info.Value,
                    Product_Declaration_Info.Package_Quantity_Delivery, Product_Declaration_Info.Quantity_Delivery, Product_Declaration_Info.Declaration_Reference_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool UpdateQuantity_Delivery(decimal Product_Declaration_Id, decimal Package_Quantity_Delivery, decimal Quantity_Delivery)
        {
            try
            {
                return CommonData.c_serviceWCF.UpdateQuantity_Delivery(Product_Declaration_Id, Package_Quantity_Delivery, Quantity_Delivery);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
