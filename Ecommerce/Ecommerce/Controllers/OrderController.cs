using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Entity;
using Ecommerce.Repository;
using Ecommerce.Common;

namespace Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        OrderRepository or = new OrderRepository();
        OrderDetailRepository odrep = new OrderDetailRepository();
        ProductRepository pr = new ProductRepository();
        public ActionResult Add(int id, int quantity)
        {
            if (Session["Order"] != null && ((Order)Session["Order"]).OrderId == 0)
            {
                Session["Order"] = null;
            }

            //Sepeti Session içersinde tutuyorum. Order(Session[Order])
            if (Session["Order"] == null)
            {
                Order o = new Order();
                o.OrderDate = DateTime.Now;
                o.IsPay = false;
                or.Insert(o);

                Session["Order"] = or.GetLatestObj(1).ProcessResult[0];
                OrderDetail od = new OrderDetail();
                od.OrderId = ((Order)Session["Order"]).OrderId;
                od.ProductId = id;
                od.Quantity = quantity;
                od.Price = pr.GetObjById(id).ProcessResult.Price * quantity;
                odrep.Insert(od);
            }
            else
            {
                Order o = (Order)Session["Order"];
                OrderDetail Update = odrep.GetOrderDetailsByTwoId(o.OrderId, id).ProcessResult;
                // Eğer farklı bir ürün sepete ekleniyorsa
                if (Update == null)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderId = o.OrderId;
                    od.ProductId = id;
                    od.Quantity = quantity;
                    od.Price = pr.GetObjById(id).ProcessResult.Price * quantity;
                    odrep.Insert(od);
                }
                // Eğer aynı üründen birden fazla sepete ekleniyor ise
                else
                {
                    Update.Quantity++;
                    Update.Price += pr.GetObjById(id).ProcessResult.Price;
                    odrep.Update(Update);
                }
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult DetailList()
        {
            Order orderBos = new Order();
            Order sepetim = (Order)Session["Order"];
            decimal? TotalPrice = 0;
            OrderRepository or = new OrderRepository();

            if (sepetim == null)
            {
                orderBos.OrderId = 0;
                orderBos.OrderDate = DateTime.Now;
                orderBos.TotalPrice = 0;
                orderBos.IsPay = false;
                Session["Order"] = orderBos;
                return View(orderBos.OrderDetails);
            }

            if (sepetim.OrderDetails != null && ((Order)Session["Order"]).OrderId != 0)
            {
                foreach (OrderDetail item in sepetim.OrderDetails)
                {
                    TotalPrice += item.Price;
                }
                if (TotalPrice >= 100)
                {
                    sepetim.TotalPrice = TotalPrice;
                }
                else if (TotalPrice != 0)
                {
                    sepetim.TotalPrice = TotalPrice + 10;
                }
                else
                {
                    sepetim.TotalPrice = 0;
                }
                or.Update(sepetim);
            }
            else if (((Order)Session["Order"]).OrderId != 0)
            {
                sepetim.TotalPrice = 0;
                or.Update(sepetim);
            }
            return View(sepetim.OrderDetails);
        }

        public ActionResult Delete(int id)
        {
            Order sepetim = (Order)Session["Order"];
            Result<int> result = odrep.OrderDetailDelete(sepetim.OrderId, id);
            return RedirectToAction("DetailList");
        }

        public ActionResult QuantityDown(int id)
        {
            if (Session["Order"] != null)
            {
                Order o = (Order)Session["Order"];
                OrderDetail Update = odrep.GetOrderDetailsByTwoId(o.OrderId, id).ProcessResult;

                if (Update.Quantity != 1)
                {
                    Update.Quantity--;
                    Update.Price -= pr.GetObjById(id).ProcessResult.Price;
                    odrep.Update(Update);
                }
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}