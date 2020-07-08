using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopie.Areas.Admin.Controllers
{
    public class RevenueController : BaseController
    {
        // GET: Admin/Revenue
        public ActionResult Index()
        {
            return View();
        }
    }
}