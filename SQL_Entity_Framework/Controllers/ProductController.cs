using SQL_Entity_Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQL_Entity_Framework.Controllers
{
    public class ProductController : Controller
    {
        //
        //Object of DataContext
        DataContext db = new DataContext();

        // GET: /Product/
        public ActionResult Index()
        {
            var data = db.Products.SqlQuery("select * from products.").ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(string productid)
        {
            var productlist = db.Products.SqlQuery("select *from products where ProductId=@p0", productid).ToList();

            return View(productlist);
        }
        public ActionResult AddProduct()
        {
            //Open View with textboxes to insert product data
            //Add View with Create templete(select Product as Model class and DataContext as Context Class)
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Products obj)
        {
            //Create Parameters which are used for product table fields. 
            List<object> parameters = new List<object>();
            parameters.Add(obj.ProductName);
            parameters.Add(obj.SerialNumber);
            parameters.Add(obj.Company);
            object[] objectarray = parameters.ToArray();
            //SQL Query function is used only for retrieve sql tables. 
            //To Perform Insert, Update , Delete we use Database.ExecuteSqlCommand which will return affected rows in database table
            int output = db.Database.ExecuteSqlCommand("insert into Products(ProductName,SerialNumber,Company) values(@p0,@p1,@p2)", objectarray);
            if (output > 0)
            {
                ViewBag.Itemmsg = "Your Product " + obj.ProductName + "  is added Successfully";
            }
            return View();
        }

        public ActionResult UpdateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProduct(Products obj)
        {
            List<object> parameters = new List<object>();
            parameters.Add(obj.ProductName);
            parameters.Add(obj.SerialNumber);
            parameters.Add(obj.Company);
            parameters.Add(obj.ProductId);
            object[] objectarray = parameters.ToArray();

            int output = db.Database.ExecuteSqlCommand("update Products set ProductName=@p0,SerialNumber=@p1,Company=@p2 where ProductId=@p3", objectarray);
            if (output > 0)
            {
                ViewBag.Itemmsg = "Your Product id " + obj.ProductId + "  is updated seccussfully";
            }
            return View();
        }

        public ActionResult ProductDelete()
        {

            //First We write table to show all data including product id textbox. where user will see all data with Delete button 
            //with each row.
            var productlist = db.Products.SqlQuery("select *from products").ToList();

            return View(productlist);
        }
        //Add action with List Action ,Product as Model class and DataContext as Context
        //This action will execute when user will click on delete
        public ActionResult Delete(int? ProductId)
        {
            //As per id we will delete product
            var productlist = db.Database.ExecuteSqlCommand("delete from Products where ProductId=@p0", ProductId);

            if (productlist != 0)
            {
                //We will go back to action ProductDelete to show updated records
                return RedirectToAction("ProductDelete");
            }
            return View();
        }
    }
}