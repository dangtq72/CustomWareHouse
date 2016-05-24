using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace CW_Service
{
    public class AllCodeDA
    {
        public DataSet Allcode_GetAll()
        {
            try
            {
                //return OracleHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "pkg_allcode.proc_allcode_getall",
                // new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output));
                //return new DataSet();

                SqlParameter[] spParameter = new SqlParameter[0];
                return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "proc_AllCode_GetBy_All", spParameter);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }
    }
}
