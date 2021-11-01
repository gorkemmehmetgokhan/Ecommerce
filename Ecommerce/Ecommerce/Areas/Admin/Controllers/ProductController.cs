using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Common;
using Ecommerce.Entity;
using Ecommerce.Repository;
using Ecommerce.Areas.Admin.Models.ViewModel;
using System.IO;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product

        ProductRepository pRep = new ProductRepository();
        CategoryRepository cr = new CategoryRepository();
        BrandRepository br = new BrandRepository();
        MediaRepository mr = new MediaRepository();
        ProductMedia pm = new ProductMedia();
        SupplierRepository sr = new SupplierRepository();
        GenderRepository gr = new GenderRepository();
        DB_EcommerceEntities ctx = new DB_EcommerceEntities();
        Result<List<Product>> result = new Result<List<Product>>();

        public ActionResult List()
        {
            result.ProcessResult = pRep.List().ProcessResult;
            return View(result.ProcessResult);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ProductViewModel pwm = new ProductViewModel();
            List<SelectListItem> CatList = new List<SelectListItem>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            List<SelectListItem> SuppList = new List<SelectListItem>();
            List<SelectListItem> GenderList = new List<SelectListItem>();

            foreach (Category item in cr.List().ProcessResult)
            {
                CatList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            foreach (Brand item in br.List().ProcessResult)
            {
                BrandList.Add(new SelectListItem { Value = item.BrandId.ToString(), Text = item.BrandName });
            }
            foreach (Supplier item in sr.List().ProcessResult)
            {
                SuppList.Add(new SelectListItem { Value = item.SupplierId.ToString(), Text = item.CompanyName });
            }
            foreach (Gender item in gr.List().ProcessResult)
            {
                GenderList.Add(new SelectListItem { Value = item.GenderId.ToString(), Text = item.GenderType });
            }
            pwm.BrandList = BrandList;
            pwm.CategoryList = CatList;
            pwm.SuppList = SuppList;
            pwm.GenderList = GenderList;
            pwm.Product = null;
            return View(pwm);
        }
        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model, IEnumerable<HttpPostedFileBase> files)
        {
            var fileName = "";
            int success = 0;

            result.IsSuccessed = pRep.Insert(model.Product).IsSuccessed;

            foreach (var file in files)
            {
                if (file != null)
                {
                    fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(path);

                    pm.ProductId = model.Product.ProductId;
                    pm.Photo = fileName;
                    ctx.ProductMedias.Add(pm);
                    success = ctx.SaveChanges();
                }
                else
                {
                    pm.ProductId = model.Product.ProductId;
                    pm.Photo = 0.ToString();
                    ctx.ProductMedias.Add(pm);
                    success = ctx.SaveChanges();
                }
            }

            if (result.IsSuccessed == true && success > 0)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductViewModel pwm = new ProductViewModel();
            List<SelectListItem> CatList = new List<SelectListItem>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            List<SelectListItem> SuppList = new List<SelectListItem>();
            List<SelectListItem> GenderList = new List<SelectListItem>();

            foreach (Category item in cr.List().ProcessResult)
            {
                CatList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            foreach (Brand item in br.List().ProcessResult)
            {
                BrandList.Add(new SelectListItem { Value = item.BrandId.ToString(), Text = item.BrandName });
            }
            foreach (Supplier item in sr.List().ProcessResult)
            {
                SuppList.Add(new SelectListItem { Value = item.SupplierId.ToString(), Text = item.CompanyName });
            }
            foreach (Gender item in gr.List().ProcessResult)
            {
                GenderList.Add(new SelectListItem { Value = item.GenderId.ToString(), Text = item.GenderType });
            }
            pwm.BrandList = BrandList;
            pwm.CategoryList = CatList;
            pwm.SuppList = SuppList;
            pwm.GenderList = GenderList;
            pwm.Product = pRep.GetObjById(id).ProcessResult;
            return View(pwm);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model, IEnumerable<HttpPostedFileBase> files)
        {       
            result.IsSuccessed = pRep.Update(model.Product).IsSuccessed;
            
            if (result.IsSuccessed == true)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            result.IsSuccessed = pRep.Delete(id).IsSuccessed;
            return RedirectToAction("List");
        }
    }
}