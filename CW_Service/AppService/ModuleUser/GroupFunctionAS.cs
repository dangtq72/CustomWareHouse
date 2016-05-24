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
        byte[] FunctionInGroupsGetAll(decimal pIdGroup);

        [OperationContract()]
        byte[] FunctionGroupsGetAll();

        [OperationContract()]
        byte[] FunctionNotInGroupsGetAll(decimal pIdGroup, decimal pIdUser, decimal pCreateOwner);

        [OperationContract()]
        byte[] FunctionNotInGroupsByAppGetAll(decimal pIdGroup, decimal pIdUser, decimal pCreateOwner);

        [OperationContract()]
        decimal GroupFunctionInsert(decimal p_id_group, string p_lst_function, string p_createby, DateTime p_createdate);
    }

    public partial class CWService : CWContractService
    {
        /// <summary>
        /// DANH SACH CAC CHUC NANG THUOC NHOM 
        /// </summary>
        /// <param name="pIdGroup"></param>
        /// <returns></returns>
        public byte[] FunctionInGroupsGetAll(decimal pIdGroup)
        {
            try
            {
                GroupFunctionDA _ObjDA = new GroupFunctionDA();
                DataSet _ds = _ObjDA.FunctionInGroupsGetAll(pIdGroup);
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
        /// laays toan bo danh sach chuc nang ko dung vi thu tuc dang lay sai
        /// </summary>
        /// <returns></returns>
        public byte[] FunctionGroupsGetAll()
        {
            try
            {
                GroupFunctionDA _ObjDA = new GroupFunctionDA();
                DataSet _ds = _ObjDA.FunctionGroupsGetAll();
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
        /// DANH SACH CAC CHUC NANG KHONG THUOC NHOM 
        /// </summary>
        /// <param name="pIdGroup"></param>
        /// <returns></returns>
        public byte[] FunctionNotInGroupsGetAll(decimal pIdGroup, decimal pIdUser, decimal pCreateOwner)
        {
            try
            {
                GroupFunctionDA _ObjDA = new GroupFunctionDA();
                DataSet _ds = _ObjDA.FunctionNotInGroupsGetAll(pIdGroup, pIdUser, pCreateOwner);
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
        /// DANH SACH CAC CHUC NANG KHONG THUOC NHOM LỌC THEO MÃ ỨNG DỤNG
        /// </summary>
        /// <param name="pIdGroup"></param>
        /// <returns></returns>
        public byte[] FunctionNotInGroupsByAppGetAll(decimal pIdGroup, decimal pIdUser, decimal pCreateOwner)
        {
            try
            {
                GroupFunctionDA _ObjDA = new GroupFunctionDA();
                DataSet _ds = _ObjDA.FunctionNotInGroupsByAppGetAll(pIdGroup, pIdUser, pCreateOwner);
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


        public decimal GroupFunctionInsert(decimal p_id_group, string p_lst_function, string p_createby, DateTime p_createdate)
        {
            try
            {
                GroupFunctionDA _ObjDA = new GroupFunctionDA();
                return _ObjDA.GroupFunctionInsert(p_id_group, p_lst_function, p_createby, p_createdate);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }
    }
}
