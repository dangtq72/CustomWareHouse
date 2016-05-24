using CW_Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZetaCompressionLibrary;

namespace CW_Business
{
    public class User_WareHose_BL
    {
        public List<User_WareHose_Info> User_WareHouse_GetByUser_Auz(decimal p_user_id)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.User_WareHouse_GetByUser(p_user_id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<User_WareHose_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<User_WareHose_Info>();
            }
        }

        public List<WareHouse_Info> WareHouse_GetByUser(decimal User_Id, decimal User_Type)
        {
            try
            {
                byte[] byteRecive = CommonData.c_serviceWCF.WareHouse_GetByUser(User_Id, User_Type);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                return NaviCommon.CBO<WareHouse_Info>.FillCollectionFromDataSet(_ds);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<WareHouse_Info>();
            }
        }

        public List<WareHouse_Info> WareHouse_GetByUser_Auz(decimal p_user_id)
        {
            try
            {
                List<WareHouse_Info> _listWareHouse = new List<WareHouse_Info>();

                byte[] byteRecive = CommonData.c_serviceWCF.User_WareHouse_GetByUser(p_user_id);
                DataSet _ds = CompressionHelper.DecompressDataSet(byteRecive);

                List<User_WareHose_Info> _listUserSymbol = NaviCommon.CBO<User_WareHose_Info>.FillCollectionFromDataSet(_ds);
                foreach (User_WareHose_Info item in _listUserSymbol)
                {
                    WareHouse_Info _tempInfo = new WareHouse_Info();
                    _tempInfo.WareHouse_Name = item.WareHouse_Name;
                    _tempInfo.WareHouse_Id = item.WareHouse_Id;
                    _listWareHouse.Add(_tempInfo);
                }
                return _listWareHouse;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<WareHouse_Info>();
            }
        }

        public bool User_WareHouse_Insert_Batch(List<User_WareHose_Info> p_lst)
        {
            try
            {
                int _count = p_lst.Count;

                for (int i = 0; i < _count; i++)
                {
                    User_WareHose_Info item = p_lst[i];
                    bool _ck = CommonData.c_serviceWCF.User_WareHouse_Insert(item.User_Id, item.WareHouse_Id, item.Created_By, item.Created_Date);
                    if (_ck == false)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public decimal WareHouse_DeleteByUser(decimal p_user_id)
        {
            try
            {
                return CommonData.c_serviceWCF.WareHouse_DeleteByUser(p_user_id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
