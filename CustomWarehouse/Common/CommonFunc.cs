using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using CW_Business;

namespace CustomWarehouse
{
    public class CommonFunc
    {

        /// <summary>
        /// KIỂM TRA QUYỀN TRUY CẬP VÀ SESSION
        /// </summary>
        public static string Nvs_Redirect_QuyenTruyCapUser(string _url, string p_ip_request = "")
        {
            try
            {
                // kiểm tra xem còn Session không
                var objUser = SessionData.CurrentUser as User_Info;
                if (objUser == null)
                {
                    return "~/home/admin";
                }

                decimal _re = CheckQuyenTruyCapUser(_url);
                if (_re == (decimal)NaviCommon.Grant_Auz_Result.Decline)
                {
                    return "~/Home/QuyenTruyCap";
                }

                return "";
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "~/home/admin";
            }
        }


        /// <summary>
        /// Date:
        /// Pp:Kiểm tra quyền (chức năng ) truy cập vào hệ thông 
        /// </summary>
        /// <param name="pFunctionName">Tên hàm tương ứng với Action Controlle</param>
        /// <returns>1: được phép truy cập  0: không được phép truy cập, -1 khong dung quyen admin</returns>
        public static decimal CheckQuyenTruyCapUser(string pFunctionName)
        {
            try
            {

                //Nếu là tài khoản Navisoft thì không cần phải check quyền 
                if (SessionData.CurrentUser.User_Name == "Administrator" && SessionData.CurrentUser.Password == "tt8administrator123!@#")
                {
                    return (decimal)NaviCommon.Grant_Auz_Result.Accept;
                }

                if (SessionData.CurrentUser.gHshFunctionOfUser.ContainsKey(pFunctionName.Trim().ToUpper()))
                {
                    return (decimal)NaviCommon.Grant_Auz_Result.Accept;
                }
                else
                {
                    //Tranh TH nguoi dung phan nhieu quyen db chua insert xong da login lai 
                    //kiểm tra lại lần nữa cho chắc ăn 
                    if (SessionData.CurrentUser.gHshFunctionOfUser.Count == 0)
                    {
                        FunctionsBL _func = new FunctionsBL();
                        SessionData.CurrentUser.gHshFunctionOfUser = _func.GetUserFuncByUserID(SessionData.CurrentUser.User_Id);
                        if (SessionData.CurrentUser.gHshFunctionOfUser.ContainsKey(pFunctionName.Trim().ToUpper()))
                        {
                            return (decimal)NaviCommon.Grant_Auz_Result.Accept;
                        }
                    }
                }
                return (decimal)NaviCommon.Grant_Auz_Result.Decline;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return (decimal)NaviCommon.Grant_Auz_Result.Decline;
            }
        }

        #region kết xuất excel bằng Flexcel

        public static int ExportExcel(FlexCel.Report.FlexCelReport flcReport, string pathFileSource, string c_fileExport, ref string _err)//, System.Windows.Forms.SaveFileDialog saveReport)
        {
            System.IO.FileStream _templateStream = null;
            try
            {
                flcReport.DeleteEmptyRanges = false; string _path_source = "";
                //string exepath =    Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "");
                MemoryStream _xlsMemoryStream = new MemoryStream();
                #region Luu file xls len memory stream
                _path_source = pathFileSource.Replace("//", "\\");
                bool ckFileIsNotOpen = true;
                //check xem file co ton tai trong duong dan ko?
                if (!System.IO.File.Exists(_path_source))//if (!System.IO.File.Exists(exepath + "\\" + pathFile.Replace("//", "\\")))
                {
                    //bao loi ko ton tai file;
                    _err = "Không tồn tại file mẫu trong thư mục mẫu!";
                    return -3; // không có file mẫu
                }
                else
                {
                    //check file co dang mo hay ko
                    try
                    {
                        var stream = new FileStream(_path_source, FileMode.Open, FileAccess.Read);
                        stream.Close();
                    }
                    catch
                    {
                        //NoteBox.Show("File mẫu đang mở hoặc bị process khác sử dụng, bạn phải đóng file đó mới có thể kết xuất", "Error", NoteBoxLevel.Error);
                        ckFileIsNotOpen = false;
                        _err = "File đang được mở trong process khác! Không thể tạo báo cáo!";
                        return -2; // file đang mo không làm j đc
                    }

                    if (ckFileIsNotOpen == true)
                    {
                        _templateStream = new System.IO.FileStream(_path_source, System.IO.FileMode.Open);
                        flcReport.Run(_templateStream, _xlsMemoryStream);
                        //luu file 
                        _xlsMemoryStream.Position = 0;
                        byte[] bytes = new byte[_xlsMemoryStream.Length];
                        _xlsMemoryStream.Read(bytes, 0, (int)_xlsMemoryStream.Length);
                        try
                        {
                            FileStream OutStream;
                            OutStream = new FileStream(c_fileExport, FileMode.Create);//new FileStream(saveReport.FileName, FileMode.Create);
                            OutStream.Write(bytes, 0, bytes.Length);
                            OutStream.Close();
                            _xlsMemoryStream.Close();
                            _templateStream.Close();
                            return 0;
                        }
                        catch (Exception ex)
                        {
                            NaviCommon.Common.log.Error(ex.ToString());
                            _templateStream.Close();
                            _err = "Lỗi tạo file báo cáo !";
                            return -1; // lỗi tạo file
                        }
                    }
                    _err = "File đang được mở trong process khác! Không thể tạo báo cáo!";
                    return -2; // file đang mo không làm j đc
                }
                #endregion
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                _templateStream.Close();
                _err = "Lỗi tạo file báo cáo !";
                return -1;
            }
        }

        public static void SetValueExportByDataTable(ref FlexCel.Report.FlexCelReport flcReport, DataSet v_ds)
        {
            try
            {
                flcReport.AddTable(v_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public static void SetValueExportByString(ref FlexCel.Report.FlexCelReport flcReport, string _ParamName, object _value)
        {
            try
            {
                flcReport.SetValue(_ParamName, _value);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }
        #endregion
    }
}