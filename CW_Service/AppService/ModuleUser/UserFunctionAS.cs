using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ZetaCompressionLibrary;
using CW_Service;

namespace CW_Service
{
    public partial interface CWContractService
    {
        [OperationContract()]
        decimal UserFunctionInsert(decimal p_id_user, decimal p_func_id, string p_lst_function, decimal p_id_group, string p_created_by);

        [OperationContract()]
        decimal UserFunctionDeleted(decimal p_id_user, decimal p_id_function, decimal p_id_group);

        [OperationContract()]
        byte[] UserFunctionGetAll(decimal pIdUser, decimal pIdGroup);
    }

    public partial class CWService : CWContractService
    {
        public decimal UserFunctionInsert(decimal p_id_user, decimal p_func_id, string p_lst_function, decimal p_id_group, string p_created_by)
        {
            try
            {
                UserFunctionDA _ObjDa = new UserFunctionDA();
                return _ObjDa.UserFunctionInsert(p_id_user, p_func_id, p_lst_function, p_id_group, p_created_by);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        public decimal UserFunctionDeleted(decimal p_id_user, decimal p_id_function, decimal p_id_group)
        {
            try
            {
                UserFunctionDA _ObjDa = new UserFunctionDA();
                return _ObjDa.UserFunctionDeleted(p_id_user, p_id_function, p_id_group);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        public byte[] UserFunctionGetAll(decimal pIdUser, decimal pIdGroup)
        {
            try
            {
                UserFunctionDA _ObjDa = new UserFunctionDA();
                DataSet _ds = _ObjDa.UserFunctionGetAll(pIdUser, pIdGroup);
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
