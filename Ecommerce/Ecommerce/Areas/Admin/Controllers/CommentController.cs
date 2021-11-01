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
    public class CommentController : Controller
    {
        // GET: Admin/Comment
        Result<List<Comment>> result = new Result<List<Comment>>();
        CommentRepository cRep = new CommentRepository();
        public ActionResult List()
        {
            return View(cRep.List().ProcessResult.Where(t => t.IsApproved == true));
        }

        [HttpGet]
        public ActionResult ApprovedComments()
        {
            return View(cRep.List().ProcessResult.Where(t => t.IsApproved == false));
        }

        public ActionResult Approved(int id)
        {
            Comment c = new Comment();
            c.IsApproved = true;
            c.CommentId = id;
            c.UserId = cRep.GetObjById(id).ProcessResult.UserId;
            c.ProductId = cRep.GetObjById(id).ProcessResult.ProductId;
            c.Text = cRep.GetObjById(id).ProcessResult.Text;
            result.IsSuccessed = cRep.Update(c).IsSuccessed;
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Delete(int id)
        {
            result.IsSuccessed = cRep.Delete(id).IsSuccessed;
            return RedirectToAction("List");
        }
    }
}