using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Business;
using CW_Info;
using System.Data;
using NaviCommon;
using System.Collections;
namespace CustomWarehouse.Areas.ModuleDeclaration.Controllers
{
    public class WaitingController : Controller
    {
        //
        // GET: /ModuleDeclaration/Waiting/ListSearchImport_Datas

        public ActionResult Add()
        {
            try
            {
                string _url = Request.RawUrl;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                {
                    return Redirect(_ok);
                }
                ContractBL _ContractBL = new ContractBL();
                List<Contracts_Info> ListContract = _ContractBL.Contract_GetByStatus((int)NaviCommon.Enum_Contract_Status.Da_Duyet);
                ViewBag.ListContract = ListContract;
                Product_BL _Product_BL = new Product_BL();
                List<Product_Info> _lst_data = _Product_BL.Product_GetAll();
                ViewBag.ListProduct = _lst_data;
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveDeralation(string Declaration_Code, decimal Contract_Id, string Contract_Code,  DateTime Register_Date,
        decimal Total_Value, decimal WareHouse_Id, string WareHouse_Name, string Gate, decimal Receive_Number,
        decimal Receive_Year, decimal VANDON_NUMBER, DateTime VANDON_DATE, string Source, string Custom_Register,
        decimal Declaration_Type, string STR_LIST_PRODUCTS)
        {
            try
            {
                Declaration_Info _ObjInfo = new Declaration_Info();
                _ObjInfo.Declaration_Code = Declaration_Code;
                _ObjInfo.Contract_Id = Contract_Id; _ObjInfo.Register_Date = Register_Date;
                _ObjInfo.Contract_Code = Contract_Code.Trim();
                _ObjInfo.Total_Value = Total_Value;  _ObjInfo.WareHouse_Id = WareHouse_Id;
                _ObjInfo.WareHouse_Name = WareHouse_Name;  _ObjInfo.Gate = Gate;
                _ObjInfo.Receive_Number = Receive_Number;
                _ObjInfo.Receive_Year = Receive_Year; _ObjInfo.VANDON_NUMBER = VANDON_NUMBER;
                _ObjInfo.VANDON_DATE = VANDON_DATE; _ObjInfo.VANDON_DATE = VANDON_DATE;
                _ObjInfo.Source = Source; _ObjInfo.Custom_Register = Custom_Register;
                _ObjInfo.Declaration_Type = Declaration_Type;
                _ObjInfo.Status = (decimal)NaviCommon.Enum_Contract_Status.ChoDuyet;
                _ObjInfo.Type = (decimal)NaviCommon.Enum_Declaration_Type.ToKhai_Nhap;
                _ObjInfo.Created_Date = NaviCommon.CommonFuc.CurrentDate();
                _ObjInfo.Created_By = SessionData.CurrentUser.User_Name;
                STR_LIST_PRODUCTS = STR_LIST_PRODUCTS.Trim().Trim('|');
                Product_Declaration_Info _SubInfo = new Product_Declaration_Info();
                List<Product_Declaration_Info> _ListProduct = new List<Product_Declaration_Info>();
                decimal _rel = 0;
                Declaration_BL _objBL = new Declaration_BL();
                _rel = _objBL.Declaration_Insert(_ObjInfo);
                if (_rel > 0)
                {
                    string[] _strProduct = STR_LIST_PRODUCTS.Split('|');
                    foreach (var _str in _strProduct)
                    {
                        _SubInfo = new Product_Declaration_Info();
                        string[] _temp = _str.Split(',');
                        if (_temp.Length == 5)
                        {
                            _SubInfo.Declaration_Id = _rel;
                            _SubInfo.Product_Id = Convert.ToDecimal(_temp[0]);
                            _SubInfo.Package_Quantity = Convert.ToDecimal(_temp[1]);
                            _SubInfo.Quantity = Convert.ToDecimal(_temp[2]);
                            _SubInfo.Made_In = _temp[3];
                            _SubInfo.Value = Convert.ToDecimal(_temp[4]);
                            _SubInfo.Type = (decimal)NaviCommon.ProductDeralationType.Import;
                            // nếu là nhập import thì trường sl còn lại nhâp = luôn số lượng nhập
                            _SubInfo.Package_Quantity_Delivery = _SubInfo.Package_Quantity;
                            _SubInfo.Quantity_Delivery = _SubInfo.Quantity;
                            _ListProduct.Add(_SubInfo);
                        }
                    }
                }
                Product_Declaration_BL _PrDBL = new Product_Declaration_BL();
                foreach (Product_Declaration_Info item in _ListProduct)
                {
                    if (_PrDBL.Product_Declaration_Insert(item) == false)
                    {
                        _rel = -1;
                        break;
                    }
                }
                return Json(new { success= _rel});
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }

        public ActionResult ListImport()
        {
            try
            {
                string _url = Request.RawUrl;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                    return Redirect(_ok);

                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;

                int p_totalrecord = 0;
                int p_start = Common.cNumberRecordOnPage * (1 - 1) + 1;
                int p_end = Common.cNumberRecordOnPage * 1;
                List<Declaration_Info> lst_data = new List<Declaration_Info>();
                Declaration_BL _ObjBL = new Declaration_BL();
                lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name,
                    "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                  Convert.ToInt16(NaviCommon.Enum_Contract_Status.ChoDuyet), "01/01/0001", -1, "01/01/0001", "", 
                  "DECLARATION_CODE ASC",
                    p_start, p_end, ref p_totalrecord);
                ViewBag.Declaration = lst_data;
                List<Contracts_Info> lstContract = new List<Contracts_Info>();
                ContractBL _ContractjBL = new ContractBL();

                lstContract = _ContractjBL.TableSearch("", DateTime.MinValue, Convert.ToInt16(NaviCommon.Enum_Contract_Status.Da_Duyet),
                    SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Name,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    "CONTRACT_CODE ASC", 0, 0, ref p_totalrecord);
                ViewBag.ListContract = lstContract;
                return View();
            }
            catch (Exception ex)
            {                
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ListSearchImport_Datas(int p_issearch, string p_keysearch, string p_orderby, string p_ordertype, int p_currentpage)
        {
            try
            {
                int p_start = Common.cNumberRecordOnPage * (p_currentpage - 1) + 1;
                int p_end = Common.cNumberRecordOnPage * p_currentpage;
                int p_totalrecord = 0;
                List<Declaration_Info> lst_data = new List<Declaration_Info>();
                Declaration_BL _ObjBL = new Declaration_BL();
                if (!string.IsNullOrEmpty(p_keysearch))
                {
                    string[] arr_keysearch = p_keysearch.Split('|');
                    if (arr_keysearch.Length == 8)
                    {
                        lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(),SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, arr_keysearch[0], arr_keysearch[1],
                            arr_keysearch[2], arr_keysearch[3], arr_keysearch[4], Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                             Convert.ToInt16(NaviCommon.Enum_Contract_Status.ChoDuyet), arr_keysearch[5],
                            Convert.ToDecimal(arr_keysearch[6]), arr_keysearch[7], "", 
                            p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                    }
                }
                else// TRƯỜNG HỢP KO CÓ GÌ THÌ LẤY TẤT
                {
                    lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(),SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                             Convert.ToInt16(NaviCommon.Enum_Contract_Status.ChoDuyet), "01/01/0001",
                            -1, "01/01/0001", "", p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                }
                ViewBag.Paging = HtmlControllHelpers.PagingData(p_currentpage, Common.cNumberRecordOnPage, p_totalrecord);
                ViewBag.RecordOnPage = Common.cNumberRecordOnPage;
                ViewBag.IsSearch = p_issearch;
                ViewBag.Declaration = lst_data;
                return PartialView("ListImportDataTable");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("ListImportDataTable");
            }
        }

        [HttpPost]
        public ActionResult ViewDetails(decimal p_id)
        {
            try
            {
                Declaration_Info  _info = new Declaration_Info();
                Declaration_BL _ObjBL = new Declaration_BL();
                _info = _ObjBL.DeclarationGetById(p_id);
                ViewBag.DecrationInfo = _info;
                return PartialView("/Areas/ModuleDeclaration/Views/Waiting/ViewDetails.cshtml");
            } 
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        [HttpPost]
        public ActionResult ViewApprove(decimal p_id)
        {
            try
            {
                Declaration_Info _info = new Declaration_Info();
                Declaration_BL _ObjBL = new Declaration_BL();
                _info = _ObjBL.DeclarationGetById(p_id);
                ViewBag.DecrationInfo = _info;
                ViewBag.IsApprove = 1;
                return PartialView("/Areas/ModuleDeclaration/Views/Waiting/ViewDetails.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }


        [HttpPost]
        public ActionResult Products_ImportFile(HttpPostedFileBase uploadFile)
        {
            string filePath = "";
            try
            {
                int _rel = 1;
                   List<Product_Declaration_Info> lst_product_Dec = new List<Product_Declaration_Info>();
                if (uploadFile != null)
                {
                    if (!string.IsNullOrEmpty(uploadFile.FileName))
                    {
                        // lấy ds sản phẩm lên trước
                        Product_BL _Product_BL = new Product_BL();
                        List<Product_Info> _lst_data = _Product_BL.Product_GetAll();
                        Hashtable _hsProduct = new Hashtable();
                        foreach (Product_Info item in _lst_data)
                        {
                            _hsProduct[item.Product_Code.ToUpper()] = item;
                        }
                        string _temp = uploadFile.FileName;
                        string _file_name = NaviCommon.CommonFuc.Get_File_name(ref _temp);
                        // Lưu file vao server
                        filePath = Server.MapPath(NaviCommon.Config_Info.c_file_Upload_Sync + _file_name);
                        // Nếu có file rồi delete cho chắc
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);

                        uploadFile.SaveAs(filePath); 
                        string file_extension = System.IO.Path.GetExtension(filePath);
                        DataSet ds = new DataSet();
                        ds = CommonFuc.ReadXlsxFile(filePath);
                        System.IO.File.Delete(filePath);
                      
                        #region Đọc file
                        if (ds.Tables.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                i++;
                                if (i < 3)
                                {
                                    // chỉ lấy dòng thứ 3 trở đi
                                    continue;
                                }
                                Product_Declaration_Info _PruductInfo = new Product_Declaration_Info();
                                string _ProductCode = "";
                                decimal _Pack_Quantity = 0;
                                decimal _Quantity = 0;
                                string _MadeIn = "";
                                decimal _Value = 0;
                                _ProductCode = dr[0].ToString();
                                if (!_hsProduct.ContainsKey(_ProductCode.ToUpper()))
                                {
                                    // không tồn tại sản phẩm
                                    _rel = -10;
                                    break;
                                }
                             
                                // số kiện
                                try
                                {
                                    _Pack_Quantity = Convert.ToDecimal(dr[1].ToString());
                                }
                                catch (Exception)
                                {
                                    _rel = -11;// ko phải là số
                                    break;    
                                }
                                // số lượng
                                try
                                {
                                    _Quantity = Convert.ToDecimal(dr[2].ToString());
                                }
                                catch (Exception)
                                {
                                    _rel = -11;// ko phải là số
                                    break;
                                }
                                //nơi xuất xứ 
                                _MadeIn = dr[3].ToString();
                                try
                                {
                                    _Value = Convert.ToDecimal(dr[4].ToString());
                                }
                                catch (Exception)
                                {
                                    _rel = -11;// ko phải là số
                                    break;
                                }
                                Product_Info _tempInfo = (Product_Info)_hsProduct[_ProductCode.ToUpper()];
                                _PruductInfo.Product_Id = _tempInfo.Product_Id;
                                _PruductInfo.Product_Code = _ProductCode;
                                _PruductInfo.Package_Quantity = _Pack_Quantity;
                                _PruductInfo.Quantity = _Quantity;
                                _PruductInfo.Made_In = _MadeIn;
                                _PruductInfo.Value = _Value;
                                lst_product_Dec.Add(_PruductInfo);
                            }
                           #endregion
                             
                        }
                    }
                }
                if (_rel > 0)
                {
                    ViewBag.ListProduct = lst_product_Dec;
                    return PartialView("/Areas/ModuleDeclaration/Views/YShare/PartialListProduct.cshtml"); 
                }
                else
                    return null;
               
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(filePath);
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("/Areas/ModuleDeclaration/Views/YShare/PartialListProduct.cshtml");

            }
        }
    }
}
