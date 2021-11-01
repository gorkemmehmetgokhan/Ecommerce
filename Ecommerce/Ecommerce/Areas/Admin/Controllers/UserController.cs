using Ecommerce.Common;
using Ecommerce.Entity;
using Ecommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        Result<List<User>> result = new Result<List<User>>();
        UserRepository uRep = new UserRepository();

        public ActionResult List()
        {
            result.ProcessResult = uRep.List().ProcessResult;
            return View(result.ProcessResult);
        }

        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(User model, string sampleradio)
        {                       
            model.Photo = 0.ToString();
            model.UserRoleId = 1;

            if (sampleradio == "Male")
            {
                model.Gender = "Erkek";
            }
            else
            {
                model.Gender = "Kadın";
            }

            if (ModelState.IsValid)
            {
                if (uRep.Insert(model).IsSuccessed)
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

        public ActionResult Delete(int id)
        {
            result.IsSuccessed = uRep.Delete(id).IsSuccessed;
            return RedirectToAction("List");
        }
    }
}