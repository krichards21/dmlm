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
    public class AlertsAdminController : Controller
    {
        private dmlmEntities db = new dmlmEntities();

        // GET: AlertsAdmin
        public ActionResult Index()
        {
            return View(db.Alerts.ToList());
        }

        // GET: AlertsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // GET: AlertsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlertsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,alertType,description,serviceProviderID,link")] Alert alert)
        {
            if (ModelState.IsValid)
            {
                db.Alerts.Add(alert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alert);
        }

        // GET: AlertsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // POST: AlertsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,alertType,description,serviceProviderID,link")] Alert alert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alert);
        }

        // GET: AlertsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // POST: AlertsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alert alert = db.Alerts.Find(id);
            db.Alerts.Remove(alert);
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
