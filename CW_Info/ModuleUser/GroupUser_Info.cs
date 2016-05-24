using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Info
{
   public  class GroupUser_Info
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Group_Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

        //Tên nhóm 
        public string Group_Name { get; set; }
        public string UserName { get; set; }
    }
}
