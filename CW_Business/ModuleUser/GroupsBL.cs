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
    public class GroupsBL
    {

        /// <summary>
        /// LAY DANH SÁCH NHÓM 
        /// </summary>
        /// <returns></returns>
        public List<Groups_Info> GroupsGetAll()
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.GroupInfoGetAll();
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Groups_Info> lstGroupsInfo = NaviCommon.CBO<Groups_Info>.FillCollectionFromDataSet(_ds);
                return lstGroupsInfo;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Groups_Info>();
            }
        }


        /// <summary>
        /// THEM MOI GROUPS
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns>-2 DA CO ROI KO THEM DC , ID CUA BAN GHI DA THEM MOI</returns>
        public decimal GroupsInsert(string p_name, string p_note, string p_create_by, DateTime p_create_date, decimal pIdUser)
        {
            try
            {
                return CommonData.c_serviceWCF.GroupInfoInsert(p_name, p_note, p_create_by, p_create_date, pIdUser);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        /// <summary>
        /// SUA  CHUC NANG 
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns>-2 DA CO ROI KO THEM DC , ID CUA BAN GHI DA THEM MOI</returns>
        public decimal GroupsUpdate(int p_id, string p_name, string p_note, string p_modified_by, DateTime p_modified_date)
        {
            try
            {
                return CommonData.c_serviceWCF.GroupInfoUpdate(p_id, p_name, p_note, p_modified_by, p_modified_date);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        /// <summary>
        /// XOA CHUC NANG 
        /// </summary>
        /// <param name="pId"></param>
        /// <returns>-2 DANG SU DUNG NHOM CHUC NANG KO DC PHEP XOA </returns>
        public decimal GroupsDelete(int pId)
        {
            try
            {
                return CommonData.c_serviceWCF.GroupInfoDelete(pId);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

    }
}
