using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSM2.Models;
using Model.DAO;
using Models.Common;

namespace CSM2.Controllers
{
    public class ProductController : Controller
    {
        private const string CartSession = "CartSession";

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult Product(string search_kw = "", int pageIndex = 1)
        {
            //var category = new CategoryDAO().ViewDetail(productId);
            //ViewBag.Category = category;
            int totalRecord = 0;///tong ban ghi cua danh muc
            var product = new ProductDao().ListByCategoryId(ref totalRecord, pageIndex, search_kw);
            ViewBag.Total = totalRecord;
            ViewBag.Page = pageIndex;

            int maxPage = 3;//so trang hien thi toi da treng trang
            int totalPage = 0; //tong so trang tính ra
            totalPage = (int)Math.Ceiling((double)(totalRecord / Constants.PageSize)) + 1;//chia tong ban ghi cho so luong tren trang, làm tron len
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;//trang cuoi cung
            ViewBag.Next = pageIndex + 1;
            ViewBag.Prev = pageIndex - 1;
            return View(product);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string search_kw, int pageIndex = 1)
        {
            //var category = new CategoryDAO().ViewDetail(productId);
            //ViewBag.Category = category;
            int totalRecord = 0;///tong ban ghi cua danh muc
            var search = new ProductDao().Search(search_kw, ref totalRecord, pageIndex);
            ViewBag.Total = totalRecord;
            ViewBag.Page = pageIndex;
            ViewBag.Keyword = search_kw;

            int maxPage = 3;//so trang hien thi toi da treng trang
            int totalPage = 0; //tong so trang tính ra
            totalPage = (int)Math.Ceiling((double)(totalRecord / Constants.PageSize)) + 1;//chia tong ban ghi cho so luong tren trang, làm tron len
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;//trang cuoi cung
            ViewBag.Next = pageIndex + 1;
            ViewBag.Prev = pageIndex - 1;
            return View(search);
        }
	}
}