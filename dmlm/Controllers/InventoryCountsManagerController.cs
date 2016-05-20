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
    public class InventoryCountsManagerController : Controller
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

        // GET: InventoryCountsManager
        public ActionResult Index()
        {
            var inventoryCounts = db.InventoryCounts.Include(i => i.Product).Include(i => i.InventoryLocation).Include(i => i.UOM).Include(i => i.User);
            return View(inventoryCounts.ToList());
        }

        // GET: InventoryCountsManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryCount inventoryCount = db.InventoryCounts.Find(id);
            if (inventoryCount == null)
            {
                return HttpNotFound();
            }
            return View(inventoryCount);
        }

        // GET: InventoryCountsManager/Create
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.Products, "Id", "name");
            ViewBag.inventoryLocationId = new SelectList(db.InventoryLocations, "Id", "name");
            ViewBag.uomId = new SelectList(db.UOMs, "Id", "name");
            ViewBag.userId = new SelectList(db.Users, "Id", "firstName");
            return View();
        }

        // POST: InventoryCountsManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,count,productId,inventoryLocationId,updateDate,userId,uomId")] InventoryCount inventoryCount)
        {
            if (ModelState.IsValid)
            {
                db.InventoryCounts.Add(inventoryCount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Products, "Id", "name", inventoryCount.productId);
            ViewBag.inventoryLocationId = new SelectList(db.InventoryLocations, "Id", "name", inventoryCount.inventoryLocationId);
            ViewBag.uomId = new SelectList(db.UOMs, "Id", "name", inventoryCount.uomId);
            ViewBag.userId = new SelectList(db.Users, "Id", "firstName", inventoryCount.userId);
            return View(inventoryCount);
        }

        // GET: InventoryCountsManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryCount inventoryCount = db.InventoryCounts.Find(id);
            if (inventoryCount == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "Id", "name", inventoryCount.productId);
            ViewBag.inventoryLocationId = new SelectList(db.InventoryLocations, "Id", "name", inventoryCount.inventoryLocationId);
            ViewBag.uomId = new SelectList(db.UOMs, "Id", "name", inventoryCount.uomId);
            ViewBag.userId = new SelectList(db.Users, "Id", "firstName", inventoryCount.userId);
            return View(inventoryCount);
        }

        // POST: InventoryCountsManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,count,productId,inventoryLocationId,updateDate,userId,uomId")] InventoryCount inventoryCount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryCount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Products, "Id", "name", inventoryCount.productId);
            ViewBag.inventoryLocationId = new SelectList(db.InventoryLocations, "Id", "name", inventoryCount.inventoryLocationId);
            ViewBag.uomId = new SelectList(db.UOMs, "Id", "name", inventoryCount.uomId);
            ViewBag.userId = new SelectList(db.Users, "Id", "firstName", inventoryCount.userId);
            return View(inventoryCount);
        }

        // GET: InventoryCountsManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryCount inventoryCount = db.InventoryCounts.Find(id);
            if (inventoryCount == null)
            {
                return HttpNotFound();
            }
            return View(inventoryCount);
        }

        // POST: InventoryCountsManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventoryCount inventoryCount = db.InventoryCounts.Find(id);
            db.InventoryCounts.Remove(inventoryCount);
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
