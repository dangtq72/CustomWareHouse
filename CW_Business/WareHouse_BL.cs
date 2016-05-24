using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZetaCompressionLibrary;

namespace CW_Business
{
   public class WareHouse_BL
    {
       public static List<WareHouse_Info> WareHouse_GetAll()
       {
           try
           {
               byte[] byteRecive = CommonData.c_serviceWCF.WAREHOUSE_GET_ALL();
               DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
               return NaviCommon.CBO<WareHouse_Info>.FillCollectionFromDataSet(_ds);
           }
           catch (Exception ex)
           {
               NaviCommon.Common.log.Error(ex.ToString());
               return new List<WareHouse_Info>();
           }
       }

        public List<WareHouse_Info> WareHouse_Search(string p_name)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.WareHouse_Search(p_name);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<WareHouse_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<WareHouse_Info>();
            }
        }

        public WareHouse_Info WareHouse_GetById(decimal WareHouse_Id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.WareHouse_GetById(WareHouse_Id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                List<WareHouse_Info> _lst = NaviCommon.CBO<WareHouse_Info>.FillCollectionFromDataSet(_ds);

                if (_lst.Count > 0)
                {
                    return _lst[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new WareHouse_Info();
            }
        }

        public static List<WareHouse_Info> WareHouse_GetByUser(decimal User_Id, decimal User_Type)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.WareHouse_GetByUser(User_Id, User_Type);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                return NaviCommon.CBO<WareHouse_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<WareHouse_Info>();
            }
        }


        public decimal WareHouse_Delete(decimal WareHouse_Id)
        {
            try
            {
                return CommonData.c_serviceWCF.WareHouse_Delete(WareHouse_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal WareHouse_Insert(string WareHouse_Code, string WareHouse_Name, string Location)
        {
            try
            {
                return CommonData.c_serviceWCF.WareHouse_Insert(WareHouse_Code, WareHouse_Name, Location);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool WareHouse_Update(decimal WareHouse_Id, string WareHouse_Name, string Location)
        {
            try
            {
                return CommonData.c_serviceWCF.Category_Update(WareHouse_Id, WareHouse_Name, Location);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
