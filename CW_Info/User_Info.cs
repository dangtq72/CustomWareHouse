using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
    [Serializable]
    public class User_Info
    {
        public decimal User_Id { get; set; }

        public string User_Name { get; set; }
        public string Password { get; set; }

        public decimal User_Type { get; set; }
        public string Type_Name { get; set; }
        public decimal Custom_Id { get; set; }
        public string Custom_Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Full_Name { get; set; }
        public decimal Status { get; set; }
        public string Status_Name { get; set; }

        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }

        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }

        public Dictionary<string, Functions_Info> gHshFunctionOfUser = new Dictionary<string, Functions_Info>();
        public DateTime Last_Update_Pass { get; set; }
    }
}
