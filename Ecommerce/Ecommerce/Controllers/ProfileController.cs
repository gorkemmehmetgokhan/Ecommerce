using Ecommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Repository;
using System.Web.Security;
using Ecommerce.Common;

namespace Ecommerce.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        UserRepository uRep = new UserRepository();
        IAddressRepository aRep = new IAddressRepository();
        
        public ActionResult MyOrders(User u)
        {
            DB_EcommerceEntities context = new DB_EcommerceEntities();
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            u.UserId = Convert.ToInt32(ticket.Name);
            var user = context.Users.FirstOrDefault(x => x.UserId == u.UserId);
            return View(user);
        }

        public ActionResult EditPassword(int id)
        {
            return View(uRep.GetObjById(id).ProcessResult);
        }
        [HttpPost]
        public ActionResult EditPassword(User model)
        {
            if (uRep.Update(model).IsSuccessed)
            {
                return RedirectToAction("MyOrders");
            }
            return View(model);
        }

        public ActionResult AddressList()
        {          
            ViewBag.Address = aRep.List().ProcessResult.Where(t => t.UserId == Convert.ToInt32(User.Identity.Name));
            return View();
        }

        public ActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAddress(InvoiceAddress address)
        {
            address.UserId = Convert.ToInt32(User.Identity.Name);
            int success = aRep.Insert(address).ProcessResult;
            if (success > 0)
            {
                return RedirectToAction("AddressList");
            }
            return View();         
        }

        public ActionResult AddressDelete(int id)
        {
            int success= aRep.Delete(id).ProcessResult;
            return RedirectToAction("AddressList");
        }
    }
}