using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using ExpansesControlSystem.DataLayer;
using ExpansesControlSystem.Models;
using ExpansesControlSystem.Tools.FileManagment;

namespace ExpansesControlSystem.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpensesControlDBEntities6 db = new ExpensesControlDBEntities6();
        
        //
        // GET: /Expenses/

        public ActionResult Index(string accName, string parAcc)
        {
                
                //write to table
            Employee emp = GetEmployee(accName);
                
            
           
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
                int newId = new Random().Next(0, int.MaxValue);
                while (db.ExpensesGroups.Count(p => p.ID == newId) != 0)
                {
                    newId = new Random().Next(0, int.MaxValue);
                }

                expensesGroup.ID = newId;
                db.ExpensesGroups.Add(expensesGroup);
                db.SaveChanges();
                var expanses = db.Expanses.Include(e => e.ExpensesGroup.Employee)
                    .Where(p => p.ExpenseGroupID == newId);
                ViewBag.GroupId = newId;
            //Write employee prop to bag
            ViewBag.Emp = emp;
            ViewBag.ParAcc = parAcc;
            ViewBag.Status = 0;
                return View(expanses.ToList());
        }

        public ActionResult IndexApprove(string empName, string parAcc, string groupId)
        {
            var grId = Int32.Parse(groupId);
            var expanses = db.Expanses.Include(e => e.ExpensesGroup.Employee).Where(p => p.ExpenseGroupID == grId);
            ViewBag.Emp = GetEmployee(empName);
            ViewBag.Status = db.ExpensesGroups.Single(p => p.ID == grId).StatusID;
            ViewBag.GroupId = groupId;
            ViewBag.ParAcc = parAcc;
            ViewBag.IsApp = true;
            return View("Index", expanses.ToList());
        }

        private Employee GetEmployee(String accName)
        {
           var le= db.TestEmpDatas.Single(p => p.SAmAccountName == accName);
            TestEmpData lm = db.TestEmpDatas.Single(p => p.SAmAccountName == le.ManadgerSAmAccountName);
           
          
            const bool isTest = true;
            var emp = new Employee();
            if (isTest)
            {
                emp.ID = le.UserId;
                emp.ManagerID = lm.UserId;
                ; //!!!!!!!!!!!!
                emp.ManagerName = le.ManadgerSAmAccountName;

                emp.Name = le.SAmAccountName;
                emp.BugetPrefix = le.RstBudgetprefix;
                if (le.UserType != null) emp.Type = (int) le.UserType;
                emp.DateTime = DateTime.Now;
            }
            else
            {
                //get data from shahar database for user accName
                var gmRepository = new GeneralizedModelRepository();
                GenerelizeModel gm = gmRepository.Get(accName);
                 //get from AD
                var ad = new ActiveDirectoryControl(accName);

                emp.ID = gm.EmployedId;
                emp.ManagerID = 911;//from AD????
                emp.ManagerName = ad.GetManager();//from AD????
                emp.Name = gm.SAmAccountName;
                emp.BugetPrefix = gm.RstBudgetprefix;
                //emp.Number =;
                emp.DateTime = DateTime.Now;


            }
            return emp;
        }

        public ActionResult IndexView(int groupId, string empName, string parAcc)
        {
            var expanses = db.Expanses.Include(e => e.ExpensesGroup.Employee)
                .Where(p => p.ExpenseGroupID == groupId);
            ViewBag.Emp = GetEmployee(empName);
            ViewBag.Status = db.ExpensesGroups.Single(p => p.ID == groupId).StatusID;
            ViewBag.GroupId = groupId;
            ViewBag.ParAcc = parAcc;
            return View("Index",expanses.ToList());
        }

        //
        // GET: /Expenses/Details/5

        public ActionResult Details(Guid id )
        {
            Expans expans = db.Expanses.Find(id);
            if (expans == null)
            {
                return HttpNotFound();
            }
            return View(expans);
        }

        //
        // GET: /Expenses/Create

        public ActionResult Create(int groupId, string empName, string parAcc)
        {
            //ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "Name");
            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(new SelectListItem { Text = "Car" });
            types.Add(new SelectListItem { Text = "Cafe" });
            types.Add(new SelectListItem { Text = "Bar", Selected = true });
            //types.Add(new SelectListItem { Text = "Shluhi" });
            ViewBag.TypeCode = types;

            List<SelectListItem> currancyList = new List<SelectListItem>();
            currancyList.Add(new SelectListItem { Text = "USD" });
            currancyList.Add(new SelectListItem { Text = "EUR" });
            ViewBag.Currency = currancyList;
            Expans exp = new Expans();
            exp.ExpenseGroupID = groupId;
            ViewBag.GroupId = groupId;
            ViewBag.EmpName = empName;
            ViewBag.ParAcc = parAcc;
            return View(exp);
        }

        //
        // POST: /Expenses/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expans exp, HttpPostedFileBase file, string empName, string parAcc)
        {
            if (ModelState.IsValid)
            {
                
                //EXPENSES 
               // exp.EmployeeID = emp.ID;
                exp.ID = Guid.NewGuid();
                try
                {
exp.FilePath = new KeepingFiles().SaveUploadFile(file);
                }
                catch
                {
                    
                }
                
                db.Expanses.Add(exp);
                db.SaveChanges();
                ViewBag.GroupId = exp.ExpenseGroupID;
                return RedirectToAction("IndexView", new RouteValueDictionary { { "groupId", exp.ExpenseGroupID }, { "empName", empName }, {"parAcc",parAcc} });
            }

            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(new SelectListItem { Text = "Car" });
            types.Add(new SelectListItem { Text = "Cafe" });
            types.Add(new SelectListItem { Text = "Bar", Selected = true });
            //types.Add(new SelectListItem { Text = "Shluhi" });
            ViewBag.TypeCode = types;

            List<SelectListItem> currancyList = new List<SelectListItem>();
            currancyList.Add(new SelectListItem { Text = "USD" });
            currancyList.Add(new SelectListItem { Text = "EUR" });
            ViewBag.Currency = currancyList;
 
           
            ViewBag.GroupId = exp.ExpenseGroupID;
            ViewBag.EmpName = empName;
            ViewBag.ParAcc = parAcc;
            return View(exp);
        }

        //
        // GET: /Expenses/Edit/5

        public ActionResult Edit(Guid id, int groupId, string empName, string parAcc)
        {
            Expans expans = db.Expanses.Find(id);
            if (expans == null)
            {
                return HttpNotFound();
            }
            var types = new List<SelectListItem>();
            types.Add(new SelectListItem { Text = "Car" });
            types.Add(new SelectListItem { Text = "Cafe" });
            types.Add(new SelectListItem { Text = "Bar", Selected = true });
           // types.Add(new SelectListItem { Text = "Shluhi" });
            ViewBag.TypeCode = types;

            var currancyList = new List<SelectListItem>();
            currancyList.Add(new SelectListItem { Text = "USD" });
            currancyList.Add(new SelectListItem { Text = "EUR" });
            ViewBag.Currency = currancyList;
            ViewBag.GroupId = groupId;
            ViewBag.EmpName = empName;
            ViewBag.ParAcc = parAcc;
           // ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "Name", expans.EmployeeID);
            return View(expans);
        }

        //
        // POST: /Expenses/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Expans expans, HttpPostedFileBase file, string empName, string parAcc)
        {
            if (ModelState.IsValid)
            {
                Expans expOld = db.Expanses.Find(expans.ID);
                expans.ExpenseGroupID = expOld.ExpenseGroupID;
                expans.ID = expOld.ID;
                try
                {
                    if (file != null)
                    {
                        expans.FilePath = new KeepingFiles().SaveUploadFile(file);
                    }
                    else
                    {
                        expans.FilePath = expOld.FilePath;
                    }

                }
                catch
                {

                }
                ViewBag.ParAcc = parAcc;
                db.Expanses.Remove(expOld);
                db.Expanses.Add(expans);
              //  db.Entry(expans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexView", new RouteValueDictionary { { "groupId", expans.ExpenseGroupID }, { "empName", empName }, { "parAcc", parAcc } });
            }
            var types = new List<SelectListItem>();
            types.Add(new SelectListItem { Text = "Car" });
            types.Add(new SelectListItem { Text = "Cafe" });
            types.Add(new SelectListItem { Text = "Bar", Selected = true });
            // types.Add(new SelectListItem { Text = "Shluhi" });
            ViewBag.TypeCode = types;

            var currancyList = new List<SelectListItem>();
            currancyList.Add(new SelectListItem { Text = "USD" });
            currancyList.Add(new SelectListItem { Text = "EUR" });
            ViewBag.Currency = currancyList;
            ViewBag.GroupId = expans.ExpenseGroupID;
            ViewBag.EmpName = empName;
            ViewBag.ParAcc = parAcc;
            //ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "Name", expans.EmployeeID);
            return View(expans);
        }

        //
        // GET: /Expenses/Delete/5

        public ActionResult Delete(Guid id, int groupId, string empName,string parAcc)
        {
            Expans expans = db.Expanses.Find(id);
            if (expans == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = expans.ExpenseGroupID;
            ViewBag.EmpName = empName;
            ViewBag.ParAcc = parAcc;
            return View(expans);
        }

        //
        // POST: /Expenses/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id, string empName, string parAcc)
        {
         
            Expans expans = db.Expanses.Find(id);
            int grId = expans.ExpenseGroupID;
            db.Expanses.Remove(expans);
            db.SaveChanges();
            ViewBag.ParAcc = parAcc;
            return RedirectToAction("IndexView", new RouteValueDictionary { { "groupId", grId }, { "empName", empName }, { "parAcc", parAcc } });
        }

        public ActionResult Approve(int groupId, string empName, string parAcc)
        {

            return RedirectToAction("ApproveG", "ExpensesGroup", new RouteValueDictionary { { "groupId", groupId }, { "empName", empName }, { "parAcc", parAcc } });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}