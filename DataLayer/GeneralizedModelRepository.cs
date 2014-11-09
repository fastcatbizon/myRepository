using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ExpansesControlSystem.Models;

namespace ExpansesControlSystem.DataLayer
{
    public class GeneralizedModelRepository
    {
        public GenerelizeModel Get(string sAmAccountName)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AD"].ConnectionString);
            using (connection)
            {
                var command = new SqlCommand(String.Format(
                    @"SELECT 
     [DATABASE]
      ,[EMPLOYED_ID]
      ,[fullname]
      ,[mail]
      ,[sAMAccountName]
      ,[ACCNAME]
      ,[CLOSED]
      ,[ACCDES]
      ,[CODE]
      ,[RST_BUDGETPREFIX]
      ,[USER]
      ,[PNL]
      ,[PNLGROUPNAME] 
FROM [Expense_Employee]
WHERE [sAMAccountName] = '{0}'", sAmAccountName),
                    connection);
                connection.Open();

                SqlDataReader r = command.ExecuteReader();

                if (r.HasRows)
                {
                    while (r.Read())
                    {

                        var gm = new GenerelizeModel();
                        gm.Database = SafeGetString(r,0);
                        gm.EmployedId =  SafeGetInt32(r,0);
                        gm.Fullname =  SafeGetString(r,1);
                        gm.Mail =  SafeGetString(r,2);
                        gm.SAmAccountName =  SafeGetString(r,3);
                        gm.Accname =  SafeGetInt32(r,4);
                        gm.Closed =  SafeGetString(r,5);
                        gm.Accdes =  SafeGetString(r,6);
                        gm.Code =  SafeGetString(r,7);
                        gm.RstBudgetprefix =  SafeGetString(r,8);
                        gm.User =  SafeGetString(r,9);
                        gm.Pnl =  SafeGetString(r,10);
                        gm.Pnlgroupname =  SafeGetString(r,11);
                    }
                }
                else
                {
                    return null;
                }
                r.Close();
                return null;
            }
        }
        public  string SafeGetString( SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }
        public Int32 SafeGetInt32(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return 0;
        }
    }
}
