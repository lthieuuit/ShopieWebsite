using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopie.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var productDao = new ProductDao();
            var productCategoryDao = new ProductCategoryDao();
            ViewBag.Products = productDao.ListProduct();
            ViewBag.Category = productCategoryDao.ListAll();
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public JsonResult ListName(string q)
        {
            var dataq = new ProductDao().ListName(q);
            return Json(new
            {
                data = dataq,
                status = true
            } ,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Category(long cateId)
        {

            //ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);

            var productDao = new ProductDao();
            var pdc = productDao.ListCategoryProduct(cateId);
            var productCategoryDao = new ProductCategoryDao();
            ViewBag.Category = productCategoryDao.ListAll();
            ViewBag.CategoryProduct = productDao.ListCategoryProduct(cateId);
            return View();
        }

        public ActionResult Search(string keyword)
        {
            var productDao = new ProductDao();
            var listPro = productDao.ListName(keyword);
            ViewBag.Keyword = productDao.ListName(keyword);
            return View();
        }
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            //ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            return View(product);
        }

    }
}