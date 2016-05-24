using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
    public class Contracts_Info
    {
        public decimal STT { get; set; }

        public decimal Contract_Id { get; set; }

        public string Contract_Code { get; set; }

        public DateTime Register_Date { get; set; }
        public DateTime Due_Date { get; set; }

        public decimal Period { get; set; }

        public decimal Receive_Number { get; set; }

        public decimal Receive_Year { get; set; }

        public decimal WareHouse_Id { get; set; }
        public string WareHouse_Name { get; set; }

        public decimal Business_Id { get; set; }
        public string Business_Name { get; set; }

        public decimal Money_Type { get; set; }

        public string Money_Type_Name { get; set; }

        public string Custom_Service { get; set; }

        public decimal Status { get; set; }

        public string Status_Name { get; set; }  

        public string Reason { get; set; }

        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }

        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }

        public string Approve_By { get; set; }

    }
}
