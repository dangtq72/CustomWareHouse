using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
    [Serializable]
    public class Product_Info
    {
        public decimal Product_Id { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public decimal Category_Id { get; set; }
        public string Category_Name { get; set; }

        public string Unit { get; set; }
    }
}
