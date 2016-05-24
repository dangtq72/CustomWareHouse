using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using ZetaCompressionLibrary;

namespace CW_Service
{
    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] Category_GetAll();

        [OperationContract()]
        byte[] Category_GetById(decimal Category_Id);

        [OperationContract()]
        byte[] Category_GetByCode(string Category_Code);

        [OperationContract()]
        byte[] Category_Search(string p_name);

        [OperationContract()]
        decimal Category_Delete(decimal Category_Id);

        [OperationContract()]
        bool Category_Insert(string Category_Code, string Category_Name, string Unit);

        [OperationContract()]
        bool Category_Update(decimal Category_Id, string Category_Name, string Unit);
    }

    public partial class CWService : CWContractService
    {
        public byte[] Category_GetAll()
        {
            try
            {
                Categories_DA _objDA = new Categories_DA();
                DataSet _ds = _objDA.Category_GetAll();
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Category_GetById(decimal Category_Id)
        {
            try
            {
                Categories_DA _objDA = new Categories_DA();
                DataSet _ds = _objDA.Category_GetById(Category_Id);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Category_GetByCode(string Category_Code)
        {
            try
            {
                Categories_DA _objDA = new Categories_DA();
                DataSet _ds = _objDA.Category_GetByCode(Category_Code);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] Category_Search(string p_name)
        {
            try
            {
                Categories_DA _objDA = new Categories_DA();
                DataSet _ds = _objDA.Category_Search(p_name);
                return CompressionHelper.CompressDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }


        public decimal Category_Delete(decimal Category_Id)
        {
            try
            {
                Categories_DA _objDA = new Categories_DA();
                return _objDA.Category_Delete(Category_Id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public bool Category_Insert(string Category_Code, string Category_Name, string Unit)
        {
            try
            {
                Categories_DA _objDA = new Categories_DA();
                return _objDA.Category_Insert(Category_Code, Category_Name, Unit);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Category_Update(decimal Category_Id, string Category_Name, string Unit)
        {
            try
            {
                Categories_DA _objDA = new Categories_DA();
                return _objDA.Category_Update(Category_Id, Category_Name, Unit);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return false;
            }
        }

    }
}
