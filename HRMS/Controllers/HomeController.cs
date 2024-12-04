using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class HomeController : Controller
    {
        BAL ObjBAL = new BAL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult UserLogin(FormCollection form)
        {
            string UserId = form["Userid"];
            string Password = form["Password"];
            Session["UserId"] = form["Userid"];
            
            if (ObjBAL.UserLogin(UserId,Password) == true)
            {        
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                TempData["IsLogin"] = "Faild";
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}