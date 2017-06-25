using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventManager;
using EventManager.Models;
using EventManager.QueriesFolder;

namespace EventManager.Controllers
{
    public class UsersController : BaseController
    {
        private EventRegEntities db = new EventRegEntities();
        private UserQueries userQueries = new UserQueries();

        // GET: Users
        public ActionResult Index()
        {
            return View(userQueries.GetAllUsersWithLocationInfo());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.location_ID = new SelectList(db.Locations, "location_ID", "city");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username, password, lastName," + 
            "firstName, phoneHome, phoneCell, phoneOffice, foodPref, companyName, branch," +
            "additionalInfo, address, city, state,zip")] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    username = user.username,
                    password = user.password,
                    lastName = user.lastName,
                    firstName = user.firstName,
                    phoneHome = user.phoneHome,
                    phoneCell = user.phoneCell,
                    phoneOffice = user.phoneOffice,
                    foodPref = user.foodPref,
                    companyName = user.companyName,
                    branch = user.branch,
                    additionalInfo = user.additionalInfo
                };

                var location = new Location
                {
                    address = user.address,
                    city = user.city,
                    state = user.state,
                    zip = user.zip,
                    addressType = "User"
                };

                (new UserQueries()).CreateUser(newUser, location);
                return RedirectToAction("Index");
            }

            //ViewBag.location_ID = new SelectList(db.Locations, "location_ID", "city", user.location_ID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.location_ID = new SelectList(db.Locations, "location_ID", "city", user.location_ID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_ID,username,password,lastName,firstName,phoneHome,phoneCell,phoneOffice,foodPref,companyName,branch,additionalInfo,location_ID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.location_ID = new SelectList(db.Locations, "location_ID", "city", user.location_ID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Users.Where(a => a.username.Equals(objUser.username) && a.password.Equals(objUser.password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.user_ID.ToString();
                    Session["UserName"] = obj.username.ToString();
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            return View(objUser);
        }
    }
}
