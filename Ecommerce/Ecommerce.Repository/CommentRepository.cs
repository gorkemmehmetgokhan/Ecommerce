using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Common;
using Ecommerce.Entity;

namespace Ecommerce.Repository
{
    public class CommentRepository : DataRepository<Comment, int>
    {
        public static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<Comment> result = new ResultProcess<Comment>();
        public override Result<int> Delete(int id)
        {
            Comment deleted = db.Comments.SingleOrDefault(t => t.CommentId == id);
            db.Comments.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<Comment>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Comments.OrderByDescending(t => t.CommentId).Take(Quantity).ToList());
        }

        public override Result<Comment> GetObjById(int id)
        {
            return result.GetT(db.Comments.SingleOrDefault(t => t.CommentId == id));
        }

        public override Result<int> Insert(Comment item)
        {
            db.Comments.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Comment>> List()
        {
            return result.GetListResult(db.Comments.ToList());
        }

        public override Result<int> Update(Comment item)
        {
            Comment edited = db.Comments.SingleOrDefault(t => t.CommentId == item.CommentId);
            edited.UserId = item.UserId;
            edited.ProductId = item.ProductId;
            edited.Text = item.Text;
            edited.IsApproved = item.IsApproved;
            return result.GetResult(db);
        }
    }
}
