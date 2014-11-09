using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpansesControlSystem.Models
{
    public class GenerelizeModel
    {
        public GenerelizeModel()
        {
        }

        public GenerelizeModel(string database, int employedId, string fullname, string mail, string sAmAccountName, int accname, string closed, string accdes, string code, string rstBudgetprefix, string user, string pnl, string pnlgroupname)
        {
            Pnlgroupname = pnlgroupname;
            Pnl = pnl;
            User = user;
            RstBudgetprefix = rstBudgetprefix;
            Code = code;
            Accdes = accdes;
            Closed = closed;
            Accname = accname;
            SAmAccountName = sAmAccountName;
            Mail = mail;
            Fullname = fullname;
            EmployedId = employedId;
            Database = database;
        }

        public string Database { get;  set; }
      public int EmployedId { get;  set; }
      public string Fullname { get;  set; }
      public string Mail { get;  set; }
      public string SAmAccountName { get;  set; }
      public int Accname { get;  set; }
      public string Closed { get;  set; }
      public string Accdes { get;  set; }
      public string Code { get;  set; }
      public string RstBudgetprefix { get;  set; }
      public string User { get;  set; }
      public string Pnl { get;  set; }
      public string Pnlgroupname { get;  set; }
    }
}