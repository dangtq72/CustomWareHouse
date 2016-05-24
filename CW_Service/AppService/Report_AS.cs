using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ZetaCompressionLibrary;

namespace CW_Service
{

    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] Get_TonKho(decimal p_warehouse_id);

        
    }

    public partial class CWService : CWContractService
    {
        public byte[] Get_TonKho(decimal p_warehouse_id)
        {
            try
            {
                Report_DA _objDA = new Report_DA();
                DataSet _ds = _objDA.Get_TonKho(p_warehouse_id);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

       
    }
}
