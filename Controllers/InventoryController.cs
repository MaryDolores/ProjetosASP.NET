using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web;
using Projeto_Teste;
using System.Data.Linq;

namespace Projeto_Teste.Controllers
{
    public class InventoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult AddProducts()
        {
            DataClasses1DataContext dbo = new DataClasses1DataContext();
            string productName = Request["name"];
            decimal amount = Convert.ToDecimal(Request["amount"]);
            dbo.Products.InsertOnSubmit(new Products
            {
                
                ProductName = productName,
                ProductAmount = amount,
            }) ;

            dbo.SubmitChanges();
            return RedirectToAction("Display");
        }

        public ActionResult Display()
        {
            DataClasses1DataContext dbo = new DataClasses1DataContext();
            var products = (from data in dbo.Products select data).ToList();
            ViewBag.productsList = products;
            return View();
        }

        public ActionResult UpdateProducts()
        {
            DataClasses1DataContext dbo = new DataClasses1DataContext();
            string name = Request["name"];
            decimal amount = Convert.ToDecimal(Request["amount"]);
            int pId = Convert.ToInt32(Request["pId"]);

            var product = (from data in dbo.Products where data.ProductId == pId select data).FirstOrDefault();
            product.ProductName = name;
            product.ProductAmount = amount;
            dbo.SubmitChanges();
            return RedirectToAction("Display");
        }

        public ActionResult DeleteProducts()
        {
            DataClasses1DataContext dbo = new DataClasses1DataContext();
            object pId = Url.RequestContext.RouteData.Values["id"];

            var product = (from data in dbo.Products where data.ProductId == Convert.ToInt32(pId) select data);
            dbo.Products.DeleteAllOnSubmit(product);
            dbo.SubmitChanges();


            return RedirectToAction("Display");
        }
    }
}