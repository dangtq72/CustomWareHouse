using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
    public class TonKho_Info
    {
        public string Product_Name { get; set; }
        public decimal Quantity_Input { get; set; }
        public decimal Period { get; set; }

        
        public string Register_Input { get; set; }
        public string Custom { get; set; }
        public DateTime Exp_Date { get; set; }
        public DateTime Register_Date { get; set; }
        public DateTime Due_Date { get; set; }

        public string Gate_Exp { get; set; }
        public string PhuongTien { get; set; }
        public decimal SL_Ton { get; set; }
        public decimal SoNgayTon { get; set; }

    }
}
