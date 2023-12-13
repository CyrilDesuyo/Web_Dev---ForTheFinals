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
                // Retrieve the existing user from the database
                var existingUser = db.Users.Find(updatedUser.userID);

                if (existingUser == null)
                {
                    // Handle the case where the user with the specified ID is not found
                    return HttpNotFound();
                }

                // Update the properties of the existing user with the values from the edited user
                existingUser.username = updatedUser.username;
                existingUser.email = updatedUser.email;
                existingUser.password = updatedUser.password;

                // Make sure to set the roleID to its existing value
                existingUser.roleID = updatedUser.roleID;

                // Mark the entity as modified and save changes
                db.Entry(existingUser).State = EntityState.Modified;
                db.SaveChanges();

                // Redirect to the user list or another appropriate page
                return RedirectToAction("ViewUsers");
            }

            // If the model state is not valid, return to the edit form with validation errors
            return View(updatedUser);
        }

    }
}