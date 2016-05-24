using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using ZetaCompressionLibrary;

namespace CW_Business
{
    public class Declaration_BL
    {

        public Declaration_Info DeclarationGetById(decimal p_id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.DeclarationGetById(p_id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Declaration_Info> _list = NaviCommon.CBO<Declaration_Info>.FillCollectionFromDataSet(_ds);
                Declaration_Info _ObjInfo = new Declaration_Info();
                if (_list.Count > 0)
                {
                    _ObjInfo = _list[0];
                }
                List<Product_Declaration_Info> _listPro = new List<Product_Declaration_Info>();
                if (_ds.Tables.Count > 1)
                {
                    _listPro = NaviCommon.CBO<Product_Declaration_Info>.FillCollectionFromDataTable(_ds.Tables[1]);
                }
                if (_listPro != null)
                {
                    _ObjInfo.ListProduct = _listPro;
                }
                return _ObjInfo;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Declaration_Info();
            }
        }

        public List<Declaration_Info> Declaration_GetAll()
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Declaration_GetAll();
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Declaration_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Declaration_Info>();
            }
        }

        public List<Declaration_Info> Declaration_GetByCode(string p_Declaration_Code)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Declaration_GetByCode(p_Declaration_Code);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Declaration_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Declaration_Info>();
            }
        }

        public List<Declaration_Info> Declaration_GetByContract(decimal p_Contract_Id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.Declaration_GetByContract(p_Contract_Id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<Declaration_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Declaration_Info>();
            }
        }

        public bool Declaration_Delete(decimal p_Declaration_Id)
        {
            try
            {
                return CommonData.c_serviceWCF.Declaration_Delete(p_Declaration_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Declaration_Update_Status(decimal Declaration_Id, decimal Status, string Reason, string Approve_By)
        {
            try
            {
                return CommonData.c_serviceWCF.Declaration_Update_Status(Declaration_Id, Status, Reason, Approve_By);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public decimal Declaration_Insert(Declaration_Info Declaration_Info)
        {
            try
            {
                return CommonData.c_serviceWCF.Declaration_Insert(Declaration_Info.Declaration_Code, Declaration_Info.Contract_Id, Declaration_Info.Contract_Code, Declaration_Info.Type,
                    Declaration_Info.Declaration_Type, Declaration_Info.Register_Date, Declaration_Info.Money_Type, Declaration_Info.Total_Value, Declaration_Info.WareHouse_Id, Declaration_Info.WareHouse_Name, Declaration_Info.WareHouse_Location, Declaration_Info.Gate,
                    Declaration_Info.Receive_Number, Declaration_Info.Receive_Year, Declaration_Info.VANDON_NUMBER, Declaration_Info.VANDON_DATE,
                    Declaration_Info.Status, Declaration_Info.Source, Declaration_Info.Declaration_Refence_Id, Declaration_Info.Declaration_Refence_Code, Declaration_Info.Custom_Register, Declaration_Info.Created_By, Declaration_Info.Created_Date);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal Declaration_Update(Declaration_Info Declaration_Info)
        {
            try
            {
                return CommonData.c_serviceWCF.Declaration_Update(Declaration_Info.Declaration_Id, Declaration_Info.Declaration_Type,
                    Declaration_Info.Register_Date, Declaration_Info.Money_Type, Declaration_Info.Total_Value, Declaration_Info.WareHouse_Id, Declaration_Info.WareHouse_Name, Declaration_Info.WareHouse_Location, Declaration_Info.Gate,
                    Declaration_Info.Receive_Number, Declaration_Info.Receive_Year, Declaration_Info.VANDON_NUMBER, Declaration_Info.VANDON_DATE,
                    Declaration_Info.Source, Declaration_Info.Custom_Register, Declaration_Info.Modified_By, Declaration_Info.Modified_Date, Declaration_Info.Status);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public List<Declaration_Info> DeclarationTableSearch(string USER_ID, decimal USER_TYPE, string USER_NAME, string DECLARATION_CODE, string CONTRACT_CODE, string RECEIVE_NUMBER, string RECEIVE_YEAR, string SO_VAN_DON,
            int TYPE, int STATUS, string REGISTER_DATE, decimal WAREHOUSE_ID, string NGAY_VAN_DON, string DECLARATION_REFENCE_CODE,
            string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.DeclarationTableSearch(USER_ID, USER_TYPE, USER_NAME, DECLARATION_CODE, CONTRACT_CODE, RECEIVE_NUMBER,
                    RECEIVE_YEAR , SO_VAN_DON,
                 TYPE, STATUS, REGISTER_DATE, WAREHOUSE_ID, NGAY_VAN_DON, DECLARATION_REFENCE_CODE,
                 p_order_by, p_start, p_end, ref p_total);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                return NaviCommon.CBO<Declaration_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Declaration_Info>();
            }
        }

        public List<Declaration_Info> DeclarationLiquidationSearch(string USER_ID, decimal USER_TYPE, string USER_NAME, string DECLARATION_CODE, string CONTRACT_CODE, string RECEIVE_NUMBER, string RECEIVE_YEAR, string SO_VAN_DON,
           int TYPE, int STATUS, string REGISTER_DATE, decimal WAREHOUSE_ID, string NGAY_VAN_DON, string DECLARATION_REFENCE_CODE,
           string p_order_by, int p_start, int p_end, ref int p_total)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.DeclarationLiquidationSearch(USER_ID, USER_TYPE, USER_NAME, DECLARATION_CODE, CONTRACT_CODE, RECEIVE_NUMBER,
                    RECEIVE_YEAR, SO_VAN_DON,
                 TYPE, STATUS, REGISTER_DATE, WAREHOUSE_ID, NGAY_VAN_DON, DECLARATION_REFENCE_CODE,
                 p_order_by, p_start, p_end, ref p_total);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                return NaviCommon.CBO<Declaration_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Declaration_Info>();
            }
        }

    }
}
