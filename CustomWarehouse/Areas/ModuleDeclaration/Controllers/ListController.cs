using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Business;
using CW_Info;
namespace CustomWarehouse.Areas.ModuleDeclaration.Controllers
{
    public class ListController : Controller
    {
        //
        // GET: /ModuleDeclaration/List/
    
        public ActionResult ListImport()
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
                int p_start = Common.cNumberRecordOnPage * (1 - 1) + 1;
                int p_end = Common.cNumberRecordOnPage * 1;
                List<Declaration_Info> lst_data = new List<Declaration_Info>();
                Declaration_BL _ObjBL = new Declaration_BL();
                lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                 -1, "01/01/0001", -1, "01/01/0001", "",  "DECLARATION_CODE ASC",
                    p_start, p_end, ref p_totalrecord);
                ViewBag.Declaration = lst_data;
                List<Contracts_Info> lstContract = new List<Contracts_Info>();
                ContractBL _ContractjBL = new ContractBL();

                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;

                lstContract = _ContractjBL.TableSearch("", DateTime.MinValue, Convert.ToInt16(NaviCommon.Enum_Contract_Status.Da_Duyet),
                    SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Name,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    "CONTRACT_CODE ASC",
                    0, 0, ref p_totalrecord);
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
                    if (arr_keysearch.Length == 9)
                    {
                        lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name,
                            arr_keysearch[0], arr_keysearch[1],
                            arr_keysearch[2], arr_keysearch[3], arr_keysearch[4], Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                            Convert.ToInt16(arr_keysearch[8]), arr_keysearch[5],
                            Convert.ToDecimal(arr_keysearch[6]), arr_keysearch[7], "", 
                            p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                    }
                }
                else// TRƯỜNG HỢP KO CÓ GÌ THÌ LẤY TẤT
                {
                    lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name,
                        "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                             -1, "01/01/0001",
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
        public ActionResult ChangeStatus(decimal P_ID, string P_REASON_REJECT, int P_STATUS)
        {
            try
            {
                Declaration_BL _objBL = new Declaration_BL();
                bool _rel = _objBL.Declaration_Update_Status(P_ID, P_STATUS, P_REASON_REJECT, SessionData.CurrentUser.User_Name);
                return Json(new { success = _rel });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { success = false });
            }
        }


        public ActionResult EditImport()
        {
            try
            {
                string _url = Request.RawUrl;
                string _ok = CommonFunc.Nvs_Redirect_QuyenTruyCapUser(_url);
                if (_ok != "")
                {
                    return Redirect(_ok);
                }
                decimal _Id = 0;
                int _tab = 1;
                if (RouteData.Values["id"] != null)
                {
                    _Id = Convert.ToDecimal(RouteData.Values["id"]);
                }
                if (RouteData.Values["id1"] != null)
                {
                    _tab = Convert.ToInt16(RouteData.Values["id1"].ToString());
                }
                Product_BL _Product_BL = new Product_BL();
                List<Product_Info> _lst_data = _Product_BL.Product_GetAll();
                ViewBag.ListProduct = _lst_data;
                ViewBag.CurrTab = _tab;
                Declaration_Info _info = new Declaration_Info();
                Declaration_BL _ObjBL = new Declaration_BL();
                _info = _ObjBL.DeclarationGetById(_Id);
                ViewBag.DecrationInfo = _info;
                ViewBag.IsApprove = 1;
                return View(_info);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveEditDeralation(decimal Declaration_Id, string Declaration_Code, decimal Contract_Id, string Contract_Code, DateTime Register_Date,
        decimal Total_Value, decimal WareHouse_Id, string WareHouse_Name, string Gate, decimal Receive_Number,
        decimal Receive_Year, decimal VANDON_NUMBER, DateTime VANDON_DATE, string Source, string Custom_Register,
        decimal Declaration_Type, string STR_LIST_PRODUCTS)
        {
            try
            {
                Declaration_Info _ObjInfo = new Declaration_Info();
                _ObjInfo.Declaration_Code = Declaration_Code;
                _ObjInfo.Declaration_Id = Declaration_Id;
                _ObjInfo.Contract_Id = Contract_Id; _ObjInfo.Register_Date = Register_Date;
                _ObjInfo.Contract_Code = Contract_Code.Trim();
                _ObjInfo.Total_Value = Total_Value; _ObjInfo.WareHouse_Id = WareHouse_Id;
                _ObjInfo.WareHouse_Name = WareHouse_Name; _ObjInfo.Gate = Gate;
                _ObjInfo.Receive_Number = Receive_Number;
                _ObjInfo.Receive_Year = Receive_Year; _ObjInfo.VANDON_NUMBER = VANDON_NUMBER;
                _ObjInfo.VANDON_DATE = VANDON_DATE; _ObjInfo.VANDON_DATE = VANDON_DATE;
                _ObjInfo.Source = Source; _ObjInfo.Custom_Register = Custom_Register;
                _ObjInfo.Declaration_Type = Declaration_Type;
                _ObjInfo.Status = (decimal)NaviCommon.Enum_Contract_Status.ChoDuyet;
                _ObjInfo.Type = (decimal)NaviCommon.Enum_Declaration_Type.ToKhai_Nhap;
                _ObjInfo.Modified_Date = NaviCommon.CommonFuc.CurrentDate();
                _ObjInfo.Modified_By = SessionData.CurrentUser.User_Name;
                STR_LIST_PRODUCTS = STR_LIST_PRODUCTS.Trim().Trim('|');
                Product_Declaration_Info _SubInfo = new Product_Declaration_Info();
                List<Product_Declaration_Info> _ListProduct = new List<Product_Declaration_Info>();
                decimal _rel = 1;
                Declaration_BL _objBL = new Declaration_BL();
                _rel = _objBL.Declaration_Update(_ObjInfo);
                Product_Declaration_BL _PrDBL = new Product_Declaration_BL();
                bool _deleteProduct = true;
                if (_rel > 0)
                {
                    // xóa hết thằng produc đi truoc
                    _deleteProduct = _PrDBL.Delete_Product_Declaration_ByDeclaration_Id(Declaration_Id);
                }
                if (_rel > 0 && _deleteProduct == true)
                {
                    string[] _strProduct = STR_LIST_PRODUCTS.Split('|');
                    foreach (var _str in _strProduct)
                    {
                        _SubInfo = new Product_Declaration_Info();
                        string[] _temp = _str.Split(',');
                        if (_temp.Length == 5)
                        {
                            _SubInfo.Declaration_Id = Declaration_Id;
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

        public ActionResult LoadDataWarning()
        {
            try
            {
                User_Info currentUser = (User_Info)SessionData.CurrentUser;
                if (currentUser == null)
                    return Redirect("~/home/admin");


                List<Declaration_Info> lst_data = new List<Declaration_Info>();
                Declaration_BL _ObjBL = new Declaration_BL();
                int p_totalrecord = 0;

                lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name,
                    "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                    -1, "01/01/0001", -1, "01/01/0001",  "", "DECLARATION_CODE ASC",
                    1, 0, ref p_totalrecord);

                decimal _count_ChoDuyet = 0;
                decimal _count_DaDuyet = 0;
                decimal _count_HuyDuyet = 0;
                decimal _count_ThanhLy = 0;

                foreach (Declaration_Info item in lst_data)
                {
                    if (item.Status == (decimal)NaviCommon.Enum_Contract_Status.ChoDuyet)
                    {
                        _count_ChoDuyet++;
                    } 
                    else if (item.Status == (decimal)NaviCommon.Enum_Contract_Status.Huy_Duyet)
                    {
                        _count_HuyDuyet++;
                    } 
                }

              //  string str_result = _count_ChoDuyet + "|" + _count_HuyDuyet + "|" + _count_ThanhLy;
                string str_result = _count_ChoDuyet + "|" + _count_HuyDuyet ;
                _count_ChoDuyet = 0;
                _count_DaDuyet = 0;
                _count_HuyDuyet = 0;
                _count_ThanhLy = 0;
                lst_data = _ObjBL.DeclarationTableSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name,
                    "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Xuat),
                 -1, "01/01/0001", -1, "01/01/0001", "", "DECLARATION_CODE ASC",
                    1, 0, ref p_totalrecord);
                foreach (Declaration_Info item in lst_data)
                {
                    if (item.Status == (decimal)NaviCommon.Enum_Contract_Status.ChoDuyet)
                    {
                        _count_ChoDuyet++;
                    }
                    else if (item.Status == (decimal)NaviCommon.Enum_Contract_Status.Huy_Duyet)
                    {
                        _count_HuyDuyet++;
                    }                  
                }

                str_result = str_result + "|" + _count_ChoDuyet + "|" + _count_HuyDuyet;
                //  lấy số tờ khai thanh lý
                _count_ChoDuyet = 0;
                lst_data = _ObjBL.DeclarationLiquidationSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name,
                 "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                 -1, "01/01/0001", -1, "01/01/0001", "", "DECLARATION_CODE ASC",
                 1, 0, ref p_totalrecord);
                foreach (Declaration_Info item in lst_data)
                {
                    _count_ChoDuyet++;
                }
                str_result = str_result + "|" + _count_ChoDuyet;
                return Json(new { value = str_result });
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return Json(new { value = "" });
            }
        }


        public ActionResult LiquidationList()
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
                int p_start = Common.cNumberRecordOnPage * (1 - 1) + 1;
                int p_end = Common.cNumberRecordOnPage * 1;
                List<Declaration_Info> lst_data = new List<Declaration_Info>();
                Declaration_BL _ObjBL = new Declaration_BL();
                lst_data = _ObjBL.DeclarationLiquidationSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name, "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                 -1, "01/01/0001", -1, "01/01/0001", "",  "DECLARATION_CODE ASC",
                    p_start, p_end, ref p_totalrecord);
                ViewBag.Declaration = lst_data;
                List<Contracts_Info> lstContract = new List<Contracts_Info>();
                ContractBL _ContractjBL = new ContractBL();

                decimal Receive_Number = -1;
                decimal Receive_Year = -1;
                decimal WareHouse_Id = -1;
                decimal Business_Id = -1;

                lstContract = _ContractjBL.TableSearch("", DateTime.MinValue, Convert.ToInt16(NaviCommon.Enum_Contract_Status.Da_Duyet),
                    SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Name,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    "CONTRACT_CODE ASC",
                    0, 0, ref p_totalrecord);
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
        public ActionResult LiquidationSearch_Datas(int p_issearch, string p_keysearch, string p_orderby, string p_ordertype, int p_currentpage)
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
                        lst_data = _ObjBL.DeclarationLiquidationSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name,
                            arr_keysearch[0], arr_keysearch[1],
                            arr_keysearch[2], arr_keysearch[3], arr_keysearch[4], Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                            Convert.ToInt16(arr_keysearch[8]), arr_keysearch[5],
                            Convert.ToDecimal(arr_keysearch[6]), arr_keysearch[7], "",
                            p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                    }
                }
                else// TRƯỜNG HỢP KO CÓ GÌ THÌ LẤY TẤT
                {
                    lst_data = _ObjBL.DeclarationLiquidationSearch(SessionData.CurrentUser.User_Id.ToString(), SessionData.CurrentUser.User_Type, SessionData.CurrentUser.User_Name,
                        "", "", "", "", "", Convert.ToInt16(NaviCommon.Enum_Declaration_Type.ToKhai_Nhap),
                             -1, "01/01/0001",
                            -1, "01/01/0001", "", p_orderby + " " + p_ordertype, p_start, p_end, ref p_totalrecord);
                }
                ViewBag.Paging = HtmlControllHelpers.PagingData(p_currentpage, Common.cNumberRecordOnPage, p_totalrecord);
                ViewBag.RecordOnPage = Common.cNumberRecordOnPage;
                ViewBag.IsSearch = p_issearch;
                ViewBag.Declaration = lst_data;
                return PartialView("LiquidationDataTable");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return PartialView("LiquidationDataTable");
            }
        }

    
    }
}
