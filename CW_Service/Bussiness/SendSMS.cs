using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CW_Service
{
    public class SendSMS
    {
        public int Send(string strPhone, string strMessage)
        {
            int _Resutl = 0;
            try
            {
                if (SMS_Config_Info.ConfigSMS == "ESMS")
                {
                    esmsAPI _send = new esmsAPI();
                    _Resutl = _send.SendSMS(strPhone, strMessage);

                    //100: Request thành công
                    //99 Lỗi không xác định , thử lại sau
                    //101 Đăng nhập thất bại (api key hoặc secrect key không đúng )
                    //102 Tài khoản đã bị khóa
                    //103 Số dư tài khoản không đủ dể gửi tin
                    //104 Mã Brandname không đúng
                    if (!_Resutl.Equals(100))
                    {
                        Common.log.Error("SendSMS to server error: " + strPhone + ". Ma loi " + _Resutl.ToString());
                        _Resutl = -1;
                    }
                    else
                    {
                        Common.log.Info("SendSMS: " + strPhone + " : " + strMessage);
                    }
                }

                return _Resutl;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
