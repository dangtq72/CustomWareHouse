using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Service
{
   public class ClSQLTool
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="op_obj_cmd"> đối tượng lệnh cần thêm tham số</param>
        /// <param name="ip_obj_para_name">tên tham số dưới CSDL</param>
        /// <param name="ip_value">giá trị của tham số truyền xuống</param>
        /// <param name="ip_type">kiểu tham số</param>
        public static void addParamater(
            SqlCommand op_obj_cmd
            , string ip_obj_para_name
            , object ip_value
            , SqlDbType ip_type,
            int is_output = 0)
        {
            SqlParameter v_obj_par = new SqlParameter();
            v_obj_par.ParameterName = ip_obj_para_name;
            v_obj_par.SqlDbType = ip_type;
            v_obj_par.Value = ip_value;
            //  v_obj_par.Direction = ParameterDirection.InputOutput;
            if (is_output == 1)
            {
                v_obj_par.Direction = ParameterDirection.Output;
            }
            op_obj_cmd.Parameters.Add(v_obj_par);

        }
    }
}
