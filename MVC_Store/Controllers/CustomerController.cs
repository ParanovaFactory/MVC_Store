using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class CustomerController : Controller
    {
        Db_MVC_StoreEntities entities = new Db_MVC_StoreEntities();
        // GET: Customer
        [Authorize]
        public ActionResult Index()
        {
            var values = entities.tblCustomers.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(tblCustomer customer)
        {
            entities.tblCustomers.Add(customer);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var value = entities.tblCustomers.Find(id);
            entities.tblCustomers.Remove(value);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Bring(int id)
        {
            var value = entities.tblCustomers.Find(id);
            return View("Bring",value);
        }

        public ActionResult Edit(tblCustomer customer) 
        {
            var value = entities.tblCustomers.Find(customer.customerId);
            value.customerName = customer.customerName;
            value.customerSurname = customer.customerSurname;
            value.customerCity = customer.customerCity;
            value.customerAmount = customer.customerAmount;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}