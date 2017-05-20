using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System.Data.Entity.Infrastructure;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
       //private FabricsEntities db = new FabricsEntities();
        ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        [OutputCache(Duration = 5, Location = System.Web.UI.OutputCacheLocation.ServerAndClient)]
        public ActionResult Index(bool Active=true)
        {
            var data = repo.GetProduct列表頁所有資料(Active, showAll: false);
            //return View(data);
            ViewData.Model = data;
            ViewData["ppp"] = data;
            ViewBag.qqq = data;
            return View();
            
            //var data = repo.All()
            //    .Where(p => p.Active.HasValue && p.Active.Value == Active)
            //    .OrderByDescending(p => p.ProductId).Take(10);
            //return View(data);

            //var dt = db.Product.
            //    Where(p => p.Active.HasValue && p.Active.Value == Active)
            //    .OrderByDescending(p => p.ProductId).Take(10);
            //return View(db.Product.Take(10));
            //return View(dt);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Product product = db.Product.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            //if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        //public ActionResult ListProducts(FormCollection fc)
        //public ActionResult ListProducts(string q,int s1=0, int s2=9999) 
        public ActionResult ListProducts(ProductListSearchVM searchCondition)
        {
            var data = repo.GetProduct列表頁所有資料(true);
            //if (!String.IsNullOrEmpty(q))
            //if (!String.IsNullOrEmpty(fc["q"]))
            //{
            //    data = data.Where(p => p.ProductName.Contains(q) & p.Stock>=s1 & p.Stock<=s2);
            //    //var keyword = fc["q"];
            //    //data = data.Where(p => p.ProductName.Contains(keyword));
            //}
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(searchCondition.q))
                {
                    data = data.Where(p => p.ProductName.Contains(searchCondition.q));
                }
                data = data.Where(p => p.Stock > searchCondition.s1 && p.Stock < searchCondition.s2);
            }

            ViewData.Model = data
            .Select(p => new ProductLiteVM()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                Stock = p.Stock
            });
            return View(); ;
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct([Bind(Include = "ProductName,Price,Stock")] ProductLiteVM pVM)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                TempData["MyMsg"] = "TempData Test";
                return RedirectToAction("ListProducts");
            }
            return View(pVM);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Product product = db.Product.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(product);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        public ActionResult Edit(int id,FormCollection fc)
        {
            var product= repo.Get單筆資料ByProductId(id);
            if (TryUpdateModel(product,
                //要驗證的屬性
                new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" }))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            //if (ModelState.IsValid)
            //{
            //    //db.Entry(product).State = EntityState.Modified;
            //    //db.SaveChanges();
            //    repo.Update(product);
            //    repo.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            Product product = repo.Get單筆資料ByProductId(id);
            repo.Delete(product);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
