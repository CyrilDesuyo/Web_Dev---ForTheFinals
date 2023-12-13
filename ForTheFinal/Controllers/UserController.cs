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

        public ActionResult addActivityToDB(FormCollection fc)
        {   
            string timeString;

            String ActivityName = fc["activityName"];
            DateTime Date = DateTime.ParseExact(fc["date"], "yy-MM-dd", null);
            String TimeString = fc["time"];
            String Location = fc["location"];
            String OOTD = fc["ootd"];

            Activity act = new Activity();
            act.activityName = ActivityName;
            act.date = Date;
            act.time = Time;
            act.location = Location;
            act.ootd = OOTD;

            if (!string.IsNullOrEmpty(timeString) && TimeSpan.TryParse(timeString, out TimeSpan parsedTime))
            {
                // Successfully parsed the time string to TimeSpan
                act.time = parsedTime;
            }
            else
            {
                // Handle invalid time format or null/empty time string
                Console.WriteLine("Invalid time format or null/empty time string");
            }

            RegToDBEntities rte = new RegToDBEntities();
            rte.Activities.Add(act);
            rte.SaveChanges();
        }
    }
}