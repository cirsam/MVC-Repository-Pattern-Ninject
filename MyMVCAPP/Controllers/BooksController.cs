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
    public class BooksController : Controller
    {
        private readonly IRepository<Books> _repository;

        public BooksController(IRepository<Books> repository)
        {
            _repository = repository;
        }


        // GET: Books
        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Books booksViewModel = _repository.GetByID(id);
            if (booksViewModel == null)
            {
                return HttpNotFound();
            }
            return View(booksViewModel);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,BookTitle,Price")] Books booksViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(booksViewModel);
                return RedirectToAction("Index");
            }

            return View(booksViewModel);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books booksViewModel = _repository.GetByID(id);
            if (booksViewModel == null)
            {
                return HttpNotFound();
            }
            return View(booksViewModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,BookTitle,Price")] Books booksViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(booksViewModel);
                return RedirectToAction("Index");
            }
            return View(booksViewModel);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books booksViewModel = _repository.GetByID(id);
            if (booksViewModel == null)
            {
                return HttpNotFound();
            }
            return View(booksViewModel);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             _repository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
