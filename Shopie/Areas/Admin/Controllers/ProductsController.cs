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
    public class ProductsController : BaseController
    {
        // GET: Admin/Products
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var sp = new ProductDao().ViewDetails(id);
            return View(sp);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
               

                long id = dao.Insert(product);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm thành công");
                }

            }
            return RedirectToAction("Index", "Products");

        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.Update(product);
                if (result)
                {
                    ModelState.AddModelError("", "Cập nhật thành công");

                    return RedirectToAction("Index","Products");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }

            }
            return RedirectToAction("Index", "Products");

        }
    }
}