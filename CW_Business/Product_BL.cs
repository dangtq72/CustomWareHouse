using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using ZetaCompressionLibrary;

namespace CW_Business
{
    public class Product_BL
    {
        public List<Product_Info> Product_GetAll()
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Product_GetAll();
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Product_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Product_Info>();
            }
        }

        public Product_Info Product_GetById(decimal Product_Id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Product_GetById(Product_Id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Product_Info> _lst = NaviCommon.CBO<Product_Info>.FillCollectionFromDataSet(_ds);
                if (_lst.Count > 0)
                {
                    return _lst[0];
                }
                return new Product_Info();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Product_Info();
            }
        }

        public Product_Info Product_GetByCode(string Product_Code)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Product_GetByCode(Product_Code);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Product_Info> _lst = NaviCommon.CBO<Product_Info>.FillCollectionFromDataSet(_ds);
                if (_lst.Count > 0)
                {
                    return _lst[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        public List<Product_Info> Product_Search(string p_name)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Product_Search(p_name);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Product_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Product_Info>();
            }
        }


        public decimal Product_Delete(decimal Product_Id)
        {
            try
            {
                return CommonData.c_serviceWCF.Product_Delete(Product_Id);
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
                return CommonData.c_serviceWCF.Product_Insert(Product_Code, Product_Name, Category_Id, Unit);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Product_Update(decimal Product_Id, string Product_Name, decimal Category_Id, string Unit)
        {
            try
            {
                return CommonData.c_serviceWCF.Product_Update(Product_Id, Product_Name, Category_Id, Unit);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

    }
}
