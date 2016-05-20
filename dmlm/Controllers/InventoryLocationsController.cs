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
    public class InventoryLocationsController : Controller
    {
        private dmlmEntities db = new dmlmEntities();
        private Models.PageModel.Page UserPage = new Models.PageModel.Page();
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            var pageModel = HttpContext.Cache.Get("pageModel");
            if (pageModel != null)
                UserPage = (Models.PageModel.Page)pageModel;
            ViewBag.Layout = UserPage.Layout;
        }

        // GET: InventoryLocations
        public ActionResult Index()
        {
            var inventoryLocations = db.InventoryLocations.Include(i => i.Location);
            return View(inventoryLocations.ToList());
        }

        // GET: InventoryLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryLocation inventoryLocation = db.InventoryLocations.Find(id);
            if (inventoryLocation == null)
            {
                return HttpNotFound();
            }
            return View(inventoryLocation);
        }

        // GET: InventoryLocations/Create
        public ActionResult Create()
        {
            ViewBag.locationId = new SelectList(db.Locations, "Id", "name");
            return View();
        }

        // POST: InventoryLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,description,isActive,locationId,createDate,updateDate,endDate")] InventoryLocation inventoryLocation)
        {
            if (ModelState.IsValid)
            {
                db.InventoryLocations.Add(inventoryLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.locationId = new SelectList(db.Locations, "Id", "name", inventoryLocation.locationId);
            return View(inventoryLocation);
        }

        // GET: InventoryLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryLocation inventoryLocation = db.InventoryLocations.Find(id);
            if (inventoryLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.locationId = new SelectList(db.Locations, "Id", "name", inventoryLocation.locationId);
            return View(inventoryLocation);
        }

        // POST: InventoryLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,description,isActive,locationId,createDate,updateDate,endDate")] InventoryLocation inventoryLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.locationId = new SelectList(db.Locations, "Id", "name", inventoryLocation.locationId);
            return View(inventoryLocation);
        }

        // GET: InventoryLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryLocation inventoryLocation = db.InventoryLocations.Find(id);
            if (inventoryLocation == null)
            {
                return HttpNotFound();
            }
            return View(inventoryLocation);
        }

        // POST: InventoryLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventoryLocation inventoryLocation = db.InventoryLocations.Find(id);
            db.InventoryLocations.Remove(inventoryLocation);
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
