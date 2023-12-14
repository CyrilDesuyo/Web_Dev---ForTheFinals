using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForTheFinal.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Activity()
        {
            return View();
        }

        public ActionResult AddActivity(FormCollection fc)
        {
            try
            {
                string activityName = fc["activityName"];
                DateTime date = DateTime.Parse(fc["date"]);
                TimeSpan time = TimeSpan.Parse(fc["time"]);
                string location = fc["location"];
                string ootd = fc["ootd"];

                Activity act = new Activity
                {
                    activityName = activityName,
                    date = date,
                    time = time,
                    location = location,
                    ootd = ootd,
                    //createdAt = DateTime.Now
                };

                using (RegToDBEntities rte = new RegToDBEntities())
                {
                    rte.Activities.Add(act);
                    rte.SaveChanges();
                }

                return RedirectToAction("Activity", "User");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error"); // You might want to create an error view
            }
        }

    }
}