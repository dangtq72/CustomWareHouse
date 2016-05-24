using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ZetaCompressionLibrary;

namespace CW_Service
{
    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] User_WareHouse_GetByUser(decimal p_user_id);

        [OperationContract()]
        bool User_WareHouse_Insert(decimal p_user_id, decimal p_warehouse_id, string p_created_by, DateTime p_created_date);

        [OperationContract()]
        decimal WareHouse_DeleteByUser(decimal p_user_id);
    }

    public partial class CWService : CWContractService
    {
        public byte[] User_WareHouse_GetByUser(decimal p_user_id)
        {
            try
            {
                User_WareHouse_DA _ObjDA = new User_WareHouse_DA();
                DataSet _ds = _ObjDA.User_WareHouse_GetByUser(p_user_id);
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

        public bool User_WareHouse_Insert(decimal p_user_id, decimal p_warehouse_id, string p_created_by, DateTime p_created_date)
        {
            try
            {
                User_WareHouse_DA _ObjDA = new User_WareHouse_DA();
                return _ObjDA.User_WareHouse_Insert(p_user_id, p_warehouse_id, p_created_by, p_created_date);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public decimal WareHouse_DeleteByUser(decimal p_user_id)
        {
            try
            {
                User_WareHouse_DA _ObjDA = new User_WareHouse_DA();
                return _ObjDA.WareHouse_DeleteByUser(p_user_id);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
