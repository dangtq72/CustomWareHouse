using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using ZetaCompressionLibrary;

namespace CW_Business
{
    public class Custom_BL
    {
        public List<Custom_Info> Custom_GetAll()
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Custom_GetAll();
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Custom_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Custom_Info>();
            }
        }

        public decimal Custom_Delete(decimal Custom_Id)
        {
            try
            {
                return CommonData.c_serviceWCF.Custom_Delete(Custom_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal Custom_Insert(string Custom_Name, string Location, string Note)
        {
            try
            {
                return CommonData.c_serviceWCF.Custom_Insert(Custom_Name, Location, Note);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

    }
}
