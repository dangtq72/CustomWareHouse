using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_Service;
using System.ServiceModel;
using ZetaCompressionLibrary;

namespace CW_Service
{
    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] GroupUserGetByUserID(decimal pIdUser);

        [OperationContract()]
        byte[] GroupUserGetNotInUserID(decimal pIdUser);


        [OperationContract()]
        decimal GroupUserInsert(decimal p_id_user, decimal p_id_group, string p_createby, DateTime p_createdate);

        [OperationContract()]
        decimal GroupUserDeleted(decimal pIdUser, decimal pIdGroup);

        [OperationContract()]
        byte[] GetUserOfGroupByIDGroup(decimal pIDGroup);

    }

    public partial class CWService : CWContractService
    {
        /// <summary>
        /// Lay danh sach cac nhom theo user dang nhap 
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <returns></returns>
        public byte[] GroupUserGetByUserID(decimal pIdUser)
        {
            try
            {
                GroupUserDA _ObjDA = new GroupUserDA();
                DataSet _ds = _ObjDA.GroupUserGetByUserID(pIdUser);
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


        public byte[] GroupUserGetNotInUserID(decimal pIdUser)
        {
            try
            {
                GroupUserDA _ObjDA = new GroupUserDA();
                DataSet _ds = _ObjDA.GroupUserGetNotInUserID(pIdUser);
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


        public decimal GroupUserInsert(decimal p_id_user, decimal p_id_group, string p_createby, DateTime p_createdate)
        {
            try
            {
                GroupUserDA _ObjDA = new GroupUserDA();
                return _ObjDA.GroupUserInsert(p_id_user, p_id_group, p_createby, p_createdate);
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
                GroupUserDA _ObjDA = new GroupUserDA();
                return _ObjDA.GroupUserDeleted(pIdUser, pIdGroup);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        /// <summary>
        /// Lấy danh sach user thuộc nhóm 
        /// </summary>
        /// <param name="pIDGroup"></param>
        /// <returns></returns>
        public byte[] GetUserOfGroupByIDGroup(decimal pIDGroup)
        {
            try
            {
                GroupUserDA _ObjDA = new GroupUserDA();
                DataSet _ds = _ObjDA.GetUserOfGroupByIDGroup(pIDGroup);
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

    }
}
