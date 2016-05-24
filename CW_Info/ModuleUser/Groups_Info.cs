using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
   public  class Groups_Info
    {
        public int Group_Id { get; set; }
        public string Group_Name { get; set; }
        public string Note { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }

        public List<User_Info> lstUserOfGroupInfo = new List<User_Info>();
    }
}
