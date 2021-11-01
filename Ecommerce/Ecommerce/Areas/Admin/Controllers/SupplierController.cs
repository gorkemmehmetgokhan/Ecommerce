using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Entity;
using Ecommerce.Common;
using Ecommerce.Repository;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        Result<List<Supplier>> result = new Result<List<Supplier>>();
        SupplierRepository sRep = new SupplierRepository();
        public ActionResult List()
        {
            result.ProcessResult = sRep.List().ProcessResult;
            return View(result.ProcessResult);
        }

        public ActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSupplier(Supplier model)
        {
            if (ModelState.IsValid)
            {

                if (sRep.Insert(model).IsSuccessed)
                    return RedirectToAction("List");
                else
                {
                    return View(model);
                }
            }
            else
                return View();
        }

        public ActionResult Edit(int id)
        {
            return View(sRep.GetObjById(id).ProcessResult);
        }

        [HttpPost]
        public ActionResult Edit(Supplier model)
        {
            if (sRep.Update(model).IsSuccessed)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            result.IsSuccessed = sRep.Delete(id).IsSuccessed;
            return RedirectToAction("List", new { @mesaj = result.UserMessage });
        }
    }
}