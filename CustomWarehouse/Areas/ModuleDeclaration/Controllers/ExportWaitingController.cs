using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Business;
using CW_Info;

namespace CustomWarehouse.Areas.ModuleDeclaration.Controllers
{
    public class ExportWaitingController : Controller
    {
        //
        // GET: /ModuleDeclaration/ExportWaiting/

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
                int p_totalrecord = 0;
                List<Declaration_Info> lst_data = new List<Declaration_Info>();
                Declaration_BL _ObjBL = new Declaration_BL();
                lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                 (Int16)NaviCommon.Enum_Contract_Status.Da_Duyet,
                 "01/01/0001", -1, "01/01/0001", "", "DECLARATION_CODE ASC",
                    0, 0, ref p_totalrecord);
                ViewBag.Declaration = lst_data; 
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }

        [HttpPost]
        public ActionResult GetProductByDeclaration(decimal p_id)
        {
            try
            {
                Declaration_Info _info = new Declaration_Info();
                Declaration_BL _ObjBL = new Declaration_BL();
                _info = _ObjBL.DeclarationGetById(p_id);
                ViewBag.DecrationInfo = _info;
                return PartialView("/Areas/ModuleDeclaration/Views/YShare/ProductDeclaration.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveDeralation(decimal Declaration_Refence_Id, string Declaration_Code,    
        DateTime Register_Date, decimal Total_Value,  string Gate, decimal Receive_Number,
        decimal Receive_Year,  string Source, string Custom_Register,
        decimal Declaration_Type, string STR_LIST_PRODUCTS)
        {
            try
            {
                Declaration_BL _objBL = new Declaration_BL();
                Declaration_Info _Importinfo = _objBL.DeclarationGetById(Declaration_Refence_Id);
                Declaration_Info _ObjInfo = new Declaration_Info();
                _ObjInfo.Declaration_Refence_Id = Declaration_Refence_Id;
                _ObjInfo.Declaration_Refence_Code = _Importinfo.Declaration_Code;
                _ObjInfo.Declaration_Code = Declaration_Code;
                _ObjInfo.Contract_Id = _Importinfo.Contract_Id; 
                _ObjInfo.Register_Date = Register_Date;
                _ObjInfo.Contract_Code = _Importinfo.Contract_Code;
                _ObjInfo.Total_Value = Total_Value;
                _ObjInfo.WareHouse_Id = _Importinfo.WareHouse_Id;
                _ObjInfo.WareHouse_Name = _Importinfo.WareHouse_Name;
                _ObjInfo.WareHouse_Location = _Importinfo.WareHouse_Location;
                _ObjInfo.Gate = Gate;
                _ObjInfo.Receive_Number = Receive_Number;
                _ObjInfo.Receive_Year = Receive_Year; 
                _ObjInfo.Source = Source; _ObjInfo.Custom_Register = Custom_Register;
                _ObjInfo.Declaration_Type = Declaration_Type;
                _ObjInfo.Status = (decimal)NaviCommon.Enum_Contract_Status.ChoDuyet;
                _ObjInfo.Type = (decimal)NaviCommon.Enum_Declaration_Type.ToKhai_Xuat;
                _ObjInfo.Created_Date = NaviCommon.CommonFuc.CurrentDate();
                _ObjInfo.Created_By = SessionData.CurrentUser.User_Name;
                STR_LIST_PRODUCTS = STR_LIST_PRODUCTS.Trim().Trim('|');
                Product_Declaration_Info _SubInfo = new Product_Declaration_Info();
                List<Product_Declaration_Info> _ListProduct = new List<Product_Declaration_Info>();
                decimal _rel = 0;
               
                _rel = _objBL.Declaration_Insert(_ObjInfo);
                if (_rel > 0)
                {
                    string[] _strProduct = STR_LIST_PRODUCTS.Split('|');
                    foreach (var _str in _strProduct)
                    {
                        _SubInfo = new Product_Declaration_Info();
                        string[] _temp = _str.Split(',');
                        if (_temp.Length == 4)
                        {
                            _SubInfo.Declaration_Id = _rel;
                            _SubInfo.Product_Id = Convert.ToDecimal(_temp[0]);
                            _SubInfo.Package_Quantity = Convert.ToDecimal(_temp[1]);
                            _SubInfo.Quantity = Convert.ToDecimal(_temp[2]);
                            _SubInfo.Value = Convert.ToDecimal(_temp[3]);
                            _SubInfo.Type = (decimal)NaviCommon.ProductDeralationType.Export;
                            _SubInfo.Declaration_Reference_Id = Declaration_Refence_Id;
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
                return Json(new { success = _rel });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = -1 });
            }
        }

        public ActionResult List()
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
                    "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Xuat),
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

                 p_totalrecord = 0;
                List<Declaration_Info> lst_Dec = new List<Declaration_Info>();
                lst_Dec = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                 (Int16)NaviCommon.Enum_Contract_Status.Da_Duyet,
                 "01/01/0001", -1, "01/01/0001", "", "DECLARATION_CODE ASC",
                    0, 0, ref p_totalrecord);
                ViewBag.DeclarationSearch = lst_Dec; 
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
                    if (arr_keysearch.Length == 9)
                    {
                        lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, arr_keysearch[0], arr_keysearch[1],
                            arr_keysearch[2], arr_keysearch[3], arr_keysearch[4], Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Xuat),
                             Convert.ToInt16(NaviCommon.Enum_Contract_Status.ChoDuyet), arr_keysearch[5],
                            Convert.ToDecimal(arr_keysearch[6]), arr_keysearch[7], arr_keysearch[8],
                            p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                    }
                }
                else// TRƯỜNG HỢP KO CÓ GÌ THÌ LẤY TẤT
                {
                    lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Xuat),
                             Convert.ToInt16(NaviCommon.Enum_Contract_Status.ChoDuyet), "01/01/0001",
                            -1, "01/01/0001", "", p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                }
                ViewBag.Paging = HtmlControllHelpers.PagingData(p_currentpage, Common.cNumberRecordOnPage, p_totalrecord);
                ViewBag.RecordOnPage = Common.cNumberRecordOnPage;
                ViewBag.IsSearch = p_issearch;
                ViewBag.Declaration = lst_data;
                return PartialView("ListDataTable");
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
                Declaration_Info _info = new Declaration_Info();
                Declaration_BL _ObjBL = new Declaration_BL();
                _info = _ObjBL.DeclarationGetById(p_id);
                ViewBag.DecrationInfo = _info;
                return PartialView("/Areas/ModuleDeclaration/Views/ExportWaiting/ViewDetails.cshtml");
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
                return PartialView("/Areas/ModuleDeclaration/Views/ExportWaiting/ViewDetails.cshtml");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }
    }
}
