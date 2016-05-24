using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CW_Service
{
    public class ReceiveSMS
    {
        public int OnReceiveStart( string strFrPhone , string strContent )
        {
            int _result = 0;
            try
            {
                // Code goi ham nhan message o day
                return _result;
            }
            catch (Exception ex)
            {
              Common.log.Error (ex.ToString ());
                return -1;
            }
        
        }
    }
}
