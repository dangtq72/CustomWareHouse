using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using ZetaCompressionLibrary;
using CW_Info;

namespace CW_Business
{
    public class UserInfo_BL
    {

        public User_Info UserInfo_GetById(decimal p_id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.UserInfo_GetById(p_id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                List<User_Info> _lst = NaviCommon.CBO<User_Info>.FillCollectionFromDataSet(_ds);
                if (_lst.Count > 0)
                {
                    return _lst[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        public List<User_Info> UserInfo_GetByType(decimal p_type)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.UserInfo_GetByType(p_type);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<User_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<User_Info>();
            }
        }

        public User_Info UserInfo_GetByName(string p_name)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.UserInfo_GetByName(p_name);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                List<User_Info> _lst = NaviCommon.CBO<User_Info>.FillCollectionFromDataSet(_ds);
                if (_lst.Count > 0)
                {
                    return _lst[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        public List<User_Info> UserInfo_Search(string name, string user_type, string status, string Custom_Id, string column,
            string type_sort, string start, string end, ref decimal P_Total)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.UserInfo_Search(name, user_type, status, Custom_Id, column, type_sort, start, end, ref P_Total);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<User_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<User_Info>();
            }
        }

        public decimal UserInfo_Insert(string user_name, string password, string fullname, decimal Custom_Id, decimal user_type, decimal status, string Phone, string Email)
        {
            try
            {
                return CommonData.c_serviceWCF.UserInfo_Insert(user_name, password, fullname, Custom_Id, user_type, status, Phone, Email);
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
                return CommonData.c_serviceWCF.UserInfo_SetPassword(p_user_id, p_status, p_password);
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
                return CommonData.c_serviceWCF.UserInfo_SetStatus(p_user_id, p_status);
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
                return CommonData.c_serviceWCF.UserInfo_Delete(p_user_id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public User_Info UserInfo_CheckLogin(string p_username, string p_password)
        {
            try
            {
                string pass_used_check = NaviCommon.CommonFuc.Encrypt(p_username.ToUpper() + p_password);
                User_Info objUser = UserInfo_GetByName(p_username);
                if (objUser == null || objUser.User_Id == 0)
                {
                    return null;
                }
                User_Info ResultUser = UserInfo_GetById(objUser.User_Id);
                if (ResultUser != null)
                {
                    if (pass_used_check == ResultUser.Password)
                    {
                        return ResultUser;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// NSD tự thay đổi thông tin
        /// </summary>
        /// <param name="p_user_id"></param>
        /// <param name="p_pass"></param>
        /// <param name="p_last_pass"></param>
        /// <returns></returns>
        public decimal UserInfo_Update_Pass(decimal p_user_id, string p_pass, DateTime p_last_pass)
        {
            try
            {
                return CommonData.c_serviceWCF.UserInfo_Update_Pass((int)p_user_id, p_pass, p_last_pass);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
