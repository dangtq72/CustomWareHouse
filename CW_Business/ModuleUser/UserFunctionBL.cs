using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_Info;
using ZetaCompressionLibrary;

namespace CW_Business
{
    public class UserFunctionBL
    {
        public decimal UserFunctionInsert(UserFunction_Info pInfo)
        {
            try
            {
                return CommonData.c_serviceWCF.UserFunctionInsert(pInfo.User_Id, pInfo.Func_Id, pInfo.lstFunction, pInfo.Group_Id, pInfo.Created_By);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -3;
            }
        }

        public Hashtable UserFunctionGetAll(decimal pIdUser, decimal pIdGroup)
        {
            try
            {
                Hashtable chashtble = new Hashtable();
                byte[] byteRecive = CommonData.c_serviceWCF.UserFunctionGetAll(pIdUser, pIdGroup);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);
                List<Functions_Info> arrlst = NaviCommon.CBO<Functions_Info>.FillCollectionFromDataSet(_ds);
                foreach (Functions_Info item in arrlst)
                {
                }
                return chashtble;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new Hashtable();
            }
        }

        public bool Delete_UserFunction_ByUser(decimal p_User_Id)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
