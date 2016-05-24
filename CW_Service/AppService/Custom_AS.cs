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
        byte[] Custom_GetAll();

        [OperationContract()]
        decimal Custom_Insert(string Custom_Name, string Location, string Note);

        [OperationContract()]
        decimal Custom_Delete(decimal Custom_Id);
    }

    public partial class CWService : CWContractService
    {
        public byte[] Custom_GetAll()
        {
            try
            {
                Custom_DA _objDA = new Custom_DA();
                DataSet _ds = _objDA.Custom_GetAll();
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public decimal Custom_Insert(string Custom_Name, string Location, string Note)
        {
            try
            {
                Custom_DA _objDA = new Custom_DA();
                return _objDA.Custom_Insert(Custom_Name,Location,Note);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal Custom_Delete(decimal Custom_Id)
        {
            try
            {
                Custom_DA _objDA = new Custom_DA();
                return _objDA.Custom_Delete(Custom_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

    }
}
