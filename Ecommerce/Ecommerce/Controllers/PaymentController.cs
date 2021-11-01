using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Repository;
using Ecommerce.Entity;


namespace Ecommerce.Controllers
{
    public class PaymentController : Controller
    {
        //GET: Payment

        [HttpGet]
        public ActionResult Pay()
        {
            ViewBag.AddressTypes = new SelectList(IAddressRepository.ListAddress().Where(t => t.UserId == Convert.ToInt32(User.Identity.Name)), "AddressId", "AddressHeader");
            ViewBag.PaymentTypes = new SelectList(PaymentRepository.List(), "PaymentId", "PaymentName");
            return View();
        }

        [HttpPost]
        public ActionResult Pay(Invoice model, List<string> PaymentTypes, List<string> AddressTypes)
        {
            model.PaymentDate = DateTime.Now;
            foreach (string item in PaymentTypes)
            {
                int PaymentId = Convert.ToInt32(item);
                model.PaymentTypeId = PaymentId;
            }

            foreach (string item in AddressTypes)
            {
                int AddressId = Convert.ToInt32(item);
                model.AddressId = AddressId;
            }

            model.OrderId = ((Order)Session["Order"]).OrderId;
            InvoiceRepository ip = new InvoiceRepository();
            if (ip.Insert(model).IsSuccessed)
            {
                Order ord = (Order)Session["Order"];
                OrderRepository ordrep = new OrderRepository();
                ord.IsPay = true;
                ord.UserId = Convert.ToInt32(User.Identity.Name);
                ordrep.Update(ord);
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}