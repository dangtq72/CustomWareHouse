//HungTD:26-09-2014
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CustomWarehouse
{
    public class HtmlControllHelpers
    {
        /// <summary>
        /// Create: sangdd
        /// Mục đích: vẽ nexttrang 
        /// </summary>
        /// <param name="pCurPage">Trang hiện tại</param>
        /// <param name="pRecordOnPage">Số bản ghi trên trang</param>
        /// <param name="pTotalRecord">Tổng số bản ghi</param>
        /// <returns></returns>
        public static string PagingData(int pCurPage, int pRecordOnPage, int pTotalRecord)
        {
            try
            {
                string pStrPaging = "";
                double _dobTotalRec = Convert.ToDouble(pTotalRecord);
                int _TotalPage = NaviCommon.CommonFuc.RoundUp(_dobTotalRec / pRecordOnPage);
                pStrPaging = HtmlControllHelpers.WritePaging(_TotalPage, pCurPage, pTotalRecord, pRecordOnPage, "bản ghi");
                return pStrPaging;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return  "";
            }
        }
        /// <summary>
        /// Hàm paging Data
        /// </summary>
        /// <typeparam name="TSource">Object tương ứng </typeparam>
        /// <param name="pLstSource"></param>
        /// <param name="pCurPage"></param>
        /// <param name="pRecordOnPage"></param>
        /// <param name="pStrPaging"></param>
        /// <returns></returns>
        public static List<TSource> PagingData<TSource>(List<TSource> pLstSource, int pCurPage, int pRecordOnPage, ref string pStrPaging)
        {
            try
            {
                List<TSource> lstCurrentPage = new List<TSource>();
                int pIntTotalRecord = pLstSource.Count;
                double _dobTotalRec = Convert.ToDouble(pIntTotalRecord);
                int _TotalPage = NaviCommon.CommonFuc.RoundUp(_dobTotalRec / pRecordOnPage);
                pStrPaging = HtmlControllHelpers.WritePaging(_TotalPage, pCurPage, pIntTotalRecord, pRecordOnPage, "bản ghi");
                int start = pRecordOnPage * (pCurPage - 1);
                int end = pRecordOnPage * pCurPage;
                if (end > pIntTotalRecord)
                {
                    end = pIntTotalRecord;
                }
                //biến gắn vị trí cuối của bản ghi cần gửi ra 
                for (int i = start; i < end; i++)
                {
                    lstCurrentPage.Add(pLstSource[i]);
                }
                return lstCurrentPage;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<TSource>();
            }
        }
        /// <summary>
        /// Sangdd
        /// Hàm gen ra chuỗi html dùng cho phân trang
        /// tổng số trang = tổng số bản ghi /số bản ghi trên 1 trang
        /// <param name="iPageCount">Tổng số trang</param>
        /// <param name="iCurrentPage">Trang hiện tại</param>
        /// <param name="iTotalRecords">Tổng số bản ghi</param>
        /// <param name="iPageSize">Số bản ghi trên 1 trang</param>
        /// <returns></returns>   
        public static string WritePaging(int iPageCount, int iCurrentPage, int iTotalRecords, int iPageSize, string pLoaiBanGhi)
        {
            try
            {
                string strPage = "";
                strPage += "<div id='d_page'>";
               // strPage += "<div id='d_total_rec'>" + "Có tổng " + iTotalRecords + " " + pLoaiBanGhi + "</div>";
                strPage += "<div id='d_total_rec'>" + "Có tổng " + iTotalRecords + " bản ghi</div>";
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
                NaviCommon.Common.log.Error(ex.ToString());
                return string.Empty;
            }
        }

        public static string WritePaging(int iPageCount, int iCurrentPage, int iTotalRecords, int iPageSize)
        {
            try
            {
                string strPage = "";
                strPage += "<div id='d_page'>";
                strPage += "<div id='d_total_rec'>" + "Có tổng " + iTotalRecords + " bản ghi</div>";
                strPage += "<div id='d_number_of_page'>";
                if (iPageCount <= 1) return strPage;
                if (iCurrentPage > 1)
                {
                    strPage += "<li onclick=\"page(1)\"><span id=\"back\"  href=\"\"><<</span></li>";
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
                    strPage += "<li onclick=\"page(" + iPageCount + ")\"><span id=\"end\"  href=\"\">>></span></li>";
                }
                strPage += "</div>";
                strPage += "</div>";
                return strPage;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return string.Empty;
            }
        }

        ///// <summary>
        /// HungTD:24-09-2014: gen html gropdownlist voi selected item
        /// </summary>
        /// <param name="p_id_selected"> id lay len tu form</param>
        /// <param name="p_DicOject"> danh sach key va values lay tu db</param>
        /// <param name="p_control_id"> id control tren html</param>
        /// <returns>html code</returns>
        public static string GenDropDownList(string p_id_selected, Dictionary<string, string> p_DicOject, string p_attribute, string p_control_id, string p_defaul_item)
        {
            try
            {
                if (p_DicOject == null)
                {
                    return "";
                }
                StringBuilder _SrtBuilder = new StringBuilder();
                _SrtBuilder.Append("<select " + p_attribute + " id='" + p_control_id + "' name ='" + p_control_id + "'>");
                _SrtBuilder.Append(p_defaul_item);
                if (p_DicOject.Count == 0)
                {
                    _SrtBuilder.Append("</select>");
                    return _SrtBuilder.ToString();
                }
                foreach (KeyValuePair<string, string> pair in p_DicOject)
                {
                    if (pair.Key.ToUpper().Trim() == p_id_selected.ToUpper().Trim())//neu co ton tai id                 
                    {
                        _SrtBuilder.Append("<option selected='selected' value='" + pair.Key + "'>");
                        _SrtBuilder.Append(pair.Value);
                        _SrtBuilder.Append("</option>");
                    }
                    else
                    {
                        _SrtBuilder.Append("<option  value='" + pair.Key + "'>");
                        _SrtBuilder.Append(pair.Value);
                        _SrtBuilder.Append("</option>");
                    }
                }
                _SrtBuilder.Append("</select>");
                return _SrtBuilder.ToString();

            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// HungTD:24-09-2014
        /// </summary>
        /// <param name="p_ListId">danh sach nhung id checked checkbox</param>
        /// <param name="p_DicOject">danh sach key va values lay tu db</param>
        /// <param name="p_control_name">id control tren html</param>
        /// <returns>html code</returns>
        public static string GenListCheckBox(string p_ListId, Dictionary<string, string> p_DicOject, string p_attribute, string p_control_name)
        {
            try
            {
                if (p_DicOject.Count == 0)
                {
                    return "";
                }
                StringBuilder _SrtBuilder = new StringBuilder();
                string[] _ListId = p_ListId.Split('|');
                foreach (KeyValuePair<string, string> pair in p_DicOject)
                {
                    if (_ListId.Contains(pair.Key))//neu co ton tai id                 
                    {
                        _SrtBuilder.Append("<input type='checkbox' checked class='" + p_control_name + "' value='" + pair.Key + "' >");
                        _SrtBuilder.Append(pair.Value);
                        _SrtBuilder.Append("<br/>");
                    }
                    else
                    {
                        _SrtBuilder.Append("<input type='checkbox' class='" + p_control_name + "'  value='" + pair.Key + "' >");
                        _SrtBuilder.Append(pair.Value);
                        _SrtBuilder.Append("<br/>");
                    }
                }
                return _SrtBuilder.ToString();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "";
            }
        }

        public static string GenListBussinessAndAuth(string p_ListId, Dictionary<string, string> p_DicOject, string p_attribute, string p_control_name)
        {
            try
            {
                if (p_DicOject.Count == 0)
                {
                    return "";
                }
                StringBuilder _SrtBuilder = new StringBuilder();
                _SrtBuilder.Append("<ul>");
                string[] _ListId = p_ListId.Split('|');
                foreach (KeyValuePair<string, string> pair in p_DicOject)
                {
                    if (_ListId.Contains(pair.Key))//neu co ton tai id                 
                    {
                        _SrtBuilder.Append("<li>");
                        _SrtBuilder.Append(pair.Value);
                        _SrtBuilder.Append("</li>");
                    }
                }
                _SrtBuilder.Append("</ul>");
                return _SrtBuilder.ToString();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "";
            }
        }
        // đây là reges đếm số dòng code trong project
        // ^(?([^\r\n])\s)*[^\s+?/]+[^\n]*$
    }
}