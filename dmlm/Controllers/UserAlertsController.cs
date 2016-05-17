using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dmlm;

namespace dmlm.Controllers
{
    public class UserAlertsController : Controller
    {
        private dmlmEntities db = new dmlmEntities();

        // GET: UserAlerts
        public ActionResult Index()
        {
            var userAlerts = db.UserAlerts.Include(u => u.User).Include(u => u.Alert);
            return View(userAlerts.ToList());
        }

        // GET: UserAlerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAlert userAlert = db.UserAlerts.Find(id);
            if (userAlert == null)
            {
                return HttpNotFound();
            }
            return View(userAlert);
        }

        // GET: UserAlerts/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.Users, "Id", "firstName");
            ViewBag.alertID = new SelectList(db.Alerts, "Id", "description");
            return View();
        }

        // POST: UserAlerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,userID,alertID")] UserAlert userAlert)
        {
            if (ModelState.IsValid)
            {
                db.UserAlerts.Add(userAlert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.Users, "Id", "firstName", userAlert.userID);
            ViewBag.alertID = new SelectList(db.Alerts, "Id", "description", userAlert.alertID);
            return View(userAlert);
        }

        // GET: UserAlerts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAlert userAlert = db.UserAlerts.Find(id);
            if (userAlert == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.Users, "Id", "firstName", userAlert.userID);
            ViewBag.alertID = new SelectList(db.Alerts, "Id", "description", userAlert.alertID);
            return View(userAlert);
        }

        // POST: UserAlerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,userID,alertID")] UserAlert userAlert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAlert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.Users, "Id", "firstName", userAlert.userID);
            ViewBag.alertID = new SelectList(db.Alerts, "Id", "description", userAlert.alertID);
            return View(userAlert);
        }

        // GET: UserAlerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAlert userAlert = db.UserAlerts.Find(id);
            if (userAlert == null)
            {
                return HttpNotFound();
            }
            return View(userAlert);
        }

        // POST: UserAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAlert userAlert = db.UserAlerts.Find(id);
            db.UserAlerts.Remove(userAlert);
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
    }
}
