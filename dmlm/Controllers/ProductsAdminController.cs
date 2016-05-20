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
    public class ProductsAdminController : Controller
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

        // GET: ProductsAdmin
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductCategory).Include(p => p.UOM).Include(p => p.Manufacturer);
            return View(products.ToList());
        }

        // GET: ProductsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ProductsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.productCategoryId = new SelectList(db.ProductCategories, "Id", "name");
            ViewBag.uomId = new SelectList(db.UOMs, "Id", "name");
            ViewBag.manufacturerId = new SelectList(db.Manufacturers, "Id", "name");
            return View();
        }

        // POST: ProductsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,createDate,description,urlPrimaryImage,urlProduct,upc,partNumber,stockingCode,manufacturerId,uomId,serviceProviderId,updateDate,endDate,isActive,productCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productCategoryId = new SelectList(db.ProductCategories, "Id", "name", product.productCategoryId);
            ViewBag.uomId = new SelectList(db.UOMs, "Id", "name", product.uomId);
            ViewBag.manufacturerId = new SelectList(db.Manufacturers, "Id", "name", product.manufacturerId);
            return View(product);
        }

        // GET: ProductsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.productCategoryId = new SelectList(db.ProductCategories, "Id", "name", product.productCategoryId);
            ViewBag.uomId = new SelectList(db.UOMs, "Id", "name", product.uomId);
            ViewBag.manufacturerId = new SelectList(db.Manufacturers, "Id", "name", product.manufacturerId);
            return View(product);
        }

        // POST: ProductsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,createDate,description,urlPrimaryImage,urlProduct,upc,partNumber,stockingCode,manufacturerId,uomId,serviceProviderId,updateDate,endDate,isActive,productCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productCategoryId = new SelectList(db.ProductCategories, "Id", "name", product.productCategoryId);
            ViewBag.uomId = new SelectList(db.UOMs, "Id", "name", product.uomId);
            ViewBag.manufacturerId = new SelectList(db.Manufacturers, "Id", "name", product.manufacturerId);
            return View(product);
        }

        // GET: ProductsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
