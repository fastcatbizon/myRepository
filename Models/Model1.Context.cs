﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ExpensesControlDBEntities6 : DbContext
    {
        public ExpensesControlDBEntities6()
            : base("name=ExpensesControlDBEntities6")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expans> Expanses { get; set; }
        public DbSet<ExpensesGroup> ExpensesGroups { get; set; }
        public DbSet<TestEmpData> TestEmpDatas { get; set; }
    }
}
