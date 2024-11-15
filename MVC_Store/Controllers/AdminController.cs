using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class AdminController : Controller
    {
        Db_MVC_StoreEntities entities = new Db_MVC_StoreEntities();
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(tblAdmin admin)
        {
            entities.tblAdmins.Add(admin);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}