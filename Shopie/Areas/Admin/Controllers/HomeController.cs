using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using Shopie.Common;
using PagedList;

namespace Shopie.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var userDao = new UserDao();
            ViewBag.Admin = userDao.ListAdmin();
            ViewBag.Customer = userDao.ListCustomer();
            return View();
        }
    }
}