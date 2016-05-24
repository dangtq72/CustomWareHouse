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
    public class AllCodeBL
    {
        public List<AllcodeInfo> AllCode_Gets_List()
        {
            try
            {

                byte[] byteRecive = CommonData.c_serviceWCF.Allcode_GetAll();
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<AllcodeInfo>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<AllcodeInfo>();
            }
        }

        public bool CheckWCF()
        {
            try
            {
                string s = CommonData.c_serviceWCF.AllCode_CheckWCF();

                if (s == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
