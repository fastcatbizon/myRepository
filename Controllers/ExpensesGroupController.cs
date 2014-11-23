using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ExpansesControlSystem.Models;

namespace ExpansesControlSystem.Controllers
{
    public class ExpensesGroupController : Controller
    {
        private ExpensesControlDBEntities6 db = new ExpensesControlDBEntities6();

        //
        // GET: /ExpensesGroup/

        public ActionResult Index()
        {
            var expensesgroups = db.ExpensesGroups.Include(e => e.Employee);
            return View(expensesgroups.ToList());
        }

        //
        // GET: /ExpensesGroup/Details/5

        public ActionResult Details(int id = 0)
        {
            ExpensesGroup expensesgroup = db.ExpensesGroups.Find(id);
            if (expensesgroup == null)
            {
                return HttpNotFound();
            }
            return View(expensesgroup);
        }

        //
        // GET: /ExpensesGroup/Create

        public ActionResult Create(string parAcc)
        {
         //ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "Name");
            //if (ModelState.IsValid)
            {
                string name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                //write to table
                Employee emp = new Employee();
                //get data from shahar database for user id
                string email = "";
                emp.ID = 34;
                emp.ManagerID = 23453245; ;//!!!!!!!!!!!!
                emp.ManagerName = "Petr Ivanov";
                emp.Name = "werfwre";
                emp.BugetPrefix = "wefwe";
                emp.Number = 324234;
                emp.DateTime = DateTime.Now;
                //emp.Expanses = new Collection<Expans>();

                bool isExistEmp = db.Employees.Count(p => p.ID == emp.ID) > 0;
                if (!isExistEmp)
                {
                    db.Employees.Add(emp);
                }
                ExpensesGroup expensesGroup = new ExpensesGroup();
                expensesGroup.EmployeeID = emp.ID;
                expensesGroup.StatusID = 0;
                //generate uniq id
                int newId = new Random().Next( 0, int.MaxValue );
                while (db.ExpensesGroups.Count(p => p.ID == newId)!=0)
                {
                    newId = new Random().Next(0, int.MaxValue);
                }
                
                expensesGroup.ID = newId;
                db.ExpensesGroups.Add(expensesGroup);
                db.SaveChanges();
                return RedirectToAction("Index", "Expenses", new RouteValueDictionary { { "groupId", newId } });
            }
            //return ;
        }

        //
        // POST: /ExpensesGroup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpensesGroup expensesgroup)
        {
            if (ModelState.IsValid)
            {
                db.ExpensesGroups.Add(expensesgroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "Name", expensesgroup.EmployeeID);
            return View(expensesgroup);
        }

        //
        // GET: /ExpensesGroup/Edit/5

        public ActionResult Edit(int id )
        {
            var expensesgroup =db.ExpensesGroups.Single(p => p.ID == id);
            db.Entry(expensesgroup).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Expenses", new RouteValueDictionary { { "groupId", expensesgroup.ID } });
        }

        //
        // POST: /ExpensesGroup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExpensesGroup expensesgroup)
        {
           
                db.Entry(expensesgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Expenses", new RouteValueDictionary { { "groupId", expensesgroup.ID } });
        }

        //
        // GET: /ExpensesGroup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ExpensesGroup expensesgroup = db.ExpensesGroups.Find(id);
            if (expensesgroup == null)
            {
                return HttpNotFound();
            }
            return View(expensesgroup);
        }

        //
        // POST: /ExpensesGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpensesGroup expensesgroup = db.ExpensesGroups.Find(id);
            db.ExpensesGroups.Remove(expensesgroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}