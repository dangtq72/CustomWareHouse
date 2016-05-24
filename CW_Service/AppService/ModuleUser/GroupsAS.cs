using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CW_Service;
using ZetaCompressionLibrary;

namespace CW_Service
{
    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] GroupInfoGetAll();

        [OperationContract()]
        decimal GroupInfoInsert(string p_name, string p_note, string p_createby, DateTime p_createdate, decimal pIdUser);


        [OperationContract()]
        decimal GroupInfoUpdate(decimal p_id, string p_name, string p_note, string p_modifiedby, DateTime p_modifieddate);

        [OperationContract()]
        decimal GroupInfoDelete(decimal pId);

    }

    public partial class CWService : CWContractService
    {
        
        public byte[] GroupInfoGetAll( )
        {
            try
            {
                GroupsDA _ObjDA = new GroupsDA();
                DataSet _ds = _ObjDA.GroupInfoGetAll();
                byte[] byteReturn;
                byteReturn = CompressionHelper.CompressDataSet(_ds);
                return byteReturn;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }


        /// <summary>
        /// INSERT NHÓM 
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns>-2 NHÓM ĐÃ TỒN TẠI, ID BẢN GHI INSERT THÀNH CÔNG</returns>
        public decimal GroupInfoInsert(string p_name, string p_note, string p_createby, DateTime p_createdate, decimal pIdUser)
        {
            try
            {
                GroupsDA _ObjDA = new GroupsDA();
                return _ObjDA.GroupInfoInsert(p_name, p_note, p_createby, p_createdate, pIdUser);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        /// <summary>
        /// UPDATE NHÓM
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns>-2 NẾU NHÓM CÓ RỒI DỰA THEO TÊN MAP , ID BẢN GHI CẬP NHẬT THÀNH CÔNG </returns>
        public decimal GroupInfoUpdate(decimal p_id, string p_name, string p_note, string p_modifiedby, DateTime p_modifieddate)
        {
            try
            {
                GroupsDA _ObjDA = new GroupsDA();
                return _ObjDA.GroupInfoUpdate(p_id, p_name, p_note, p_modifiedby, p_modifieddate);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        /// <summary>
        /// Xóa Nhóm CHức Năng 
        /// </summary>
        /// <param name="pId"></param>
        /// <returns> (>0)ID ban ghi đã xóa -2 đang có trong bảng group_user ko được phép xóa </returns>
        public decimal GroupInfoDelete(decimal pId)
        {
            try
            {
                GroupsDA _ObjDA = new GroupsDA();
                return _ObjDA.GroupInfoDelete(pId);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

    }
}
