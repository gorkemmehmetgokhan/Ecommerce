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
    public class MediaController : Controller
    {
        // GET: Admin/Media
        Result<List<ProductMedia>> result = new Result<List<ProductMedia>>();
        MediaRepository mRep = new MediaRepository();
        public ActionResult List()
        {
            result.ProcessResult = mRep.List().ProcessResult;
            return View(result.ProcessResult);
        }

        public ActionResult Edit(int id)
        {
            return View(mRep.GetObjById(id).ProcessResult);
        }
        [HttpPost]
        public ActionResult Edit(ProductMedia model, HttpPostedFileBase PhotoPath)
        {
            string PhotoName = model.Photo;
            if (PhotoPath.ContentLength > 0 & PhotoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                PhotoPath.SaveAs(path);
            }
            model.Photo = PhotoName;

            if (mRep.Update(model).IsSuccessed)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            result.IsSuccessed = mRep.Delete(id).IsSuccessed;
            return RedirectToAction("List");
        }
    }
}