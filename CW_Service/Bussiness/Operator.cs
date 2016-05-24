using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CW_Info;

namespace CW_Service
{
    public class Operator
    {
        public void ThreadSendAlert_Expiredate()
        {
            while (true)
            {
                try
                {
                    Process_GetAndSendAlert();

                    // 1h chạy 1 lần
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(5000);
                    Common.log.Error(ex.ToString());
                }
            }
        }

        void Process_GetAndSendAlert()
        {
            try
            {
                Declaration_DA _Declaration_DA = new Declaration_DA();
                DataSet _ds = _Declaration_DA.Declaration_GetExpireDate();

                List<Declaration_Info> _lst = NaviCommon.CBO<Declaration_Info>.FillCollectionFromDataSet(_ds);

                SendSMS _SendSMS = new SendSMS();

                foreach (Declaration_Info item in _lst)
                {
                    if (item.Phone == null || item.Phone == "")
                        continue;
                    _SendSMS.Send(item.Phone, "Hop dong thue kho so " + item.Contract_Code + " cua quy khach da qua han. Vui long lien he voi chu kho");
                }
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
            }
        }
    }
}
