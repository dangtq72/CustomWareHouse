using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ZetaCompressionLibrary;


namespace CW_Service
{

    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] FunctionsGetAll();

        [OperationContract()]
        byte[] GetUserFuncByUserID(decimal pIdUser);

        [OperationContract()]
        byte[] FunctionsGetBy_Type(string p_func_type);
    }

    public partial class CWService : CWContractService
    {

        /// <summary>
        /// DANH SACH CHỨC NĂNG LOAD HẾT LÊN LUÔN FULL CHẮC TẦM 1000 BẢN GHI 
        /// </summary>
        /// <returns></returns>
        public byte[] FunctionsGetAll()
        {
            try
            {
                FunctionsDA _ObjDA = new FunctionsDA();
                DataSet _ds = _ObjDA.FunctionsGetAll();
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
        /// LAY DANH SACH CAC CHUC NANG THEO USER DANG NHAP 
        /// LAY LEN KHI ĐANG NHẬP USER 
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <returns></returns>
        public byte[] GetUserFuncByUserID(decimal pIdUser)
        {
            try
            {
                FunctionsDA _ObjDA = new FunctionsDA();
                DataSet _ds = _ObjDA.GetUserFuncByUserID(pIdUser);
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

 
        public byte[] FunctionsGetBy_Type(string p_func_type)
        {
            try
            {
                FunctionsDA _ObjDA = new FunctionsDA();
                DataSet _ds = _ObjDA.FunctionsGetBy_Type(p_func_type);
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
