using CW_Business;
using CW_Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CustomWarehouse
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();

                // get datamem
                GetDataMem();

                DataMemory.Get_Function_ByUser();

                AreaRegistration.RegisterAllAreas();

                WebApiConfig.Register(GlobalConfiguration.Configuration);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);

                //thread dịnh kỳ kiểm tra kết nối tới services 
                Thread _threadCheckConnectWCF = new Thread(CheckConnectToWCFProvideData) { IsBackground = true };
                _threadCheckConnectWCF.Start();

                NaviCommon.Common.log.Info("----------Start Succeess------------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "  ------------------------");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Create:Dangtq
        /// Ham thưc kiện kiểm tra kết nối tới services gắn lại timeout cho services 
        /// </summary>
        void CheckConnectToWCFProvideData()
        {
            try
            {
                while (true)
                {
                    AllCodeBL _AllCodeBL = new AllCodeBL();
                    NaviCommon.Common.ConnectedWCF = _AllCodeBL.CheckWCF();

                    if (NaviCommon.Common.ConnectedWCF == false)
                    {
                        NaviCommon.Common.log.Error("Loi ket noi toi Service");
                    }

                    Thread.Sleep(3000);//3s chekc 1 lan 
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                NaviCommon.Common.ConnectedWCF = false;
            }
        }

        void GetDataMem()
        {
            try
            {
                #region Allcode
                DataMemory.c_hs_Allcode.Clear();

                AllCodeBL _AllCodeBL = new AllCodeBL();
                List<AllcodeInfo> _lst_al = _AllCodeBL.AllCode_Gets_List();

                if (_lst_al.Count > 0)
                {
                    foreach (AllcodeInfo item in _lst_al)
                    {
                        if (DataMemory.c_hs_Allcode.ContainsKey(item.CdName + "|" + item.CdType) == false)
                        {
                            List<AllcodeInfo> _lst = new List<AllcodeInfo>();
                            _lst.Add(item);
                            DataMemory.c_hs_Allcode[item.CdName + "|" + item.CdType] = _lst;
                        }
                        else
                        {
                            List<AllcodeInfo> _lst = (List<AllcodeInfo>)DataMemory.c_hs_Allcode[item.CdName + "|" + item.CdType];
                            _lst.Add(item);
                        }
                    }
                }
                #endregion

                #region Custom

                DataMemory.Get_ListCustom();
                #endregion
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }
    }
}