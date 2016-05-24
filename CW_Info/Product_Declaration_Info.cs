using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
    public class Product_Declaration_Info
    {
        public decimal Product_Declaration_Id { get; set; }
        public decimal Product_Id { get; set; }

        public string Product_Code { get; set; }

        public decimal Declaration_Id { get; set; }

        // 1: nhập, 2: xuất                      
        public decimal Type { get; set; }                                // Loại

        public decimal Wrong_Number { get; set; }                       // Sai số

        public string Unit { get; set; }
        public string Made_In { get; set; }
        public decimal Package_Quantity { get; set; }                   // Số kiện
        
        public decimal Quantity { get; set; }                           // Số lượng nhập
        public decimal Value { get; set; }                              // Giá trị

        // cập nhật (vào thằng nhập) khi xuất kho
        public decimal Package_Quantity_Delivery { get; set; }          // Số kiện xuất
        public decimal Quantity_Delivery { get; set; }                  // Số lượng xuất (Từ 2 cái này sẽ tính ra đc thằng tồn)

       public decimal Declaration_Reference_Id { get; set; }      
    }
}
