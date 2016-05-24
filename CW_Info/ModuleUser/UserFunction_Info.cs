using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
   public  class UserFunction_Info
    {
        public int User_Id { get; set; }
        public int Func_Id { get; set; }
        public string lstFunction { get; set; }
        public int Group_Id { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
    }
}
