using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class CategoryController : Controller
    {
        Db_MVC_StoreEntities entities = new Db_MVC_StoreEntities();
        // GET: Category
        [Authorize]
        public ActionResult Index()
        {
            var values = entities.tblCategories.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(tblCategory category)
        {
            entities.tblCategories.Add(category);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var value = entities.tblCategories.Find(id);
            entities.tblCategories.Remove(value);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Bring(int id)
        {
            var value = entities.tblCategories.Find(id);
            return View("Bring", value);
        }

        public ActionResult Edit(tblCategory category) 
        {
            var value = entities.tblCategories.Find(category.categoryId);
            value.categoryName = category.categoryName;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
     }
}