using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
    [Serializable]
    public class WareHouse_Info
    {
        public decimal WareHouse_Id { get; set; }
        public string WareHouse_Code { get; set; }
        public string WareHouse_Name { get; set; }
        public string Location { get; set; }
    }
}
