using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExpansesControlSystem.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Index(string accName)
        {
            ViewBag.AccName = accName;
            return View();
        }
        //
        // GET: /Main/1
        public ActionResult AddNew(string accName)
        {
            return RedirectToAction("Index", "Expenses", new RouteValueDictionary { { "accName", accName } });
        }
    }
}
