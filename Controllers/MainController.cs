using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ExpansesControlSystem.Models;
using MailEngine;

namespace ExpansesControlSystem.Controllers
{
    public class MainController : Controller
    {
        private ExpensesControlDBEntities6 db = new ExpensesControlDBEntities6();
        //
        // GET: /Main/

        public ActionResult Index(string accName)
        {
            var childList = new List<SelectListItem>();
            childList.Add(new SelectListItem { Text = "My self", Selected = true  });
            
            //who is
             var le= db.TestEmpDatas.Single(p => p.SAmAccountName == accName);
            //load all child
            switch (le.UserType)
            {
                case (0):
                    //usual
                    break;
                case (1):
                    //manager
                   var childs = db.TestEmpDatas.Where(p => p.ManadgerSAmAccountName == accName);
                    foreach (var testEmpData in childs)
                    {
                       childList.Add(new SelectListItem{Text =testEmpData.SAmAccountName}); 
                    }
                    break;
                case (2):
                    //financer
                    var childsAll = db.TestEmpDatas;
                    foreach (var testEmpData in childsAll)
                    {
                       childList.Add(new SelectListItem{Text =testEmpData.SAmAccountName}); 
                    }
                    break;
            }
            ViewBag.Childs = childList;
            ViewBag.AccName = accName;
            return View();
        }
        //
        // GET: /Main/1
        public ActionResult AddNew(string accName, string parAcc)
        {
            return RedirectToAction("Index", "Expenses", new RouteValueDictionary { { "accName", accName }, { "parAcc", parAcc } });
        }

        public ActionResult SaveGroup(string accName, string groupId)
        {

            // отправить мыло
            ViewBag.AccName = accName;
            MailSender mailSender = new MailSender();
            var empt = db.TestEmpDatas.Single(p => p.SAmAccountName == accName);
           
            // поменять статус на локед!!!
            int grId = Convert.ToInt32(groupId);
            db.ExpensesGroups.Single(p => p.ID == grId).StatusID = 1;
            db.ExpensesGroups.Single(p => p.ID == grId).GroupDate = DateTime.Now;
            //TODO: проверить изменяет ли
            db.SaveChanges();
            new Task(() => mailSender.SendAnEmail(empt.Mail, "TEST EXPENSE FOR " + accName, "test!!! - staus - LOCKED")).Start();
          return  RedirectToAction("Index", new RouteValueDictionary { { "accName", accName } });
        }
    }
}
