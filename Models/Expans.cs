//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExpansesControlSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expans
    {
        public System.Guid ID { get; set; }
        public string TypeCode { get; set; }
        public System.DateTime Date { get; set; }
        public string Currency { get; set; }
        public decimal Ammount { get; set; }
        public string Note { get; set; }
        public string FileDescription { get; set; }
        public string FilePath { get; set; }
        public int ExpenseGroupID { get; set; }
    
        public virtual ExpensesGroup ExpensesGroup { get; set; }
    }
}