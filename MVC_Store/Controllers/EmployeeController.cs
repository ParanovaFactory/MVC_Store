using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class EmployeeController : Controller
    {
        Db_MVC_StoreEntities entities = new Db_MVC_StoreEntities();
        // GET: Employee
        [Authorize]
        public ActionResult Index()
        {
            var values = entities.tblEmployees.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(tblEmployee employee)
        {
            entities.tblEmployees.Add(employee);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id) 
        {
            var value = entities.tblEmployees.Find(id);
            entities.tblEmployees.Remove(value);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Bring(int id) 
        {
            var value = entities.tblEmployees.Find(id);
            return View("Bring",value);
        }

        public ActionResult Edit(tblEmployee employee) 
        {
            var value = entities.tblEmployees.Find(employee.employeeId);
            value.employeeName = employee.employeeName;
            value.employeeSurname = employee.employeeSurname;
            value.employeeDepartment = employee.employeeDepartment;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}