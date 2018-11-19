using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyMVCAPP.DataModels;
using MyMVCAPP.Models;
using MyMVCAPP.Repository;

namespace MyMVCAPP.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Products> _repository;

        public ProductsController(IRepository<Products> repository)
        {
            _repository = repository;
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products productsViewModol = _repository.GetByID(id);
            if (productsViewModol == null)
            {
                return HttpNotFound();
            }
            return View(productsViewModol);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price")] Products productsViewModol)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(productsViewModol);
                return RedirectToAction("Index");
            }

            return View(productsViewModol);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products productsViewModol = _repository.GetByID(id);
            if (productsViewModol == null)
            {
                return HttpNotFound();
            }
            return View(productsViewModol);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price")] Products productsViewModol)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(productsViewModol);
                return RedirectToAction("Index");
            }
            return View(productsViewModol);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products productsViewModol = _repository.GetByID(id);
            if (productsViewModol == null)
            {
                return HttpNotFound();
            }
            return View(productsViewModol);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
