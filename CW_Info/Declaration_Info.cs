using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
    public class Declaration_Info
    {
        public decimal STT { get; set; }
        public decimal Declaration_Id { get; set; }
        public string Declaration_Code { get; set; }
        public decimal Contract_Id { get; set; }
        public string Contract_Code { get; set; }

        // ngày đăng ký
        public DateTime Register_Date { get; set; }
        public decimal Money_Type { get; set; }
        public string Money_Type_Name { get; set; }

        // 1 tờ khai nhập, 2 xuất kho
        public decimal Type { get; set; }

        // loại hình: xuất đầu tư, nhập đầu tư ?? chưa biết có những loại nào
        public decimal Declaration_Type { get; set; }

        public string Declaration_Type_Name { get; set; }

        public decimal Total_Value { get; set; }
        public decimal WareHouse_Id { get; set; }
        public string WareHouse_Name { get; set; }
        public string WareHouse_Location { get; set; }

        // cửa khẩu xuất/ cửa khẩu nhập tùy theo type
        public string Gate { get; set; }

        // Hải quan đăng ký
        public string Custom_Register { get; set; }

        public decimal Receive_Number { get; set; }
        public decimal Receive_Year { get; set; }
        public decimal VANDON_NUMBER { get; set; }
        public DateTime  VANDON_DATE { get; set; }
        public decimal Status { get; set; }
        public string Status_Name { get; set; }  
        public string Reason { get; set; }
        public string Source { get; set; }

        public string Source_Name { get; set; } 

        public decimal Declaration_Refence_Id { get; set; }
        public string Declaration_Refence_Code { get; set; }

        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }

        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public string Approve_By { get; set; }

        public string Phone { get; set; }


        List<Product_Declaration_Info> _listProduct = new List<Product_Declaration_Info>();

        public List<Product_Declaration_Info> ListProduct 
        {
            set { _listProduct = value; } 
           get { return  _listProduct; } 
        }
    }
}
