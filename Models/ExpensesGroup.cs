//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Linq;

namespace ExpansesControlSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExpensesGroup
    {
        public ExpensesGroup()
        {
            this.Expanses = new HashSet<Expans>();
        }
    
        public int ID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public int EmployeeID { get; set; }
        public Nullable<System.DateTime> GroupDate { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Expans> Expanses { get; set; }

   /*     public List<Expans> GetUniqueExpenses(List<Expans> myList)
        {

            var listOfTypes = new List<string>();
            foreach (var exp in myList)
            {
                listOfTypes.Add(exp.TypeCode);
            }

            listOfTypes =  listOfTypes.Distinct().ToList();

            var uniqueExpenses = new List<Expans>();

            foreach (var type in listOfTypes)
            {
                var findAll = myList.FindAll(exp=>exp.TypeCode==type);
                //try to make it via LINQ
                decimal sumAmmount = 0.0m;

                foreach (var element in findAll)
                {
                    sumAmmount = sumAmmount + element.Ammount;
                }

                var myExpense = new Expans()
                {
                    Ammount = sumAmmount,
                    Currency = null,
                    TypeCode = type,
                    Date = DateTime.Now, //LOL
                    ExpenseGroupID = 0,
                    ExpensesGroup = null,
                    FileDescription = null,
                    FilePath = null,
                    ID = new Guid(),
                    Note = null,
                };

                uniqueExpenses.Add(myExpense);

            }

            return uniqueExpenses;

        } */
    }
}
