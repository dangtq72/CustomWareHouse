using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_Service
{
   public class Business_DA
    {
       public DataSet BUSINESS_GET_ALL()
       {
           try
           {
               return SqlHelper.ExecuteDataset(Config_DB_Info.GConnectionString, CommandType.StoredProcedure, "PROC_BUSINESS_GET_ALL");
           }
           catch (Exception ex)
           {
               Common.log.Error(ex.ToString());
               return new DataSet();
           }
       }
    }
}
