using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSM2.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        #region ActionResult
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public ActionResult Create()
        {
            SetCategoryViewBag();
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                if (ProductDao.Instance.insert(model))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm thất bại!");
                }
            }
            SetCategoryViewBag();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var product = ProductDao.Instance.getByID(id);
            SetCategoryViewBag(product.CateID);
            return View(product);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                if (ProductDao.Instance.Update(model))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa sản phẩm thất bại!");
                }
            }
            SetCategoryViewBag(model.CateID);
            return View(model);
        }

        public void SetCategoryViewBag(int? selectedID = null)
        {
            var dao = new CategoryDAO();
            var listCategory = dao.GetListActive();
            ViewBag.CategoryID = new SelectList(listCategory, "CateID", "CateName", selectedID);
        }

        #endregion

        #region JsonResult

        public JsonResult ChangeStatus(int id)
        {
            bool result = false;
            try
            {
                result = ProductDao.Instance.changeStatus(id);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var result = ProductDao.Instance.delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

	}
}