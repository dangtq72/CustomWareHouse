using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Business
{
    public class CommonData
    {
        static object objLockService_ = new object();
        static WareHoseService.CWContractServiceClient _serviceWCF;//service dùng chung cho cả hệ thống

        public static WareHoseService.CWContractServiceClient c_serviceWCF
        {
            get
            {
                try
                {
                    lock (objLockService_)
                    {

                        if (_serviceWCF == null)
                        {
                            _serviceWCF = new WareHoseService.CWContractServiceClient();
                            SetDefaultServiceConfig();
                        }
                        else if (_serviceWCF.State == System.ServiceModel.CommunicationState.Faulted)
                        {
                            _serviceWCF.Abort();
                            _serviceWCF = new WareHoseService.CWContractServiceClient();
                            SetDefaultServiceConfig();
                        }
                        else if (_serviceWCF.State == System.ServiceModel.CommunicationState.Closed)
                        {
                            _serviceWCF = new WareHoseService.CWContractServiceClient();
                            SetDefaultServiceConfig();
                        }

                        //mỗi lần gọi sẽ test kết nối, nếu kết nối ok thì sẽ set lại sendtimeout thành 10 phút
                        _serviceWCF.InnerChannel.OperationTimeout = TimeSpan.FromSeconds(30);
                        string s = _serviceWCF.AllCode_CheckWCF();
                        if (s == "OK")
                        {
                            _serviceWCF.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(10);
                        }
                        return _serviceWCF;
                    }

                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                    return new WareHoseService.CWContractServiceClient();
                }
            }
        }

        public static void RestartWCFService()
        {
            try
            {
                if (_serviceWCF == null)
                {
                    _serviceWCF = new WareHoseService.CWContractServiceClient();
                    SetDefaultServiceConfig();
                }
                else
                {
                    _serviceWCF.Abort();
                    _serviceWCF = new WareHoseService.CWContractServiceClient();
                    SetDefaultServiceConfig();
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        private static void SetDefaultServiceConfig()
        {
            try
            {
                _serviceWCF.InnerChannel.OperationTimeout = TimeSpan.FromSeconds(20);
                _serviceWCF.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(20);
                _serviceWCF.Endpoint.Binding.OpenTimeout = TimeSpan.FromMinutes(1);
                _serviceWCF.Endpoint.Binding.CloseTimeout = TimeSpan.FromSeconds(10);
            }
            catch
            {
            }
        }
    }
}
