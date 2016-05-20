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
    public class ProductInventoryLocationsController : Controller
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

        // GET: ProductInventoryLocations
        public ActionResult Index()
        {
            var productInventoryLocations = db.ProductInventoryLocations.Include(p => p.InventoryLocation).Include(p => p.Product);
            return View(productInventoryLocations.ToList());
        }

        // GET: ProductInventoryLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInventoryLocation productInventoryLocation = db.ProductInventoryLocations.Find(id);
            if (productInventoryLocation == null)
            {
                return HttpNotFound();
            }
            return View(productInventoryLocation);
        }

        // GET: ProductInventoryLocations/Create
        public ActionResult Create()
        {
            ViewBag.inventoryLocationId = new SelectList(db.InventoryLocations, "Id", "name");
            ViewBag.productId = new SelectList(db.Products, "Id", "name");
            return View();
        }

        // POST: ProductInventoryLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,inventoryLocationId,productId,createDate,isActive")] ProductInventoryLocation productInventoryLocation)
        {
            if (ModelState.IsValid)
            {
                db.ProductInventoryLocations.Add(productInventoryLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.inventoryLocationId = new SelectList(db.InventoryLocations, "Id", "name", productInventoryLocation.inventoryLocationId);
            ViewBag.productId = new SelectList(db.Products, "Id", "name", productInventoryLocation.productId);
            return View(productInventoryLocation);
        }

        // GET: ProductInventoryLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInventoryLocation productInventoryLocation = db.ProductInventoryLocations.Find(id);
            if (productInventoryLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.inventoryLocationId = new SelectList(db.InventoryLocations, "Id", "name", productInventoryLocation.inventoryLocationId);
            ViewBag.productId = new SelectList(db.Products, "Id", "name", productInventoryLocation.productId);
            return View(productInventoryLocation);
        }

        // POST: ProductInventoryLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,inventoryLocationId,productId,createDate,isActive")] ProductInventoryLocation productInventoryLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productInventoryLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.inventoryLocationId = new SelectList(db.InventoryLocations, "Id", "name", productInventoryLocation.inventoryLocationId);
            ViewBag.productId = new SelectList(db.Products, "Id", "name", productInventoryLocation.productId);
            return View(productInventoryLocation);
        }

        // GET: ProductInventoryLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInventoryLocation productInventoryLocation = db.ProductInventoryLocations.Find(id);
            if (productInventoryLocation == null)
            {
                return HttpNotFound();
            }
            return View(productInventoryLocation);
        }

        // POST: ProductInventoryLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductInventoryLocation productInventoryLocation = db.ProductInventoryLocations.Find(id);
            db.ProductInventoryLocations.Remove(productInventoryLocation);
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
