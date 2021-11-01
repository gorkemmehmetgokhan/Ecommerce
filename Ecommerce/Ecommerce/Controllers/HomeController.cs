using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Repository;
using Ecommerce.Areas.Admin.Models.ViewModel;
using Ecommerce.Entity;
using System.Web.Security;
using PagedList;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ProductRepository pRep = new ProductRepository();
        CategoryRepository catRep = new CategoryRepository();
        BrandRepository bRep = new BrandRepository();
        UserRepository uRep = new UserRepository();
        CommentRepository cRep = new CommentRepository();
        public ActionResult Index()
        {
            return View(pRep.GetLatestObj(12).ProcessResult);
        }

        public ActionResult AllProducts(int page = 1)
        {
            List<Product> pList = pRep.List().ProcessResult;
            PagedList<Product> model = new PagedList<Product>(pList, page, 8);
            return View(model);
        }

        public ActionResult ListByBrand(int? id, int page = 1)
        {
            List<Product> pList = pRep.List().ProcessResult.Where(t => t.BrandId == id).ToList();
            PagedList<Product> model = new PagedList<Product>(pList, page, 8);
            return View(model);
        }

        public ActionResult ListByCategory(int? id, int page = 1)
        {
            List<Product> pList = pRep.List().ProcessResult.Where(t => t.CategoryId == id).ToList();
            PagedList<Product> model = new PagedList<Product>(pList, page, 8);
            return View(model);
        }

        public ActionResult ListByGender(int? id, int page = 1)
        {
            List<Product> pList = pRep.List().ProcessResult.Where(t => t.GenderId == id).ToList();
            PagedList<Product> model = new PagedList<Product>(pList, page, 8);
            return View(model);
        }

        public ActionResult ListBySupplier(int? id, int page = 1)
        {
            List<Product> pList = pRep.List().ProcessResult.Where(t => t.SupplierId == id).ToList();
            PagedList<Product> model = new PagedList<Product>(pList, page, 8);
            return View(model);
        }
        public ActionResult ProductDetail(int id)
        {
            Product p = pRep.GetObjById(id).ProcessResult;
            return View(p);
        }
        public ActionResult Login()
        {
            //kullanıcının oturum acma islemi basarili ise
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = User.Identity.Name;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            //validation control
            if (ModelState.IsValid)
            {
                using (DB_EcommerceEntities ctx = new DB_EcommerceEntities())
                {
                    var user = ctx.Users.FirstOrDefault(x => x.Email == u.Email && x.Password == u.Password);
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Fullname, true);
                        FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user, string sampleradio)
        {          
            if (sampleradio == "Male")
            {
                user.Gender = "Erkek";
            }
            else
            {
                user.Gender = "Kadın";
            }
            user.UserRoleId = 2;
            int success = uRep.Insert(user).ProcessResult;
            if (success > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ProductComment(Comment c)
        {
            if (User.Identity.IsAuthenticated)
            {
                c.UserId = Convert.ToInt32(User.Identity.Name);
            }
            else
            {
                c.UserId = 5;
            }
            c.PostTime = DateTime.Now;
            c.IsApproved = false;

            int success = cRep.Insert(c).ProcessResult;
            if (success > 0)
            {
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Search(string searchProduct, int page = 1)
        {
            ViewBag.Search = searchProduct;
            List<Product> listProduct = pRep.List().ProcessResult.
            Where(t => t.ProductName.ToLower().Contains(searchProduct.ToLower()) || t.Category.CategoryName.ToLower().Contains(searchProduct.ToLower()) || t.Brand.BrandName.ToLower().Contains(searchProduct.ToLower())).ToList();
            PagedList<Product> product = new PagedList<Product>(listProduct, page, 8);
            ViewBag.Product = product;
            return View(ViewBag.Product);
        }


        [HttpGet]
        public ActionResult SearchByPrice(int priceMin, int priceMax, int page = 1)
        {
            ViewBag.SearchMin = priceMin;
            ViewBag.SearchMax = priceMax;
            List<Product> listProduct = pRep.List().ProcessResult.
            Where(t => t.Price >= priceMin && t.Price <= priceMax).ToList();
            PagedList<Product> product = new PagedList<Product>(listProduct, page, 4);
            ViewBag.Product = product;
            return View(ViewBag.Product);
        }
    }
}