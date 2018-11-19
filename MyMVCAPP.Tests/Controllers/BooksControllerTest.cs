using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyMVCAPP.Controllers;
using MyMVCAPP.DataModels;
using MyMVCAPP.Repository;

namespace MyMVCAPP.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTest
    {
        [TestMethod]
        public void TestIndexMethod()
        {
            IEnumerable<Books> books = new List<Books>()
            {
                new Books { BookId = 1, BookTitle = "Harry Porter", Price = 10 },
                new Books { BookId = 2, BookTitle = "Harry Porter", Price = 20 },
                new Books { BookId = 3, BookTitle = "Harry Porter", Price = 30 },
                new Books { BookId = 4, BookTitle = "Harry Porter", Price = 40 }
            };

            //Mock IRepository of books
            Mock<IRepository<Books>> mockBooksController = new Mock<IRepository<Books>>();
            mockBooksController.Setup(b => b.GetAll()).Returns(books);

            //Pass in the IRepository books
            BooksController booksController = new BooksController(mockBooksController.Object);
            ViewResult result = booksController.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetailsMethod()
        {
            Books book = new Books { BookId = 1, BookTitle = "Harry Porter", Price = 10 };

            //Mock IRepository of books
            Mock<IRepository<Books>> mockBooksController = new Mock<IRepository<Books>>();
            mockBooksController.Setup(b => b.GetByID(It.IsAny<int>())).Returns(book);

            //Pass in the IRepository books
            BooksController booksController = new BooksController(mockBooksController.Object);
            ViewResult result = booksController.Details(1) as ViewResult;
            Assert.AreEqual(book,result.Model);
        }

        [TestMethod]
        public void TestCreateMethod()
        {
            Books books = new Books { BookId = 1, BookTitle = "Harry Porter", Price = 10 };

            //Mock IRepository of books
            Mock<IRepository<Books>> mockBooksController = new Mock<IRepository<Books>>();
            mockBooksController.Setup(b => b.Insert(It.IsAny<Books>())).Callback<Books>(s=>books=s);

            //Pass in the IRepository books
            BooksController booksController = new BooksController(mockBooksController.Object);
            var result = booksController.Create(books) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void TestEditMethod()
        {
            Books books = new Books { BookId = 1, BookTitle = "Harry Porter", Price = 10 };

            //Mock IRepository of books
            Mock<IRepository<Books>> mockBooksController = new Mock<IRepository<Books>>();
            mockBooksController.Setup(b => b.Update(It.IsAny<Books>())).Callback<Books>(s => books = s);

            //Pass in the IRepository books
            BooksController booksController = new BooksController(mockBooksController.Object);
            var result = booksController.Edit(books) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void TestDeleteMethod()
        {
            Books book = new Books { BookId = 1, BookTitle = "Harry Porter", Price = 10 };

            //Mock IRepository of books
            Mock<IRepository<Books>> mockBooksController = new Mock<IRepository<Books>>();
            mockBooksController.Setup(b => b.GetByID(It.IsAny<int>())).Returns(book);

            //Pass in the IRepository books
            BooksController booksController = new BooksController(mockBooksController.Object);
            ViewResult result = booksController.Edit(1) as ViewResult;
            Assert.AreEqual(book, result.Model);
        }

        [TestMethod]
        public void TestDeleteConfirmedMethod()
        {
            Books book = new Books { BookId = 1, BookTitle = "Harry Porter", Price = 10 };

            //Mock IRepository of books
            Mock<IRepository<Books>> mockBooksController = new Mock<IRepository<Books>>();
            mockBooksController.Setup(b => b.Delete(It.IsAny<int>()));

            //Pass in the IRepository books
            BooksController booksController = new BooksController(mockBooksController.Object);
            var result = booksController.DeleteConfirmed(1) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }
    }
}
