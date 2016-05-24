using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using ZetaCompressionLibrary;

namespace CW_Business
{
    public class ContractBL
    {
        public List<Contracts_Info> Contract_GetAll()
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Contract_GetAll();
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Contracts_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Contracts_Info>();
            }
        }

        public List<Contracts_Info> Contract_GetByStatus(int p_status)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Contract_GetByStatus(p_status);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                return NaviCommon.CBO<Contracts_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Contracts_Info>();
            }
        }

        public List<Contracts_Info> TableSearch(string CONTRACT_CODE, DateTime FR0MDATE, int p_status, decimal User_Type, string User_Id, string Created_By,
            decimal Receive_Number, decimal Receive_Year, decimal WareHouse_Id, decimal Business_Id,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.TableSearch(CONTRACT_CODE, FR0MDATE, p_status, User_Type, User_Id, Created_By,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    p_order_by, p_start, p_end, ref p_total);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                return NaviCommon.CBO<Contracts_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Contracts_Info>();
            }
        }

        public List<Contracts_Info> Contract_Search_ThanhKhoan(string CONTRACT_CODE, DateTime FR0MDATE, int p_status, decimal User_Type, string User_Id, string Created_By,
            decimal Receive_Number, decimal Receive_Year, decimal WareHouse_Id, decimal Business_Id,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Contract_Search_ThanhKhoan(CONTRACT_CODE, FR0MDATE, p_status, User_Type, User_Id, Created_By,
                    Receive_Number, Receive_Year, WareHouse_Id, Business_Id,
                    p_order_by, p_start, p_end, ref p_total);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                return NaviCommon.CBO<Contracts_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Contracts_Info>();
            }
        }

        public List<Contracts_Info> Contract_GetByCode(string p_Contract_Code)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Contract_GetByCode(p_Contract_Code);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Contracts_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Contracts_Info>();
            }
        }

        public  Contracts_Info  Contract_GetById(decimal p_Contract_Id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Contract_GetById(p_Contract_Id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                Contracts_Info _objInfo = new Contracts_Info();
                 List<Contracts_Info> _list = NaviCommon.CBO<Contracts_Info>.FillCollectionFromDataSet(_ds);
                 if (_list.Count > 0)
                 {
                     _objInfo = _list[0];
                 }
                 return _objInfo;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Contracts_Info();
            }
        }

        public List<Contracts_Info> Contract_GetBy_WareHouse(decimal WareHouse_Id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Contract_GetBy_WareHouse(WareHouse_Id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Contracts_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Contracts_Info>();
            }
        }

        public List<Contracts_Info> Contract_GetBy_User(string User_Name)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Contract_GetBy_User(User_Name);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Contracts_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Contracts_Info>();
            }
        }

        public decimal Contract_Delete(decimal p_Contract_Id)
        {
            try
            {
                return CommonData.c_serviceWCF.Contract_Delete(p_Contract_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool Contract_Update_Status(decimal Contract_Id, decimal Status, string Reason, string Approve_By)
        {
            try
            {
                return CommonData.c_serviceWCF.Contract_Update_Status(Contract_Id, Status, Reason, Approve_By);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Contract_Insert(Contracts_Info Contracts_Info)
        {
            try
            {
                return CommonData.c_serviceWCF.Contract_Insert(Contracts_Info.Contract_Code, Contracts_Info.Register_Date, Contracts_Info.Period,
                    Contracts_Info.Receive_Year, Contracts_Info.WareHouse_Id, Contracts_Info.WareHouse_Name, Contracts_Info.Business_Id,
                    Contracts_Info.Money_Type, Contracts_Info.Status, Contracts_Info.Created_By, Contracts_Info.Created_Date, Contracts_Info.Due_Date);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Contract_Update(Contracts_Info Contracts_Info)
        {
            try
            {
                return CommonData.c_serviceWCF.Contract_Update(Contracts_Info.Contract_Id, Contracts_Info.Register_Date, Contracts_Info.Period,
                    Contracts_Info.Receive_Year, Contracts_Info.WareHouse_Id, Contracts_Info.WareHouse_Name, Contracts_Info.Business_Id,
                    Contracts_Info.Money_Type, Contracts_Info.Modified_By, Contracts_Info.Modified_Date, Contracts_Info.Status, Contracts_Info.Due_Date);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
