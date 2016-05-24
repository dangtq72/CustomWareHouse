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
        byte[] UserInfo_GetById(decimal p_id);

        [OperationContract()]
        byte[] UserInfo_GetByType(decimal p_type);

        [OperationContract()]
        byte[] UserInfo_GetByName(string p_name);

        [OperationContract()]
        byte[] UserInfo_Search(string name, string user_type, string status, string Custom_Id, string column,
            string type_sort, string start, string end, ref decimal P_Total);

        [OperationContract()]
        decimal UserInfo_Insert(string user_name, string password, string fullname, decimal Custom_Id, decimal user_type, decimal status, string Phone, string Email);

        [OperationContract()]
        decimal UserInfo_SetPassword(int p_user_id, int p_status, string p_password);

        [OperationContract()]
        decimal UserInfo_SetStatus(int p_user_id, int p_status);

        [OperationContract()]
        decimal UserInfo_Delete(int p_user_id);

        [OperationContract()]
        decimal UserInfo_Update_Pass(int p_user_id, string p_pass, DateTime p_last_pass);
    }
    public partial class CWService : CWContractService
    {

        public byte[] UserInfo_GetById(decimal p_id)
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                byte[] byteReturn;
                DataSet _ds = _UserInfoDA.UserInfo_GetById(p_id);
                byteReturn = CompressionHelper.CompressDataSet(_ds);
                return byteReturn;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] UserInfo_GetByType(decimal p_type)
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                byte[] byteReturn;
                DataSet _ds = _UserInfoDA.UserInfo_GetByType(p_type);

                byteReturn = CompressionHelper.CompressDataSet(_ds);
                return byteReturn;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] UserInfo_GetByName(string p_name)
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                byte[] byteReturn;
                DataSet _ds = _UserInfoDA.UserInfo_GetByName(p_name);
                byteReturn = CompressionHelper.CompressDataSet(_ds);
                return byteReturn;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public byte[] UserInfo_Search(string name, string user_type, string status, string Custom_Id, string column,
            string type_sort, string start, string end, ref decimal P_Total)
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                byte[] byteReturn;
                DataSet _ds = _UserInfoDA.UserInfo_Search(name, user_type, status, Custom_Id, column, type_sort, start, end, ref P_Total);
                byteReturn = CompressionHelper.CompressDataSet(_ds);
                return byteReturn;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

        public decimal UserInfo_Insert(string user_name, string password, string fullname, decimal Custom_Id, 
            decimal user_type, decimal status, string Phone, string Email)
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                decimal _ck = _UserInfoDA.UserInfo_Insert(user_name, password, fullname, Custom_Id, user_type, status, Phone, Email);

                if (_ck == 0 && user_type == (decimal)NaviCommon.Enum_User_Type.DoanhNghiep)
                    DBMemory.GetUser_DoanhNghiep();
                return _ck;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }

        }

        public decimal UserInfo_SetPassword(int p_user_id, int p_status, string p_password)
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                return _UserInfoDA.UserInfo_SetPassword(p_user_id, p_status, p_password);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal UserInfo_SetStatus(int p_user_id, int p_status)
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                return _UserInfoDA.UserInfo_SetStatus(p_user_id, p_status);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal UserInfo_Delete(int p_user_id)
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                decimal _ck = _UserInfoDA.UserInfo_Delete(p_user_id);
                DBMemory.GetUser_DoanhNghiep();
                return _ck;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal UserInfo_Update_Pass(int p_user_id, string p_pass, DateTime p_last_pass)
        {
            try
            {
                UserInfoDA _UserInfoDA = new UserInfoDA();
                return _UserInfoDA.UserInfo_Update_Pass(p_user_id, p_pass, p_last_pass);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

    }
}