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
    public class BillController : BaseController
    {
        // GET: Admin/Bill
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BillDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new BillDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(Order bill1)
        {
            if (ModelState.IsValid)
            {
                var dao = new BillDao();
                long id = dao.Insert(bill1);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Bill");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm hóa đơn thành công");
                }

            }
            return RedirectToAction("Index", "Bill");

        }
        [HttpPost]
        public ActionResult Edit(Order bill1)
        {
            if (ModelState.IsValid)
            {
                var dao = new BillDao();

                var result = dao.Update(bill1);
                if (result)
                {
                    return RedirectToAction("Index", "Bill");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thành công");
                }

            }
            return RedirectToAction("Index", "Bill");

        }
        public ActionResult Detail(long id)
        {
            var bill = new BillDetailDao().ViewDetail(id);
            //ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            return View(bill);
        }
    }
}