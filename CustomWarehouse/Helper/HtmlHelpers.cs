using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.IO;
using System.Text;

namespace CustomWarehouse
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// load danh sach menu 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pIntNumberInput"> </param>
        /// <returns></returns>
        ///  this HtmlHelper helper khai bao nhằm tạo extention cho HtmlHelper
        public static HtmlString CreatTextInputDynamic(this HtmlHelper helper ,int pIntNumberInput)
        {
            try
            {
                //muon load động css chuyển về stringformat 
                string input = "";
                string nameTextInput ="_txturlEmbedReport"+ pIntNumberInput.ToString() ;
                string nameBtnAddNew ="_btnAdd"+ pIntNumberInput.ToString() ;
                input += string.Format("<input class='inputUrl' name='{0}' type='text'   placeholder='Url Report Obiee' />",nameTextInput);
                input += string.Format("<input class='inputButton' type='submit' name='{0}' value=' Add Report '    />",   nameBtnAddNew);
                return new HtmlString(input);
            }
            catch  
            {
                return new HtmlString("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="url"> </param>
        /// <returns></returns>
        public static HtmlString Url2HtmlString(this HtmlHelper helper, string url)
        {
            
            System.Net.WebClient client = new System.Net.WebClient();
            byte[] buffer = client.DownloadData(url);
            string abl = client.DownloadString(url);
            string htmlstring = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            string decode = Uri.UnescapeDataString(url);
            return new HtmlString(abl);

        }

        public static MvcHtmlString StripHTML(string input)
        {


            return new MvcHtmlString(System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty).Replace("&nbsp;", " "));
        }

        public static MvcHtmlString StripHTML(this HtmlHelper html, string input)
        {
            return new MvcHtmlString(System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty).Replace("&nbsp;", " "));
        }
        // đây là reges đếm số dòng code trong project
        // ^(?([^\r\n])\s)*[^\s+?/]+[^\n]*$

        public static string PagingData(int pCurPage, int pRecordOnPage, int pTotalRecord, string _str_loai_ban_ghi)
        {
            try
            {
                string pStrPaging = "";
                double _dobTotalRec = Convert.ToDouble(pTotalRecord);
                int _TotalPage = NaviCommon.CommonFuc.RoundUp(_dobTotalRec / pRecordOnPage);
                pStrPaging = WritePaging(_TotalPage, pCurPage, pTotalRecord, pRecordOnPage, _str_loai_ban_ghi);
                return pStrPaging;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "";
            }
        }

        public static string WritePaging(int iPageCount, int iCurrentPage, int iTotalRecords, int iPageSize, string pLoaiBanGhi)
        {
            try
            {
                string strPage = "";
                strPage += "<div id='d_page'>";
                // strPage += "<div id='d_total_rec'>" + "Có tổng " + iTotalRecords + " " + pLoaiBanGhi + "</div>";
                strPage += "<div id='d_total_rec'>" + "Có tổng " + iTotalRecords + " " + pLoaiBanGhi + " </div>";
                strPage += "<div id='d_number_of_page'>";
                if (iPageCount <= 1) return strPage;
                if (iCurrentPage > 1)
                {
                    //sangdd moi them dong 1 trở về trang đầu
                    strPage += "<li onclick=\"page(1)\"><span id=\"first\"  href=\"\"><<</span></li>";

                    strPage += "<li onclick=\"page('" + (iCurrentPage - 1) + "')\"><span id=\"back\"  href=\"\"><</span></li>";
                }
                if (iPageCount <= 5)
                {
                    for (int j = 0; j < iPageCount; j++)
                    {
                        if (iCurrentPage == (j + 1))
                        {
                            //HungTD rem doan nay
                            strPage += "<li style=\"background-color: #CDCDCD;\" onclick=\"page(" + (j + 1) + ")\"><span class=\"a-active\" id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></li>";
                            // strPage += "<li><span class=\"a-active\" id='_currpage' onclick=\"page(" + (j + 1) + ")\" href=\"\">" + (j + 1) + "</span></li>";
                        }
                        else
                        {
                            strPage += "<li onclick=\"page(" + (j + 1) + ")\"><span id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></li>";
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
                        strPage += t == iCurrentPage ? "<li onclick=\"page(" + (t) + ")\" style=\"background-color: #CDCDCD;\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>"
                                    : "<li   onclick=\"page(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
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
                            strPage += t == iCurrentPage ? "<li onclick=\"page(" + (t) + ")\" style=\"background-color: #CDCDCD;\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>"
                                     : "<li   onclick=\"page(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
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
                            strPage += t == iCurrentPage ? "<li onclick=\"page(" + (t) + ")\" style=\"background-color: #CDCDCD;\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>"
                                     : "<li   onclick=\"page(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
                            //strPage += "<li onclick=\"page(" + (t) + ")\"><span class=" + cl + " id=" + (t) + "  href=\"\">" + (t) + "</span></li>";
                        }
                    }

                }
                if (iCurrentPage < iPageCount)
                {
                    strPage += "<li onclick=\"page('" + (iCurrentPage + 1) + "')\"><span id=\"next\"  href=\"\">></span></li>";
                    //sangdd moi them dong 1 trở về trang cuối
                    strPage += "<li onclick=\"page(" + iPageCount + ")\"><span id=\"end\"  href=\"\">>></span></li>";
                }
                strPage += "</div>";
                strPage += "</div>";
                return strPage;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

    }
}