using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Text;
using ZetaCompressionLibrary;


namespace CW_Service
{
    [ServiceContract()]
    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] Allcode_GetAll();

        [OperationContract()]
        string AllCode_CheckWCF();

        [OperationContract()]
        DateTime Get_Current_Date();

    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                   ConcurrencyMode = ConcurrencyMode.Multiple,
                   UseSynchronizationContext = false)]

    public partial class CWService : CWContractService
    {

        public CWService()
        {
            //OperationContext context = OperationContext.Current;
            //MessageProperties messageProperties = context.IncomingMessageProperties;
            //RemoteEndpointMessageProperty endpointProperty =
            //    messageProperties[RemoteEndpointMessageProperty.Name]
            //    as RemoteEndpointMessageProperty;
        }

        public DateTime Get_Current_Date()
        {
            try
            {
                return DateTime.Now;
            }
            catch (Exception)
            {

                return DateTime.Now;
            }
        }


        public byte[] Allcode_GetAll()
        {
            try
            {
                byte[] byteReturn;
                AllCodeDA objAllCodeDA = new AllCodeDA();
                DataSet ds = objAllCodeDA.Allcode_GetAll();

                byteReturn = CompressionHelper.CompressDataSet(ds);
                return byteReturn;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public string AllCode_CheckWCF()
        {
            return "OK";
        }
        
    }
}
