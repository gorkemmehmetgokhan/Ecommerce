using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Repository;
using Ecommerce.Entity;
using Ecommerce.Common;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository cRep = new CategoryRepository();
        Result<List<Category>> result = new Result<List<Category>>();
        DB_EcommerceEntities ctx = new DB_EcommerceEntities();

        public ActionResult List(string mesaj)
        {
            result.ProcessResult = cRep.List().ProcessResult;
            ViewBag.Mesaj = mesaj;
            return View(result.ProcessResult);
        }

        public ActionResult AddCategory()
        {         
            ViewBag.Categories = ctx.Categories.ToList(); //For Parent Category
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category model)
        {          
            model.CreatedDate = DateTime.Now;         
            result.IsSuccessed = cRep.Insert(model).IsSuccessed;
            return RedirectToAction("AddCategory");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = ctx.Categories.ToList();
            return View(cRep.GetObjById(id).ProcessResult);
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            result.IsSuccessed = cRep.Update(model).IsSuccessed;
            return RedirectToAction("List","Category");
        }
        public ActionResult Delete(int id)
        {
            result.IsSuccessed = cRep.Delete(id).IsSuccessed;
            return RedirectToAction("List", new { @mesaj = result.UserMessage });
        }
    }
}