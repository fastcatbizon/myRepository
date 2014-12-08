using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ExpansesControlSystem.Models;
using Microsoft.Ajax.Utilities;

namespace ExpansesControlSystem.Controllers
{
    public class ExpensesGroupController : Controller
    {
        private ExpensesControlDBEntities6 db = new ExpensesControlDBEntities6();

        //
        // GET: /ExpensesGroup/

        public ActionResult Index(string accName, string parAcc)
        {
            var emp = db.Employees.Single(p => p.Name == accName);
            var expensesgroups = db.ExpensesGroups.Where(e => e.EmployeeID==emp.ID).Include(p=>p.Employee).Include(p=>p.Expanses);
            var expenseGroupList = expensesgroups.ToList();
            ViewBag.ParAcc = parAcc;
            return View(expenseGroupList);
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
        [ActionName("ApproveG")]
        public ActionResult ApproveG(int groupId, string empName, string parAcc)
        {
            var emp = db.Employees.Single(p => p.Name == empName);
            
            switch (emp.Type)
            {
                case (1):
                    //manager
                    db.ExpensesGroups.Single(p => p.ID == groupId).StatusID = 2;
                    break;
                case (2):
                    //finance
                    db.ExpensesGroups.Single(p => p.ID == groupId).StatusID = 3;
                    break;

            }
            var expensesgroups = db.ExpensesGroups.Where(e => e.EmployeeID == emp.ID).Include(p => p.Employee).Include(p => p.Expanses);
            var expenseGroupList = expensesgroups.ToList();
            ViewBag.ParAcc = parAcc;
            return View(expenseGroupList);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}