using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using ZetaCompressionLibrary;

namespace CW_Business
{
    public class Category_BL
    {
        public List<Categories_Info> Category_GetAll()
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Category_GetAll();
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Categories_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Categories_Info>();
            }
        }

        public List<Categories_Info> Category_Search(string p_name)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Category_Search(p_name);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Categories_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Categories_Info>();
            }
        }

        public Categories_Info Category_GetById(decimal Category_Id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Category_GetById(Category_Id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                List<Categories_Info> _lst = NaviCommon.CBO<Categories_Info>.FillCollectionFromDataSet(_ds);

                if (_lst.Count > 0)
                {
                    return _lst[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Categories_Info();
            }
        }

        public Categories_Info Category_GetByCode(string Category_Code)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Category_GetByCode(Category_Code);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                List<Categories_Info> _lst = NaviCommon.CBO<Categories_Info>.FillCollectionFromDataSet(_ds);

                if (_lst.Count > 0)
                {
                    return _lst[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Categories_Info();
            }
        }

        public decimal Category_Delete(decimal Category_Id)
        {
            try
            {
                return CommonData.c_serviceWCF.Category_Delete(Category_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool Category_Insert(string Category_Code, string Category_Name, string Unit)
        {
            try
            {
                return CommonData.c_serviceWCF.Category_Insert(Category_Code, Category_Name, Unit);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Category_Update(decimal Category_Id, string Category_Name, string Unit)
        {
            try
            {
                return CommonData.c_serviceWCF.Category_Update(Category_Id, Category_Name, Unit);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

    }
}
