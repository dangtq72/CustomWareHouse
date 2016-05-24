using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_Info;
using ZetaCompressionLibrary;


namespace CW_Business
{
    public class GroupFunctionBL
    {
        /// <summary>
        /// DANH SACH CAC CHUC NANG THUOC NHOM 
        /// </summary>
        /// <param name="pIdGroup"></param>
        /// <returns></returns>
        public Hashtable FunctionInGroupsGetAll(decimal pIdGroup)
        {
            try
            {
                Hashtable chashtble = new Hashtable();
                byte[] byteRecive = CommonData.c_serviceWCF.FunctionInGroupsGetAll(pIdGroup);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Functions_Info> lstGroupsInfo = NaviCommon.CBO<Functions_Info>.FillCollectionFromDataSet(_ds);
                foreach (Functions_Info item in lstGroupsInfo)
                {
                    chashtble[item.Func_Id] = item;
                }
                return chashtble;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Hashtable();
            }
        }

        public List<Functions_Info> FunctionInGroupsGetAllToList(decimal pIdGroup)
        {
            try
            {

                byte[] byteRecive = CommonData.c_serviceWCF.FunctionInGroupsGetAll(pIdGroup);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Functions_Info> lstGroupsInfo = NaviCommon.CBO<Functions_Info>.FillCollectionFromDataSet(_ds);
                return lstGroupsInfo;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Functions_Info>();
            }
        }


        /// <summary>
        /// DANH SACH CAC CHUC NANG KHONG THUOC NHOM 
        /// </summary>
        /// <param name="pIdGroup"></param>
        /// <returns></returns>
        public List<Functions_Info> FunctionNotInGroupsGetAll(decimal pIdGroup, decimal pIdUser, decimal pCreateOwner = 1)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.FunctionNotInGroupsGetAll(pIdGroup, pIdUser, pCreateOwner);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Functions_Info> lstGroupsInfo = NaviCommon.CBO<Functions_Info>.FillCollectionFromDataSet(_ds);
                return lstGroupsInfo;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Functions_Info>();
            }
        }

        /// <summary>
        /// DANH SACH CAC CHUC NANG KHONG THUOC NHOM và lấy theo ứng dụng
        /// </summary>
        /// <param name="pIdGroup"></param>
        /// <returns></returns>
        public List<Functions_Info> FunctionNotInGroupsByAppGetAll(decimal pIdGroup, decimal pIdUser, decimal pCreateOwner = 1)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.FunctionNotInGroupsByAppGetAll(pIdGroup, pIdUser, pCreateOwner);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Functions_Info> lstGroupsUserInfo = NaviCommon.CBO<Functions_Info>.FillCollectionFromDataSet(_ds);
                return lstGroupsUserInfo;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Functions_Info>();
            }
        }

        /// <summary>
        /// THEM MOI FUNCTION VAO NHOM 
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        public decimal GroupFunctionInsert(GroupFunction_Info pInfo)
        {
            try
            {
                return CommonData.c_serviceWCF.GroupFunctionInsert(pInfo.Group_Id, pInfo.lstFunction, pInfo.CreateBy, pInfo.CreateDate);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

    }
}
