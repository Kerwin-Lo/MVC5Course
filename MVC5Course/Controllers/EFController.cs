using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using MVC5Course.Models;
namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {
        
        // GET: EF
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();
            var result = all.Where(p=>p.Active==true && p.Is刪除==false).OrderByDescending(p => p.ProductId).Take(10);
            return View(result);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var result = db.Product.Find(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.ProductName = product.ProductName;
                item.Price = product.Price;
                item.Stock= product.Stock;
                item.Active = product.Active;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Details(int id)
        {
            var result = db.Database.SqlQuery<Product>("SELECT * FROM dbo.Product WHERE ProductId=@p0", id).FirstOrDefault();
            return View(result);
        }

        public ActionResult Delete(int id)
        {

            var product = db.Product.Find(id);
            //foreach (var p in product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(p);
            //}
            //db.OrderLine.RemoveRange(product.OrderLine);

            //db.Product.Remove(product);
            product.Is刪除 = true;
            try
            {

                db.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

    }
}