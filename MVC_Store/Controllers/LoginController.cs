using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_Store.Controllers
{
    public class LoginController : Controller
    {
        Db_MVC_StoreEntities entities = new Db_MVC_StoreEntities();
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(tblAdmin admin)
        {
            var value = entities.tblAdmins.FirstOrDefault(x => x.userName == admin.userName && x.password == admin.password);
            if (value != null)
            {
                FormsAuthentication.SetAuthCookie(value.userName, false);
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout(int id)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}