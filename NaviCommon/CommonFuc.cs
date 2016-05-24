using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace NaviCommon
{
    public class CommonFuc
    {

        public const string strDate = "dd/MM/yyyy";
        private static readonly Random rand = new Random();
        /// <summary>
        /// Hàm check ký tự đặc biệt
        /// </summary>
        /// <param name="pStrContent">Nội dung chuối truyền vào check </param>
        /// <returns>True : False</returns>
        public static bool CheckSpecialCharactor(string pStrContent)
        {
            try
            {
                if (pStrContent.ToString().Contains("@") == true || pStrContent.ToString().Contains("#") == true || pStrContent.ToString().Contains("!") == true
                    || pStrContent.ToString().Contains("$") == true || pStrContent.ToString().Contains("%") == true || pStrContent.ToString().Contains("^") == true
                    || pStrContent.ToString().Contains("&") == true || pStrContent.ToString().Contains("*") == true || pStrContent.ToString().Contains("(") == true
                    || pStrContent.ToString().Contains(")") == true || pStrContent.ToString().Contains("_") == true
                    || pStrContent.ToString().Contains("=") == true || pStrContent.ToString().Contains("+") == true || pStrContent.ToString().Contains("\\") == true
                    || pStrContent.ToString().Contains("|") == true || pStrContent.ToString().Contains("`") == true || pStrContent.ToString().Contains("~") == true
                    || pStrContent.ToString().Contains("<") == true || pStrContent.ToString().Contains(">") == true || pStrContent.ToString().Contains("?") == true
                    || pStrContent.ToString().Contains("/") == true || pStrContent.ToString().Contains(".") == true || pStrContent.ToString().Contains("{") == true
                    || pStrContent.ToString().Contains("}") == true || pStrContent.ToString().Contains("[") == true || pStrContent.ToString().Contains("]") == true
                    || pStrContent.ToString().Contains(";") == true || pStrContent.ToString().Contains(":") == true || pStrContent.ToString().Contains(",") == true
                    || pStrContent.ToString().Contains(Convert.ToString('"')) == true || pStrContent.ToString().Contains("'") == true)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString()); return false;
            }
        }

        /// <summary>
        ///     xóa hết kí tự đặc biệt
        /// </summary>
        /// <param name="pStrContent">Chuỗi cần format</param>
        /// <returns></returns>
        public static string RemoveSpecialCharacter(string pStrContent)
        {
            try
            {
                string str_result = "";
                str_result = pStrContent.Replace("@", "");
                str_result = str_result.Replace("#", "");
                str_result = str_result.Replace("!", "");
                str_result = str_result.Replace("$", "");
                str_result = str_result.Replace("%", "");
                str_result = str_result.Replace("^", "");
                str_result = str_result.Replace("&", "");
                str_result = str_result.Replace("*", "");
                str_result = str_result.Replace("(", "");
                str_result = str_result.Replace(")", "");
                str_result = str_result.Replace("_", "");
                str_result = str_result.Replace("=", "");
                str_result = str_result.Replace("-", "");
                str_result = str_result.Replace("+", "");
                str_result = str_result.Replace("\\", "");
                str_result = str_result.Replace("`", "");
                str_result = str_result.Replace("~", "");
                str_result = str_result.Replace("<", "");
                str_result = str_result.Replace(">", "");
                str_result = str_result.Replace("?", "");
                str_result = str_result.Replace("/", "");
                str_result = str_result.Replace(".", "");
                str_result = str_result.Replace("{", "");
                str_result = str_result.Replace("}", "");
                str_result = str_result.Replace("[", "");
                str_result = str_result.Replace("]", "");
                str_result = str_result.Replace(";", "");
                str_result = str_result.Replace(":", "");
                str_result = str_result.Replace(",", "");
                str_result = str_result.Replace(Convert.ToString('"'), "");
                str_result = str_result.Replace("'", "");
                str_result = str_result.Replace(" ", "");
                return UrlHelper.ReplaceUnicodeUrl(str_result);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// Kiểm tra ký tự đặc biệt
        /// </summary>
        /// <param name="strnewAcc"></param>
        /// <returns></returns>
        public static bool checkCharacterAZ09(string strnewAcc)
        {
            try
            {
                bool _ck = false;
                int counter = 0;
                int convertedPw;
                CharEnumerator charEnum = strnewAcc.GetEnumerator();
                while (charEnum.MoveNext())
                {
                    convertedPw = Convert.ToInt32(strnewAcc[counter]);

                    if ((convertedPw >= 65) && (convertedPw <= 90))
                    {
                        //ky tu hoa
                        _ck = true;
                    }
                    else if ((convertedPw >= 97) && (convertedPw <= 122))
                    {
                        //ky tu thuong 
                        _ck = true;
                    }
                    else if ((convertedPw >= 48) && (convertedPw <= 57))
                    {
                        //ky tu so
                        _ck = true;
                    }
                    else
                    {
                        _ck = false;
                        break;
                    }
                    counter++;
                }

                return _ck;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool checkCharacterAZ09_For_Symbol(string strnewAcc)
        {
            try
            {
                bool _ck = false;
                int counter = 0;
                int convertedPw;
                CharEnumerator charEnum = strnewAcc.GetEnumerator();
                while (charEnum.MoveNext())
                {
                    convertedPw = Convert.ToInt32(strnewAcc[counter]);

                    if ((convertedPw >= 65) && (convertedPw <= 90))
                    {
                        //ky tu hoa
                        _ck = true;
                    }
                    else if ((convertedPw >= 97) && (convertedPw <= 122))
                    {
                        //ky tu thuong 
                        _ck = true;
                    }
                    else if ((convertedPw >= 48) && (convertedPw <= 57))
                    {
                        //ky tu so
                        _ck = true;
                    }
                    else if (convertedPw == 44 || convertedPw == 32)// Dấu , và ' '
                    {
                        _ck = true;
                    }
                    else {
                        _ck = false;
                        break;
                    }
                    counter++;
                }

                return _ck;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Chuyển kiểu Date thành kiểu chuỗi
        /// </summary>
        /// <param name="p_date">Kiểu Datetime truyền vào</param>
        /// <returns>Chuoi ký tự theo định dạng </returns>
        public static string ConvertDate2String(DateTime p_date)
        {
            try
            {
                return p_date.ToString(strDate);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// Convert String To Date 
        /// </summary>
        /// <param name="pStr">Chuoi truyền vào chuyển sang dạng DateTime</param>
        /// <returns></returns>
        public static DateTime ConvertString2Date(string pStr)
        {
            string DayMontYear = "";
            pStr = pStr.Trim();
            if (!string.IsNullOrEmpty(pStr))
            {
                if (pStr.Length != 10)
                {

                    if (pStr.Length > 10)
                    {
                        string[] arr_date_time = pStr.Split(' ');
                        if (arr_date_time.Length > 0)
                            pStr = arr_date_time[0];
                    }

                    string[] arrDate = pStr.Split('/');
                    if (arrDate.Length == 3)
                    {
                        if (arrDate[0].Length == 1)
                        {
                            DayMontYear += "0" + arrDate[0] + "/";
                        }
                        else
                        {
                            DayMontYear += arrDate[0] + "/";
                        }

                        if (arrDate[1].Length == 1)
                        {
                            DayMontYear += "0" + arrDate[1];
                        }
                        else
                        {
                            DayMontYear += arrDate[1];
                        }

                        DayMontYear = DayMontYear + "/" + arrDate[2];

                    }
                }
                else
                {
                    DayMontYear = pStr;
                }
            }
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                return DateTime.ParseExact(DayMontYear, strDate, provider);
            }
            catch (Exception ex)
            {
                Common.log.Error("Loi convert ngay thang truyen vao la :" + DayMontYear + ex.ToString());
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Chuyển từ kiểu String -> Format kiểu tiền tệ
        /// </summary>
        /// <param name="str">Chuỗi cần convert</param>
        /// <returns></returns>
        public static String ConvertString2Money(string pStr)
        {
            try
            {
                if (pStr != "")
                {
                    return String.Format("{0:#,###}", pStr);
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "0";
            }
        }

        public static String FormatNumber(decimal pStr)
        {
            try
            {
                if (pStr != 0)
                {
                    return String.Format("{0:#,###}", pStr);
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "0";
            }
        }

        /// <summary>
        /// Hàm làm tròn lên số double 1.2 ->2 ..
        /// </summary>
        /// <param name="pDoubleNum">Giá trị kiểu double </param>
        /// <returns>trả về kiểu Integer với giá trị làm tròn lên </returns>
        public static int RoundUp(double pDoubleNum)
        {
            try
            {
                return Convert.ToInt32(Math.Ceiling(pDoubleNum).ToString());
            }
            catch (Exception ex)
            {
                Common.log.Error("Loi Double : " + pDoubleNum.ToString() + ex.ToString());
                return -1;
            }
        }

        public static string Encrypt(string toEncrypt)
        {
            try
            {
                var md5Hasher = new MD5CryptoServiceProvider();
                byte[] hashedBytes;
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(toEncrypt));
                StringBuilder s = new StringBuilder();
                foreach (byte _hashedByte in hashedBytes)
                {
                    s.Append(_hashedByte.ToString("x2"));
                }
                return s.ToString();
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// Sangdd 
        /// Hàm lấy ngày giờ hiện tại lưu vào db
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentDate()
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                //string IFormatDate = "dd/MM/yyyy HH:mm tt"; //khoong lay giay 
                string IFormatDate = "dd/MM/yyyy HH:mm:ss";
                string pStr = DateTime.Now.ToString(IFormatDate);
                return DateTime.ParseExact(pStr, IFormatDate, provider);

            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Lấy ngày tháng duy nhất 
        /// </summary>
        /// <returns></returns>
        public static DateTime TruncDate()
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                string IFormatDate = "dd/MM/yyyy";
                string pStr = DateTime.Now.ToString(IFormatDate);
                return DateTime.ParseExact(pStr, IFormatDate, provider);

            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Lay ten luu lai anh cho do bi trung ten khi tai anh len tu client
        /// </summary>
        /// <returns></returns>
        public static string GetImageNames()
        {
            try
            {
                string IFormatDate = "ddMMyyyyhhmmss";
                string pStr = DateTime.Now.ToString(IFormatDate);
                return pStr;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return DateTime.Now.ToString("ddMMyyyyhhmmss");
            }
        }

        /// Check xem có phải là kiểu decimal không ?
        /// Sangdd 
        /// </summary>
        /// <param name="pStr">Chuỗi nhập vào</param>
        /// <returns>True Flase</returns>
        public static bool CheckDecimal(string pStr)
        {
            decimal Num;
            bool isNum = decimal.TryParse(pStr, out Num);
            if (isNum)
                return true;
            else
                return false;
        }

        /// Check xem pStr Có phải là kiểu int hay không 
        /// Sangdd
        /// </summary>
        /// <param name="pStr">Chuỗi nhập vào</param>
        /// <returns>True Flase</returns>
        public static bool CheckInt(string pStr)
        {
            Int64 Num;
            Int64.TryParse(pStr, out Num);
            if (Num > 0 || pStr.Equals("0"))
                return true;
            else
                return false;
        }


        /// Validate kiểu chuối nhập vào có phải định dạng kiểu Email hay không 
        /// Sangdd 
        /// </summary>
        /// <param name="strEmail">Chuỗi nhập vào</param>
        /// <returns>True, False</returns>
        public static bool checkValidEmail(string strEmail)
        {
            try
            {
                string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex rx = new Regex(pattern, RegexOptions.IgnoreCase);
                if (!rx.IsMatch(strEmail))
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// truyền vào ngày tháng trả ra thứ mấy 
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns></returns>
        public static string GetDayOfWeek(DateTime pDate, string pLanguage)
        {
            try
            {
                string pStr = "";
                int day = Convert.ToInt32(pDate.DayOfWeek);
                if (!string.IsNullOrEmpty(pLanguage))
                {
                    if (pLanguage.ToUpper() == "VI")
                    {
                        if (day == 1)
                        {
                            pStr = "Thứ hai";
                        }
                        else if (day == 2)
                        {
                            pStr = "Thứ ba";
                        }
                        else if (day == 3)
                        {
                            pStr = "Thứ tư";
                        }
                        else if (day == 4)
                        {
                            pStr = "Thứ năm";
                        }
                        else if (day == 5)
                        {
                            pStr = "Thứ sáu";
                        }
                        else if (day == 6)
                        {
                            pStr = "Thứ bẩy";
                        }
                        else if (day == 0)
                        {
                            pStr = "Chủ nhật";
                        }
                    }
                    else
                    {
                        if (day == 1)
                        {
                            pStr = "Monday";
                        }
                        else if (day == 2)
                        {
                            pStr = "Tuesday";
                        }
                        else if (day == 3)
                        {
                            pStr = "Wednesday";
                        }
                        else if (day == 4)
                        {
                            pStr = "Thursday";
                        }
                        else if (day == 5)
                        {
                            pStr = "Friday";
                        }
                        else if (day == 6)
                        {
                            pStr = "Saturday";
                        }
                        else if (day == 0)
                        {
                            pStr = "Sunday";
                        }
                    }
                }
                else
                {
                    if (day == 1)
                    {
                        pStr = "Monday";
                    }
                    else if (day == 2)
                    {
                        pStr = "Tuesday";
                    }
                    else if (day == 3)
                    {
                        pStr = "Wednesday";
                    }
                    else if (day == 4)
                    {
                        pStr = "Thursday";
                    }
                    else if (day == 5)
                    {
                        pStr = "Friday";
                    }
                    else if (day == 6)
                    {
                        pStr = "Saturday";
                    }
                    else if (day == 0)
                    {
                        pStr = "Sunday";
                    }
                }

                return pStr;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "";
            }
        }


        public static string DateTimeOnPortal(DateTime pDate)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                string IFormatDate = "dd/MM/yyyy HH:mm:ss";
                string date = pDate.ToString(IFormatDate).ToString();
                return date;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return DateTime.MinValue.ToString();
            }
        }

        //HƯNG TD: sửa 18/01/2016
        //public string Paging(int iCurrentPage, int iRowSize, int iRowTotal, string strFunction)
        //{
        //    string strResult = "";
        //    strResult += "<ul>";
        //    if (iCurrentPage > 1)
        //    {
        //        strResult += "<li onclick=\"" + strFunction + "(1)\">&laquo;&laquo;</li>";
        //        strResult += "<li onclick=\"" + strFunction + "(" + (iCurrentPage - 1) + ")\">Trước</li>";
        //    }
        //    if (iRowTotal > 1)
        //    {
        //        if (iCurrentPage <= iRowTotal)
        //        {
        //            // NamTD thêm 1 trang sau trang hiện tại nữa
        //            if (iCurrentPage > 2)
        //            {
        //                strResult += "<li onclick=\"" + strFunction + "(" + (iCurrentPage - 2) + ")\">" + (iCurrentPage - 2) + "</li>";
        //            }
        //            // End NamTD
        //            if (iCurrentPage > 1)
        //            {
        //                strResult += "<li onclick=\"" + strFunction + "(" + (iCurrentPage - 1) + ")\">" + (iCurrentPage - 1) + "</li>";
        //                strResult += "<li class=\"pagin-active\" onclick=\"" + strFunction + "(" + (iCurrentPage) + ")\"\">" + (iCurrentPage) + "</li>";

        //                if (iCurrentPage >= 2 && iCurrentPage < iRowTotal)
        //                {
        //                    strResult += "<li onclick=\"" + strFunction + "(" + (iCurrentPage + 1) + ")\">" + (iCurrentPage + 1) + "</li>";
        //                }
        //            }
        //            else if (iCurrentPage == 1)
        //            {
        //                strResult += "<li class=\"pagin-active\" onclick=\"" + strFunction + "(" + iCurrentPage + ")\">" + (iCurrentPage) + "</li>";
        //                if (iRowTotal > 1) { strResult += "<li onclick=\"" + strFunction + "(" + (iCurrentPage + 1) + ")\">" + (iCurrentPage + 1) + "</li>"; }
        //                if (iRowTotal > 2) { strResult += "<li onclick=\"" + strFunction + "(" + (iCurrentPage + 2) + ")\">" + (iCurrentPage + 2) + "</li>"; }
        //            }
        //            if (iRowTotal > 3)
        //            {
        //                //.....
        //                strResult += "<li>...</li>";

        //                //2 row cuoi
        //                strResult += "<li onclick=\"" + strFunction + "(" + (iRowTotal - 1) + ")\">" + (iRowTotal - 1) + "</li>";
        //                strResult += "<li onclick=\"" + strFunction + "(" + (iRowTotal) + ")\">" + iRowTotal + "</li>";
        //            }
        //        }
        //    }
        //    if ((iCurrentPage == iRowTotal - 2 || iCurrentPage == iRowTotal - 1 || iCurrentPage == iRowTotal) && iRowTotal > 3)
        //    {
        //        strResult = "<ul>";
        //        if (iCurrentPage > 1)
        //        {
        //            strResult += "<li onclick=\"" + strFunction + "(1)\">&laquo;&laquo;</li>";
        //            strResult += "<li onclick=\"" + strFunction + "(" + (iCurrentPage - 1) + ")\">Trước</li>";
        //        }

        //        strResult += "<li onclick=\"" + strFunction + "(1)\">" + 1 + "</li>";
        //        strResult += "<li onclick=\"" + strFunction + "(2)\">" + 2 + "</li>";

        //        //.....
        //        strResult += "<li>...</li>";

        //        //3 row cuoi
        //        if (iCurrentPage == iRowTotal - 2) { strResult += "<li class=\"pagin-active\" onclick=\"" + strFunction + "(" + iCurrentPage + ")\">" + (iCurrentPage) + "</li>"; }
        //        else { strResult += "<li onclick=\"" + strFunction + "(" + (iRowTotal - 2) + ")\">" + (iRowTotal - 2) + "</li>"; }

        //        if (iCurrentPage == iRowTotal - 1)
        //        {
        //            strResult += "<li class=\"pagin-active\" onclick=\"" + strFunction + "(" + iCurrentPage + ")\">" + (iCurrentPage) + "</li>";
        //        }
        //        else { strResult += "<li onclick=\"" + strFunction + "(" + (iRowTotal - 1) + ")\">" + (iRowTotal - 1) + "</li>"; }

        //        if (iCurrentPage == iRowTotal)
        //        {
        //            strResult += "<li class=\"pagin-active\" onclick=\"" + strFunction + "(" + iCurrentPage + ")\">" + (iCurrentPage) + "</a></li>";
        //        }
        //        else { strResult += "<li onclick=\"" + strFunction + "(" + iRowTotal + ")\">" + iRowTotal + "</a></li>"; }
        //    }
        //    if (iCurrentPage < iRowTotal)
        //    {
        //        strResult += "<li onclick=\"" + strFunction + "(" + (iCurrentPage + 1) + ")\">Sau</a></li>";
        //        strResult += "<li onclick=\"" + strFunction + "(" + iRowTotal + ")\">&raquo;&raquo;</a></li>";
        //    }
        //    strResult += "</ul>";
        //    return strResult;
        //}
        /// <summary>
        /// HungTD thêm: 18/01/2016
        /// </summary>
        /// <param name="pCurPage"></param>
        /// <param name="pRecordOnPage"></param>
        /// <param name="pTotalRecord"></param>
        /// <param name="p_js_function"></param>
        /// <param name="_str_loai_ban_ghi"></param>
        /// <returns></returns>
        public static string Paging(int pCurPage, int pRecordOnPage, int pTotalRecord, string p_js_function = "", string _str_loai_ban_ghi = "")
        {
            try
            {
                if (_str_loai_ban_ghi == "")
                {
                    _str_loai_ban_ghi = "Bản ghi";
                }
                if (p_js_function == "")
                {
                    p_js_function = "page";
                }
                string pStrPaging = "";
                double _dobTotalRec = Convert.ToDouble(pTotalRecord);
                int _TotalPage = NaviCommon.CommonFuc.RoundUp(_dobTotalRec / pRecordOnPage);
                pStrPaging = WritePaging(_TotalPage, pCurPage, pTotalRecord, pRecordOnPage, _str_loai_ban_ghi, p_js_function);
                return pStrPaging;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "";
            }
        }

        public static string WritePaging(int iPageCount, int iCurrentPage, int iTotalRecords, int iPageSize, string pLoaiBanGhi, string p_jsfuncion = "")
        {
            try
            {
                if (p_jsfuncion == "")
                {
                    p_jsfuncion = "page";
                }
                string strPage = "";
                strPage += "<div id='d_page'>";
                // strPage += "<div id='d_total_rec'>" + "Có tổng " + iTotalRecords + " " + pLoaiBanGhi + "</div>";
                strPage += "<div id='d_total_rec'>" + "Có tổng " + iTotalRecords + " " + pLoaiBanGhi + " </div>";
                strPage += "<div id='d_number_of_page'>";
                if (iPageCount <= 1) return strPage;
                if (iCurrentPage > 1)
                {
                    //sangdd moi them dong 1 trở về trang đầu
                    strPage += "<li onclick=\"" + p_jsfuncion + "(1)\"><span id=\"first\"  href=\"\"><<</span></li>";

                    strPage += "<li onclick=\"" + p_jsfuncion + "('" + (iCurrentPage - 1) + "')\"><span id=\"back\"  href=\"\"><</span></li>";
                }
                if (iPageCount <= 5)
                {
                    for (int j = 0; j < iPageCount; j++)
                    {
                        if (iCurrentPage == (j + 1))
                        {
                            //HungTD rem doan nay
                            strPage += "<li style=\"background-color: #CDCDCD;\" onclick=\"" + p_jsfuncion + "(" + (j + 1) + ")\"><span class=\"a-active\" id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></li>";
                        }
                        else
                        {
                            strPage += "<li onclick=\"" + p_jsfuncion + "(" + (j + 1) + ")\"><span id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></li>";
                        }
                    }
                }
                else
                {
                    string cl = "";
                    int t;
                    int pagePreview = 0; //nếu đang ở trang 2 thì vẽ đc có 1 trang trước nó nên sẽ vẽ thêm 3 trang phía sau 
                    //default là vẽ 2 trang trc 2 trang sau 
                    int soTrangVeLui = 2;
                    if ((iPageCount - iCurrentPage) == 1)
                    {
                        soTrangVeLui = soTrangVeLui + 1;
                    }
                    else if ((iPageCount - iCurrentPage) == 0)
                    {
                        soTrangVeLui = soTrangVeLui + 2;
                    }
                    for (t = iCurrentPage - soTrangVeLui; t <= iCurrentPage; t++) //ve truoc 2 trang 
                    {
                        if (t < 1) continue;
                        cl = t == iCurrentPage ? "a-active" : "";
                        strPage += t == iCurrentPage ? "<li onclick=\"" + p_jsfuncion + "(" + (t) + ")\" style=\"background-color: #CDCDCD;\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>"
                                    : "<li   onclick=\"" + p_jsfuncion + "(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
                        //strPage += "<li onclick=\"page(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
                        pagePreview++;
                    }
                    t = 0;
                    cl = "";
                    if (iCurrentPage == 1) //truong hop trang dau tien thi ve du 5 trang 
                    {
                        for (t = iCurrentPage + 1; t < iCurrentPage + 5; t++)
                        {
                            if (t >= t + 5 || t > iPageCount) continue;
                            cl = t == iCurrentPage ? "a-active" : "";
                            strPage += t == iCurrentPage ? "<li onclick=\"" + p_jsfuncion + "(" + (t) + ")\" style=\"background-color: #CDCDCD;\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>"
                                     : "<li   onclick=\"" + p_jsfuncion + "(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
                            //strPage += "<li  onclick=\"page(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
                        }
                    }
                    else if (iCurrentPage > 1)  //truogn hop ma la trang lon hon 1 thi se ve 4 trang ke tiep + 1 trang truoc 
                    {
                        int incr = 5 - (pagePreview - 1);
                        for (t = iCurrentPage + 1; t < iCurrentPage + incr; t++)
                        {
                            if (t >= t + incr || t > iPageCount) continue;
                            cl = t == iCurrentPage ? "a-active" : "";
                            strPage += t == iCurrentPage ? "<li onclick=\"" + p_jsfuncion + "(" + (t) + ")\" style=\"background-color: #CDCDCD;\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>"
                                     : "<li   onclick=\"" + p_jsfuncion + "(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
                            //strPage += "<li onclick=\"page(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
                        }
                    }

                }
                if (iCurrentPage < iPageCount)
                {
                    strPage += "<li onclick=\"" + p_jsfuncion + "('" + (iCurrentPage + 1) + "')\"><span id=\"next\"  href=\"\">></span></li>";
                    //sangdd moi them dong 1 trở về trang cuối
                    strPage += "<li onclick=\"" + p_jsfuncion + "(" + iPageCount + ")\"><span id=\"end\"  href=\"\">>></span></li>";
                }
                strPage += "</div>";
                strPage += "</div>";
                return strPage;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return string.Empty;
            }
        }

        public static List<T> GetPaging<T>(List<T> pListSource, int PCurrentPage, ref int pFromRow, ref string pHtmlPaging, string pLoaiBanghi = "")
        {
            try
            {
                if (pLoaiBanghi == "")
                {
                    pLoaiBanghi = "Bản ghi";
                }
                CommonFuc _olCommon = new CommonFuc();
                pFromRow = (PCurrentPage - 1) * Common.RecordOnpage;
                List<T> lstResutl = new List<T>();
                if (pListSource.Count > PCurrentPage * Common.RecordOnpage)
                {
                    for (int i = (PCurrentPage - 1) * Common.RecordOnpage; i < PCurrentPage * Common.RecordOnpage; i++)
                    {
                        lstResutl.Add(pListSource[i]);
                    }
                }
                else if (pListSource.Count <= PCurrentPage * Common.RecordOnpage && pListSource.Count > (PCurrentPage - 1) * Common.RecordOnpage)
                {
                    for (int i = (PCurrentPage - 1) * Common.RecordOnpage; i < pListSource.Count; i++)
                    {
                        lstResutl.Add(pListSource[i]);
                    }
                }
                else
                {
                    lstResutl = pListSource;
                }
                //if (pListSource.Count > Common.RecordOnpage)
                //{
                //    double page = (double)pListSource.Count / (double)Common.RecordOnpage;
                //    int totalPage = RoundUp(page);
                //    pHtmlPaging = Paging(PCurrentPage, Common.RecordOnpage, pListSource.Count, "jsPaging");
                //}
                double page = (double)pListSource.Count / (double)Common.RecordOnpage;
                int totalPage = RoundUp(page);
                pHtmlPaging = Paging(PCurrentPage, Common.RecordOnpage, pListSource.Count, "jsPaging", pLoaiBanghi);
                return lstResutl;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new List<T>();
            }
        }

        public static string Create_OTP_Code()
        {
            try
            {
                // chu thuong 97 - 122
                // chu hoa 65 - 87
                // so 48 - 57
                StringBuilder sb = new StringBuilder();
                char c;
                Random rand = new Random();
                for (int i = 0; i < Config_Info.c_Length_Opt; i++)
                {
                    int _r = rand.Next(1, 2);
                    if (_r == 1)
                    {
                        c = Convert.ToChar(Convert.ToInt32(rand.Next(48, 57)));
                    }
                    else
                    {
                        c = Convert.ToChar(Convert.ToInt32(rand.Next(97, 122)));
                    }
                    sb.Append(c);
                }

                return sb.ToString().ToLower();
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Đọc file excel ra dataset
        /// </summary>
        /// <param name="filelocation">Tên file</param>
        public static DataSet ReadXlsxFile(string filelocation)
        {
            try
            {
                string HDR = "No";
                //string HDR = "Yes";

                string IMEX = "0";
                string strConn;
                //if (filelocation.Substring(filelocation.LastIndexOf('.')).ToLower() == ".xlsx")
                //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                //else
                //    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

                // IMEX=1 là excel hiển thị như thế nào thì nó sẽ đọc như thế
                // donght ko can check version nữa mặc định mình sẽ cài thằng 2013 nên ko cần check thằng thấp hơn .xls
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=" + IMEX + "\"";

                DataSet output = new DataSet();

                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    DataTable schemaTable = conn.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    foreach (DataRow schemaRow in schemaTable.Rows)
                    {
                        string sheet = schemaRow["TABLE_NAME"].ToString();

                        if (!sheet.EndsWith("_"))
                        {
                            try
                            {
                                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                                cmd.CommandType = CommandType.Text;

                                DataTable outputTable = new DataTable(sheet);
                                output.Tables.Add(outputTable);
                                new OleDbDataAdapter(cmd).Fill(outputTable);
                            }
                            catch (Exception ex)
                            {
                                Common.log.Error(ex.Message);
                            }
                        }
                    }
                }

                return output;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }

        public static DataSet FillDataSetFromExcel(string filePath)
        {
            try
            {
                DataSet ds_return = new DataSet();
                // Lưu file vao server
                //string filePath = Microsoft.SqlServer.Server.MapPath("/Content/FlexcelDesignFile/" + uploadFile.FileName);
                //uploadFile.SaveAs(filePath);

                // Đọc file Excel ra DataSet
                string file_extension = System.IO.Path.GetExtension(filePath);
                string connectionString_excel = "";
                switch (file_extension.ToUpper())
                {
                    case ".XLS": //Excel 97-03
                        connectionString_excel = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"");
                        break;
                    case ".XLSX": //Excel 07~
                        connectionString_excel = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"");
                        break;
                }
                OleDbConnection con = new OleDbConnection(connectionString_excel);
                con.Open();

                // Get lis SheetName in file
                List<string> lst_sheetname = new List<string>();
                DataTable dt = new DataTable();
                dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string item = dt.Rows[i]["TABLE_NAME"].ToString().Replace("'", "");
                    if (item.Contains("$"))
                        lst_sheetname.Add(item);
                }

                // Get data in file
                if (lst_sheetname.Count > 0)
                {
                    for (int sheet_index = 0; sheet_index < lst_sheetname.Count; sheet_index++)
                    {
                        OleDbDataAdapter cmd = new System.Data.OleDb.OleDbDataAdapter("select * from [" + lst_sheetname[sheet_index].ToString() + "]", con);
                        DataTable dt_data = new DataTable();
                        cmd.Fill(dt_data);
                        ds_return.Tables.Add(dt_data);
                    }
                }
                con.Close();

                // Return DataSet
                return ds_return;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        /// <summary>
        /// DongHT: Định dạng lại cái số điện thoại nhập vào
        /// </summary>
        /// <param name="p_phone"></param>
        /// <returns></returns>
        public static string FormatPhone(string p_phone)
        {
            try
            {
                string result = p_phone.Replace(".", "").Replace(" ", "").Replace("+84", "0").Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "");
                string subStr = result.Substring(0, 2);
                string _kq = result.Substring(2, result.Length - 2);
                if (subStr == "84")
                {

                    result = "0" + _kq;

                }
                else {
                    if (result.IndexOf("0") != 0)
                    {
                        result = "0" + result;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        public static string Get_File_name(ref string p_file_Path)
        {
            try
            {
                int _last_index_char = p_file_Path.LastIndexOf("\\");
                if (_last_index_char == -1)
                {
                    return p_file_Path;
                }
                string _file_name = p_file_Path.Substring(_last_index_char + 1);
                p_file_Path = p_file_Path.Substring(0, _last_index_char);

                return _file_name;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        public static string Create_Random_Character(int p_number_get)
        {
            try
            {

                // chu thuong 97 - 122
                // chu hoa 65 - 87
                // so 48 - 57
                StringBuilder sb = new StringBuilder();
                char c;
                Random rand = new Random();
                for (int i = 0; i < p_number_get; i++)
                {
                    c = Convert.ToChar(rand.Next(97, 122));
                    Thread.Sleep(10);
                    sb.Append(c);
                }
                return sb.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "";
            }
        }

        public static string Create_Random_Password()
        {
            try
            {
                // chu thuong 97 - 122
                // chu hoa 65 - 87
                // so 48 - 57
                StringBuilder sb = new StringBuilder();
                char c;
                //Random rand = new Random();
                for (int i = 0; i < Config_Info.c_Length_Password; i++)
                {
                    int _r = rand.Next(1, 4);
                    if (_r == 1)
                    {
                        c = Convert.ToChar(rand.Next(48, 57));
                    }
                    else
                    {
                        c = Convert.ToChar(rand.Next(97, 122));
                    }
                    sb.Append(c);
                }

                return sb.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }

        public static bool CheckIsUnicode_Upper(string p_unicode)
        {
            try
            {
                bool _ck = false;
                string[] pattern = new string[7];

                pattern[0] = "á|ả|à|ạ|ã|ă|ắ|ẳ|ằ|ặ|ẵ|â|ấ|ẩ|ầ|ậ|ẫ";
                pattern[1] = "ó|ỏ|ò|ọ|õ|ô|ố|ổ|ồ|ộ|ỗ|ơ|ớ|ở|ờ|ợ|ỡ";
                pattern[2] = "é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ";
                pattern[3] = "ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ";
                pattern[4] = "í|ì|ỉ|ị|ĩ";
                pattern[5] = "ý|ỳ|ỷ|ỵ|ỹ";
                pattern[6] = "đ";

                // kiểm tra xem có chữ tiếng việt thường hay không
                for (int i = 0; i < pattern.Length; i++)
                {
                    MatchCollection matchs = Regex.Matches(p_unicode, pattern[i].ToUpper());
                    if (matchs.Count > 0)
                        return true;
                }

                return _ck;
            }
            catch
            {
                return false;
            }
        }

    }

}
