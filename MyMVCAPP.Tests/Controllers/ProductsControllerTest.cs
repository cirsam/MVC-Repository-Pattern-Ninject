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
    public class ProductsControllerTest
    {
        [TestMethod]
        public void TestIndexMethod()
        {
            IEnumerable<Products> products = new List<Products>()
            {
                new Products { ProductId = 1, ProductName = "Harry Porter", Price = 10 },
                new Products { ProductId = 2, ProductName = "Harry Porter", Price = 20 },
                new Products { ProductId = 3, ProductName = "Harry Porter", Price = 30 },
                new Products { ProductId = 4, ProductName = "Harry Porter", Price = 40 }
            };

            //Mock IRepository of Products
            Mock<IRepository<Products>> mockProductsController = new Mock<IRepository<Products>>();
            mockProductsController.Setup(b => b.GetAll()).Returns(products);

            //Pass in the IRepository Products
            ProductsController productsController = new ProductsController(mockProductsController.Object);
            ViewResult result = productsController.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetailsMethod()
        {
            Products product = new Products { ProductId = 1, ProductName = "Harry Porter", Price = 10 };

            //Mock IRepository of products
            Mock<IRepository<Products>> mockProductsController = new Mock<IRepository<Products>>();
            mockProductsController.Setup(b => b.GetByID(It.IsAny<int>())).Returns(product);

            //Pass in the IRepository products
            ProductsController productsController = new ProductsController(mockProductsController.Object);
            ViewResult result = productsController.Details(1) as ViewResult;
            Assert.AreEqual(product, result.Model);
        }

        [TestMethod]
        public void TestCreateMethod()
        {
            Products products = new Products { ProductId = 1, ProductName = "Harry Porter", Price = 10 };

            //Mock IRepository of products
            Mock<IRepository<Products>> mockProductsController = new Mock<IRepository<Products>>();
            mockProductsController.Setup(b => b.Insert(It.IsAny<Products>())).Callback<Products>(s => products = s);

            //Pass in the IRepository products
            ProductsController productsController = new ProductsController(mockProductsController.Object);
            var result = productsController.Create(products) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void TestEditMethod()
        {
            Products products = new Products { ProductId = 1, ProductName = "Harry Porter", Price = 10 };

            //Mock IRepository of products
            Mock<IRepository<Products>> mockProductsController = new Mock<IRepository<Products>>();
            mockProductsController.Setup(b => b.Update(It.IsAny<Products>())).Callback<Products>(s => products = s);

            //Pass in the IRepository products
            ProductsController productsController = new ProductsController(mockProductsController.Object);
            var result = productsController.Edit(products) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void TestDeleteMethod()
        {
            Products products = new Products { ProductId = 1, ProductName = "Harry Porter", Price = 10 };

            //Mock IRepository of products
            Mock<IRepository<Products>> mockProductsController = new Mock<IRepository<Products>>();
            mockProductsController.Setup(b => b.GetByID(It.IsAny<int>())).Returns(products);

            //Pass in the IRepository products
            ProductsController productsController = new ProductsController(mockProductsController.Object);
            ViewResult result = productsController.Edit(1) as ViewResult;
            Assert.AreEqual(products, result.Model);
        }

        [TestMethod]
        public void TestDeleteConfirmedMethod()
        {
            Products products = new Products { ProductId = 1, ProductName = "Harry Porter", Price = 10 };

            //Mock IRepository of products
            Mock<IRepository<Products>> mockProductsController = new Mock<IRepository<Products>>();
            mockProductsController.Setup(b => b.Delete(It.IsAny<int>()));

            //Pass in the IRepository products
            ProductsController productsController = new ProductsController(mockProductsController.Object);
            var result = productsController.DeleteConfirmed(1) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }
    }
}
