using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ZetaCompressionLibrary;

namespace CW_Service
{

    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] WAREHOUSE_GET_ALL();

        [OperationContract()]
        byte[] WareHouse_GetById(decimal WareHouse_Id);

        [OperationContract()]
        byte[] WareHouse_GetByUser(decimal User_Id, decimal User_Type);

        [OperationContract()]
        byte[] WareHouse_Search(string p_name);

        [OperationContract()]
        decimal WareHouse_Delete(decimal WareHouse_Id);

        [OperationContract()]
        decimal WareHouse_Insert(string WareHouse_Code, string WareHouse_Name, string Location);

        [OperationContract()]
        bool WareHouse_Update(decimal WareHouse_Id, string WareHous_Name, string Location);
    }

    public partial class CWService : CWContractService
    {
        public byte[] WAREHOUSE_GET_ALL()
        {
            try
            {
                WareHouse_DA _objDA = new WareHouse_DA();
                DataSet _ds = _objDA.WAREHOUSE_GET_ALL();
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] WareHouse_GetById(decimal WareHouse_Id)
        {
            try
            {
                WareHouse_DA _objDA = new WareHouse_DA();
                DataSet _ds = _objDA.WareHouse_GetById(WareHouse_Id);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] WareHouse_GetByUser(decimal User_Id, decimal User_Type)
        {
            try
            {
                WareHouse_DA _objDA = new WareHouse_DA();
                DataSet _ds = _objDA.WareHouse_GetByUser(User_Id, User_Type);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }



        public byte[] WareHouse_Search(string p_name)
        {
            try
            {
                WareHouse_DA _objDA = new WareHouse_DA();
                DataSet _ds = _objDA.WareHouse_Search(p_name);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }


        public decimal WareHouse_Delete(decimal WareHouse_Id)
        {
            try
            {
                WareHouse_DA _objDA = new WareHouse_DA();
                decimal _ck = _objDA.WareHouse_Delete(WareHouse_Id);
                if (_ck == 1)
                    DBMemory.GetWareHouse();
                return _ck;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal WareHouse_Insert(string WareHouse_Code, string WareHouse_Name, string Location)
        {
            try
            {
                WareHouse_DA _objDA = new WareHouse_DA();
                decimal _ck = _objDA.WareHouse_Insert(WareHouse_Code, WareHouse_Name, Location);
                if (_ck == 0)
                    DBMemory.GetWareHouse();
                return _ck;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool WareHouse_Update(decimal WareHouse_Id, string WareHous_Name, string Location)
        {
            try
            {
                WareHouse_DA _objDA = new WareHouse_DA();
                bool _ck = _objDA.WareHouse_Update(WareHouse_Id, WareHous_Name, Location);
                if (_ck)
                    DBMemory.GetWareHouse();
                return _ck;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
