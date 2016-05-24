using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;

namespace NaviCommon
{
    public class Common
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static char SOH = (char)0x01;

        /// <summary>
        /// MyQUuer Luu insert thong tke truy cap
        /// </summary>
        public static MyQueue cQueueTKTC = new MyQueue();

        /// <summary>
        ///so ban ghi /1 trang 
        /// /// </summary>
        public static readonly int RecordOnpage = 10;

        /// <summary>
        /// Luu trang thai ket noi toi services provideData
        /// </summary>
        public static bool ConnectedWCF = false;

        public static object LockListFunctWhenChangeData = new object();

    }

    public class NVS_FTP
    {
        // liên quan tới việc upload file
        public static string User_FTP = "";
        public static string Pass_FTP = "";
        public static string User_FTP_Local = "";
        public static string Pass_FTP_Local = "";

        /// <summary>
        /// Với FTP không cần đăng nhập user Password
        /// </summary>
        public static string GUser_FTP = "";
        public static string GPass_FTP = "";
    }

    public class UrlHelper
    {

        ///// <summary>
        ///// chuyển về kiểu url xóa bỏ toàn bộ tiếng việt và các ký tự đặc beiets
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>

        /// <summary>chuyển về kiểu url xóa bỏ toàn bộ tiếng việt và các ký tự đặc beiets
        /// Build Url 
        /// </summary>
        /// <param name="pArticlesType">Loại tin </param>
        /// <param name="pArticlesID">ID bài viết</param>
        /// <param name="pMemID">ID MemberID đối với tin thành viên ,StockID với tin TCPH</param>
        /// <param name="pStockID"> StockID với tin TCPH</param>
        /// <param name="PageRouter">Định tuyến trang </param>
        /// <param name="str">Title</param>
        /// <returns>Url đã đúng định dạng</returns>
        public static String buildUrl(int pArticlesType, int pArticlesID, int pMemID, int pStockID, string PageRouter, string str)
        {
            try
            {
                int pMemStockID = 0;
                if (pStockID > 0)
                {
                    pMemStockID = pStockID;
                }
                else if (pMemID > 0)
                {
                    pMemStockID = pMemID;
                }
                string url = "";
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = str.Normalize(NormalizationForm.FormD);
                temp = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                string partern = "~!@#$%^&*()_+|{}[]:'><?\"/,.;”“";
                for (int i = 0; i < partern.Length; i++)
                {
                    temp = temp.Replace(partern[i].ToString(), "");
                }
                while (temp.IndexOf("  ") > 0)
                {
                    temp = temp.Replace("  ", " ");
                }
                if (temp.Contains('–'))
                {
                    temp.Replace("–", "");
                }
                temp = temp.Trim().Replace(' ', '-');
                if (temp.Contains("–"))
                {
                    temp = temp.Remove(temp.IndexOf("–"), 1);
                }
                while (temp.IndexOf("--") > 0)
                {
                    temp = temp.Replace("--", "-");
                }
                if (temp.Length > 150)
                {
                    temp = temp.Substring(0, 150);
                }
                url = pArticlesType.ToString() + "-" + PageRouter + "-" + pArticlesID.ToString() + "/" + pMemStockID.ToString() + "/" + temp + ".htm";
                return url;
            }
            catch
            {
                return "chi-tiet-tin.htm";
            }
        }
        /// <summary>Replace Unicode Url
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String ReplaceUnicodeUrl(string str)
        {
            try
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = str.Normalize(NormalizationForm.FormD);
                temp = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                string partern = "~!@#$%^&*()_+|{}[]:'><?\"/,.;”“";
                for (int i = 0; i < partern.Length; i++)
                {
                    temp = temp.Replace(partern[i].ToString(), "");
                }
                while (temp.IndexOf("  ") > 0)
                {
                    temp = temp.Replace("  ", " ");
                }
                if (temp.Contains('–'))
                {
                    temp.Replace("–", "");
                }
                temp = temp.Trim().Replace(' ', '-');
                if (temp.Contains("–"))
                {
                    temp = temp.Remove(temp.IndexOf("–"), 1);
                }
                while (temp.IndexOf("--") > 0)
                {
                    temp = temp.Replace("--", "-");
                }
                //DongHT: Lấy độ dài 100 thôi
                if (temp.Length > 100)
                {
                    temp = temp.Substring(0, 100);
                }
                return temp;
            }
            catch
            {
                return "";
            }
        }
    }

    public class Config_Info
    {
        public static int c_Length_Opt = 10;
        public static int c_Length_Password = 8;
        public static int c_Time_Otp = 5;// thời gian hiệu lực của mã otp
        public static int c_Time_Load_Warning = 10000;// thời gian load thông tin cảnh báo 10s

        public static string c_file_Upload = "/File_Upload/";                           // đường dẫn lưu file upload
        public static string c_file_Upload_Content = "/File_Upload/Content/";           // đường dẫn lưu file nội dung
        public static string c_file_Upload_Result = "/File_Upload/Result/";             // đường dẫn lưu file nghị quyết
        public static string c_file_Upload_Invite = "/File_Upload/Invite/";             // đường dẫn lưu file mời họp
        public static string c_file_Upload_Auz = "/File_Upload/Auz/";                   // đường dẫn lưu file ủy quyền
        public static string c_file_Upload_Suggest_Auz = "/File_Upload/Suggest_Auz/";   // đường dẫn lưu file gợi ý ủy quyền

        public static string c_file_Upload_Sync = "/File_Upload/Sync/";                 // đường dẫn lưu file đồng bộ
        public static string c_file_ChangeUserInfo = "/Change_User_Info/";              // đường dẫn lưu file thay doi thong tin ca nha cua khach hang
        public static string c_file_ChangeUserInfo_BK = "/Change_User_Info_BackUp/";    // đường dẫn lưu file thay doi thong tin ca nha cua khach hang (backup)
        public static string c_file_DownloadTem = "/Content/FlexcelExportFile/";        // file tem report
        public static string c_file_Email_Sample = "/Template_Email/";
        public static int c_TimeOut_OTP = 5;                                            // thời gian delay mã otp 5 phút

        public static int TimeReloadVotingStatus = 10000;                               //thời gian gửi request lên server để lấy trạng thái realtime voting/ miliseconds

        public static int TimeReGetOTP = 5000;                                          //thời gian gửi request lên server để lấy mã OTP 5s

        public static int NUMBER_DATE_CHANGE_PASS = 30;                                 // so ngày bắt buộc phải đổi mật khẩu
        public static string c_FORDER_PUBLIC_ADMIN = "FORDER_PUBLIC_ADMIN";             // forder public admin
        public static string c_FORDER_PUBLIC_PORTAL = "FORDER_PUBLIC_PORTAL";             // forder public portal

        public static decimal c_numberLockUser = 5;                                     // số lần đăng nhập sai

        #region sử dụng cho thư mời
        public static string c_IMG_SEND_EMAIL = ""; //Ảnh trên Email
        public static string c_Phong_VSD = "";
        public static string c_Phone_VSD = "";
        public static string c_Website_Vote = "";
        #endregion

        public static int c_ErrorDefSuccess = 200;                                  // trả ra ok khi gửi sms

    }

    public class SystemParaKey
    {

        public static string c_dic_USER_WAREHOUSE = "USER_WAREHOUSE";
        public static string c_dic_USER_OF_WAREHOUSE = "USER_OF_WAREHOUSE";

        public static string c_NUMBER_DATE_CHANGE_PASS = "NUMBER_DATE_CHANGE_PASS";

        public static string c_numberLockUser = "NUMBER_LOCK_USER";                                 
        public static string c_numer_Minute_Lock = "NUMER_MINUTE_LOCK";

        public static string c_ErrorDefSuccess = "Error_Def_Success";

    }

}
