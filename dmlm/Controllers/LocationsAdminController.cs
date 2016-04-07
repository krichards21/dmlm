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
    public class LocationsAdminController : Controller
    {
        private dmlmEntities db = new dmlmEntities();

        // GET: LocationsAdmin
        public ActionResult Index()
        {
            var locations = db.Locations.Include(l => l.LocationCategory).Include(l => l.ServiceProvider);
            return View(locations.ToList());
        }

        // GET: LocationsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: LocationsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.locationCategoryId = new SelectList(db.LocationCategories, "Id", "name");
            ViewBag.serviceProviderId = new SelectList(db.ServiceProviders, "Id", "name");
            return View();
        }

        // POST: LocationsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,description,address1,address2,address3,city,state,postalCode,county,latitude,long,radius,zoneDescription,googleMapSmall,googleMapMedium,url,imageUrl,operatingHoursStart,operatingHoursEnd,operatingDays,serviceProviderId,locationCategoryId,phone")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.locationCategoryId = new SelectList(db.LocationCategories, "Id", "name", location.locationCategoryId);
            ViewBag.serviceProviderId = new SelectList(db.ServiceProviders, "Id", "name", location.serviceProviderId);
            return View(location);
        }

        // GET: LocationsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.locationCategoryId = new SelectList(db.LocationCategories, "Id", "name", location.locationCategoryId);
            ViewBag.serviceProviderId = new SelectList(db.ServiceProviders, "Id", "name", location.serviceProviderId);
            return View(location);
        }

        // POST: LocationsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,description,address1,address2,address3,city,state,postalCode,county,latitude,long,radius,zoneDescription,googleMapSmall,googleMapMedium,url,imageUrl,operatingHoursStart,operatingHoursEnd,operatingDays,serviceProviderId,locationCategoryId,phone")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.locationCategoryId = new SelectList(db.LocationCategories, "Id", "name", location.locationCategoryId);
            ViewBag.serviceProviderId = new SelectList(db.ServiceProviders, "Id", "name", location.serviceProviderId);
            return View(location);
        }

        // GET: LocationsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: LocationsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
