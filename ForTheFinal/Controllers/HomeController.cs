using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForTheFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult addUserToDB(FormCollection fc)
        {
            String Username = fc["username"];
            String Email = fc["email"];
            String Password = fc["password"];
            int UserType = Convert.ToInt16(fc["select"]);

            User use = new User();
            use.username = Username;
            use.email = Email;
            use.password = Password;
            use.roleID = UserType;

            RegToDBEntities rte = new RegToDBEntities();
            rte.Users.Add(use);
            rte.SaveChanges();

            if (UserType == 1)
            {
                return RedirectToAction("AdminPage", "Account");
            }
            else if (UserType == 2)
            {

                return RedirectToAction("UserPage", "Account");
            }


            return RedirectToAction("Login", "Home");

        }

    }
}