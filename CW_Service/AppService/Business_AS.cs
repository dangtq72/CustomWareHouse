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
        byte[] BUSINESS_GET_ALL();

    }

    public partial class CWService : CWContractService
    {
        public byte[] BUSINESS_GET_ALL()
        {
            try
            {
                Business_DA _objDA = new Business_DA();
                DataSet _ds = _objDA.BUSINESS_GET_ALL();
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
