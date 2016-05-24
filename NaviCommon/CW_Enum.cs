using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviCommon
{

    public enum Enum_Contract_Status
    {
        ChoDuyet = 0,
        Da_Duyet = 1,
        Huy_Duyet = 2,
        Thanh_Khoan = 3
    }

    public enum Enum_Declaration_Type
    {
        ToKhai_Nhap = 1,
        ToKhai_Xuat = 2
    }

    public enum Enum_User_Type
    {
        HaiQuan = 1,
        Kho = 2,
        DoanhNghiep = 3
    }

    public enum Enum_User_Status
    {
        News = 0,
        Confrim = 1,
        Reset_PassWord = 2
    }

    public enum ProductDeralationType
    {
        Import = 1,
        Export = 2
    }

    public enum Grant_Auz_Result
    {
        Accept = 1,
        Decline = 2,
        Not_Config = 3,
    }

}
