using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Repository;
using Ecommerce.Entity;
using Ecommerce.Common;
using Ecommerce.Areas.Admin.Models.ViewModel;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class SliderController : Controller
    {
        // GET: Admin/Slider
        SliderRepository sRep = new SliderRepository();
        Result<List<Slider>> result = new Result<List<Slider>>();
        CategoryRepository cr = new CategoryRepository();
        BrandRepository br = new BrandRepository();
        ProductRepository pr = new ProductRepository();
        public ActionResult List()
        {
            result.ProcessResult = sRep.List().ProcessResult;
            return View(result.ProcessResult);
        }

        [HttpGet]
        public ActionResult AddSlider()
        {
            SliderViewModel pwm = new SliderViewModel();
            List<SelectListItem> CatList = new List<SelectListItem>();
            //List<SelectListItem> BrandList = new List<SelectListItem>();
            //List<SelectListItem> ProductList = new List<SelectListItem>();
          
            foreach (Category item in cr.List().ProcessResult)
            {
                CatList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            //foreach (Brand item in br.List().ProcessResult)
            //{
            //    BrandList.Add(new SelectListItem { Value = item.BrandId.ToString(), Text = item.BrandName });
            //}
            //foreach (Product item in pr.List().ProcessResult)
            //{
            //    ProductList.Add(new SelectListItem { Value = item.ProductId.ToString(), Text = item.ProductName });
            //}
          
            //pwm.BrandList = BrandList; 
            //pwm.ProductList = ProductList;

            pwm.CategoryList = CatList;
            pwm.Slider = null;
            return View(pwm);
        }

        [HttpPost]
        public ActionResult AddSlider(SliderViewModel model, HttpPostedFileBase photo)
        {
            string PhotoName = "";
            if (photo != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photo.SaveAs(path);
            }

            model.Slider.Photo = PhotoName;
            if (photo == null)
            {
                model.Slider.Photo = 0.ToString();
            }
       
            int success = sRep.Insert(model.Slider).ProcessResult;
            if (success > 0)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditSlider(int id)
        {
            SliderViewModel pwm = new SliderViewModel();
            List<SelectListItem> CatList = new List<SelectListItem>();
           

            foreach (Category item in cr.List().ProcessResult)
            {
                CatList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
       
            pwm.CategoryList = CatList;
            pwm.Slider = sRep.GetObjById(id).ProcessResult;
            return View(pwm);
        }

        [HttpPost]
        public ActionResult EditSlider(SliderViewModel model, HttpPostedFileBase PhotoPath)
        {
            string PhotoName = model.Slider.Photo;
            if (PhotoPath.ContentLength > 0 & PhotoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                PhotoPath.SaveAs(path);
            }
            model.Slider.Photo = PhotoName;

            result.IsSuccessed = sRep.Update(model.Slider).IsSuccessed;

            if (result.IsSuccessed == true)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            result.IsSuccessed = sRep.Delete(id).IsSuccessed;
            return RedirectToAction("List");
        }
    }
}