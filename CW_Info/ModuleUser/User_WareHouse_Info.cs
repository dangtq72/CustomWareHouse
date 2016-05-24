using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
    public class User_WareHose_Info
    {
        public decimal User_Id { get; set; }
        public decimal WareHouse_Id { get; set; }
        public string WareHouse_Name { get; set; }

        public string Created_By { get; set; }

        public DateTime Created_Date { get; set; }
    }
}
