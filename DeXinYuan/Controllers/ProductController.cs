using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeXinYuan.Models;

namespace DeXinYuan.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(int categoryId=0)
        {
            var list = SqlHelper.Query<Category>("select * from Category where IsDel=0").ToList();

            var pList = SqlHelper.Query<Product>("select * from Product where IsDel=0 and CategoryId=@CategoryId", new { categoryId= categoryId==0?list[0].Id: categoryId }).ToList();

            ViewBag.categoryId = categoryId == 0 ? list[0].Id : categoryId;
            ViewBag.products = pList;
            return View(list);
        }

        public ActionResult Detail(int id)
        {
            return View();
        }
    }
}