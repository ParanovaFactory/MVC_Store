using MVC_Store.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class ProductController : Controller
    {
        Db_MVC_StoreEntities entities = new Db_MVC_StoreEntities();
        // GET: Product
        [Authorize]
        public ActionResult Index(string p)
        {
            // var values = entities.tblProducts.ToList();
            var values = from x in entities.tblProducts select x;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.productName.Contains(p));
            }
            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            List<SelectListItem> categoy = (from i in entities.tblCategories.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.categoryName,
                                                Value = i.categoryId.ToString()
                                            }).ToList();
            ViewBag.ctg = categoy;
            return View();
        }
        [HttpPost]
        public ActionResult Add(tblProduct product)
        {
            var ctg = entities.tblCategories.Where(c => c.categoryId == product.tblCategory.categoryId).FirstOrDefault();
            product.tblCategory = ctg;
            entities.tblProducts.Add(product);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var value = entities.tblProducts.Find(id);
            entities.tblProducts.Remove(value);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Bring(int id)
        {
            var value = entities.tblProducts.Find(id);
            List<SelectListItem> categoy = (from i in entities.tblCategories.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.categoryName,
                                                Value = i.categoryId.ToString()
                                            }).ToList();
            ViewBag.ctg = categoy;
            return View("Bring", value);
        }

        public ActionResult Edit(tblProduct product)
        {
            var ctg = entities.tblCategories.Where(c => c.categoryId == product.tblCategory.categoryId).FirstOrDefault();
            product.tblCategory = ctg;
            var value = entities.tblProducts.Find(product.productId);
            value.productName = product.productName;
            value.productBrand = product.productBrand;
            value.productStock = product.productStock;
            value.productBuyPrice = product.productBuyPrice;
            value.productSellPrice = product.productSellPrice;
            value.productCategory = product.tblCategory.categoryId;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}