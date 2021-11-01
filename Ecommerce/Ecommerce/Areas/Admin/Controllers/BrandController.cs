using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Entity;
using Ecommerce.Repository;
using Ecommerce.Common;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        Result<List<Brand>> result = new Result<List<Brand>>();
      
        BrandRepository bRep = new BrandRepository();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            result.ProcessResult = bRep.List().ProcessResult;
            return View(result.ProcessResult);
        }

        public ActionResult AddBrand()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddBrand(Brand model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";
            if (photoPath.ContentLength > 0 & photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
            }
            model.Photo = PhotoName;
            if (ModelState.IsValid)
            {
                
                if (bRep.Insert(model).IsSuccessed)
                    return RedirectToAction("List");
                else
                {
                    ViewBag.Mesaj = result.UserMessage;
                    return View(model);
                }
            }
            else
                return View();
        }

        public ActionResult Edit(int id)
        {
            return View(bRep.GetObjById(id).ProcessResult);
        }
        [HttpPost]
        public ActionResult Edit(Brand model, HttpPostedFileBase PhotoPath)
        {
            string PhotoName = model.Photo;
            if (PhotoPath.ContentLength > 0 & PhotoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                PhotoPath.SaveAs(path);
            }
            model.Photo = PhotoName;
         
            if (bRep.Update(model).IsSuccessed)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            result.IsSuccessed = bRep.Delete(id).IsSuccessed;
            return RedirectToAction("List");
        }
    }
}