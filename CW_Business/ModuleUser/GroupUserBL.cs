using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_Info;
using ZetaCompressionLibrary;
using NaviCommon;


namespace CW_Business
{
    public class GroupUserBL
    {

        /// <summary>
        /// DANH SACH NHOM MA USER THUOC NHOM
        /// </summary>
        /// <param name="pIdUser">ID USER </param>
        /// <returns>DANH SACH CAC NHOM THUOC USER </returns>
        public List<GroupUser_Info> GroupUserGetByUserID(int pIdUser)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.GroupUserGetByUserID(pIdUser);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<GroupUser_Info> lstGroupsUserInfo = NaviCommon.CBO<GroupUser_Info>.FillCollectionFromDataSet(_ds);
                return lstGroupsUserInfo;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<GroupUser_Info>();
            }
        }


        /// <summary>
        /// Lay danh sach user theo group 
        /// </summary>
        /// <param name="pIDGroup"></param>
        /// <returns></returns>
        public List<User_Info> GetUserOfGroupByIDGroup(int pIDGroup)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.GetUserOfGroupByIDGroup(pIDGroup);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<User_Info> lstGroupsInfo = NaviCommon.CBO<User_Info>.FillCollectionFromDataSet(_ds);
                return lstGroupsInfo;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<User_Info>();
            }
        }

        public List<Groups_Info> GroupUserGetNotInUserID(int pIdUser, int PIdUserCurrent)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.GroupUserGetNotInUserID(pIdUser);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Groups_Info> lstGroupsUserInfo = NaviCommon.CBO<Groups_Info>.FillCollectionFromDataSet(_ds);
                return lstGroupsUserInfo;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Groups_Info>();
            }
        }


        public decimal GroupUserInsert(GroupUser_Info pInfo)
        {
            try
            {
                return CommonData.c_serviceWCF.GroupUserInsert(pInfo.User_Id, pInfo.Group_Id, pInfo.CreateBy, pInfo.CreateDate);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }


        public decimal GroupUserDeleted(decimal pIdUser, decimal pIdGroup)
        {
            try
            {
                return CommonData.c_serviceWCF.GroupUserDeleted(pIdUser, pIdGroup);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

    }
}
