using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace CW_Service
{
    public partial interface CWContractService
    {
        [OperationContract()]
        byte[] DBMem_Get_HashInfo(string strhashName);

    }

    public partial class CWService : CWContractService
    {
        public byte[] DBMem_Get_HashInfo(string strhashName)
        {
            try
            {
                byte[] byteReturn = null;
                switch (strhashName)
                {
                    case "c_lst_UserDoanhNghiep":
                        lock (DBMemory.c_lst_UserDoanhNghiep)
                            byteReturn = Util.ObjectToByteArray(DBMemory.c_lst_UserDoanhNghiep);
                        break;
                    case "c_lst_WareHouse":
                        lock (DBMemory.c_lst_UserDoanhNghiep)
                            byteReturn = Util.ObjectToByteArray(DBMemory.c_lst_WareHouse);
                        break;
                    case "c_lst_Product":
                        lock (DBMemory.c_lst_UserDoanhNghiep)
                            byteReturn = Util.ObjectToByteArray(DBMemory.c_lst_Product);
                        break;
                }
                return byteReturn;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new byte[0];
            }
        }

    }
}
