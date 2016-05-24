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
    public class Report_BL
    {
        public List<TonKho_Info> Get_TonKho(decimal p_warehouse_id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Get_TonKho(p_warehouse_id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                return NaviCommon.CBO<TonKho_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<TonKho_Info>();
            }
        }

    }
}
