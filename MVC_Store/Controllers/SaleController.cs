using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class SaleController : Controller
    {
        Db_MVC_StoreEntities entities = new Db_MVC_StoreEntities();
        // GET: Sale
        [Authorize]
        public ActionResult Index()
        {
            var values = entities.tblSales.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult Add() 
        {
            List<SelectListItem> product = (from i in entities.tblProducts.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.productName,
                                                Value = i.productId.ToString()
                                            }).ToList();
            List<SelectListItem> employee = (from i in entities.tblEmployees.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.employeeName,
                                                 Value = i.employeeId.ToString()
                                             }).ToList();
            List<SelectListItem> customer = (from i in entities.tblCustomers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.customerName,
                                                 Value = i.customerId.ToString()
                                             }).ToList();

            ViewBag.prd =product;
            ViewBag.emp =employee;
            ViewBag.cst =customer;
            return View();
        }
        [HttpPost]
        public ActionResult Add(tblSale sale)
        {
            var prd = entities.tblProducts.Where(p => p.productId == sale.tblProduct.productId).FirstOrDefault();
            sale.tblProduct = prd;
            var emp = entities.tblEmployees.Where(e => e.employeeId == sale.tblEmployee.employeeId).FirstOrDefault();
            sale.tblEmployee = emp;
            var cst = entities.tblCustomers.Where(c => c.customerId == sale.tblCustomer.customerId).FirstOrDefault();
            sale.tblCustomer = cst;
            sale.saleDate = DateTime.Now;
            entities.tblSales.Add(sale);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var value = entities.tblSales.Find(id);
            entities.tblSales.Remove(value);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Bring(int id)
        {
            var value = entities.tblSales.Find(id);

            List<SelectListItem> product = (from i in entities.tblProducts.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.productName,
                                                Value = i.productId.ToString()
                                            }).ToList();
            List<SelectListItem> employee = (from i in entities.tblEmployees.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.employeeName,
                                                 Value = i.employeeId.ToString()
                                             }).ToList();
            List<SelectListItem> customer = (from i in entities.tblCustomers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.customerName,
                                                 Value = i.customerId.ToString()
                                             }).ToList();

            ViewBag.prd = product;
            ViewBag.emp = employee;
            ViewBag.cst = customer;

            return View("Bring",value);
        }

        public ActionResult Edit(tblSale sale)
        {
            var prd = entities.tblProducts.Where(p => p.productId == sale.tblProduct.productId).FirstOrDefault();
            sale.tblProduct = prd;
            var emp = entities.tblEmployees.Where(e => e.employeeId == sale.tblEmployee.employeeId).FirstOrDefault();
            sale.tblEmployee = emp;
            var cst = entities.tblCustomers.Where(c => c.customerId == sale.tblCustomer.customerId).FirstOrDefault();
            sale.tblCustomer = cst;

            var value = entities.tblSales.Find(sale.saleId);
            value.saleProduct = sale.tblProduct.productId;
            value.saleEmployee = sale.tblEmployee.employeeId;
            value.saleCustomer = sale.tblCustomer.customerId;
            value.saleAmount = sale.saleAmount;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}