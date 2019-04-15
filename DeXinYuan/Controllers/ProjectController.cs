using DeXinYuan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeXinYuan.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            var list = SqlHelper.Query<Category>("select * from Category where IsDel=0").ToList();

            var pList = SqlHelper.Query<Product>("select * from Product where IsDel=0 and CategoryId=@CategoryId", new { categoryId = 1}).ToList();

            ViewBag.products = pList;
            return View(list);
        }
    }
}