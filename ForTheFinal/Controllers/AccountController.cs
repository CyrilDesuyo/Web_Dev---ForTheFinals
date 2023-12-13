using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace ForTheFinal.Controllers
{
    public class AccountController : Controller
    {
        private RegToDBEntities db = new RegToDBEntities();

        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            User user = db.Users.FirstOrDefault(u => (u.username == username || u.email == username) && u.password == password);

            if (user != null)
            {

                if (user.roleID == 1)
                {

                    return RedirectToAction("AdminPage", "Account");
                }
                else if (user.roleID == 2)
                {

                    return RedirectToAction("UserPage", "Account");
                }
            }


            return RedirectToAction("Login", "Home");
        }

        public ActionResult Logout()
        {

            Session.Abandon();


            FormsAuthentication.SignOut();


            return RedirectToAction("Login", "Home");
        }

        public ActionResult AdminPage()
        {
            return View();
        }

        public ActionResult UserPage()
        {
            return View();
        }


        public ActionResult ViewUsers()
        {
            RegToDBEntities db = new RegToDBEntities();
            var users = db.Users.Where(u => u.roleID == 2).ToList();

            return View(users);
        }

        public ActionResult ViewAdmin()
        {
            RegToDBEntities db = new RegToDBEntities();
            var users = db.Users.Where(u => u.roleID == 1).ToList();

            return View(users);
        }

        //new update
        public ActionResult EditUser(int id)
        {
            RegToDBEntities db = new RegToDBEntities();
            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.Find(updatedUser.userID);

                if (existingUser == null)
                {
                    return HttpNotFound();
                }

                existingUser.username = updatedUser.username;
                existingUser.email = updatedUser.email;
                existingUser.password = updatedUser.password;

                existingUser.roleID = updatedUser.roleID;

                db.Entry(existingUser).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ViewUsers");
            }

            return View(updatedUser);
        }


        //delete
        public ActionResult ConfirmDelete(int id)
        {
            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // Handle the actual deletion of the user
        [HttpPost, ActionName("ConfirmDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            // Redirect to the user list or another appropriate page after deletion
            return RedirectToAction("ViewUsers");
        }
    }
}