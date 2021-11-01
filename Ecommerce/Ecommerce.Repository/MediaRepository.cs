using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entity;
using Ecommerce.Common;

namespace Ecommerce.Repository
{
    public class MediaRepository : DataRepository<ProductMedia, int>
    {
        public static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<ProductMedia> result = new ResultProcess<ProductMedia>();
        public override Result<int> Delete(int id)
        {
            ProductMedia deleted = db.ProductMedias.SingleOrDefault(t => t.ProductMediaId == id);
            db.ProductMedias.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<ProductMedia>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.ProductMedias.OrderByDescending(t => t.ProductMediaId).Take(Quantity).ToList());
        }

        public override Result<ProductMedia> GetObjById(int id)
        {
            return result.GetT(db.ProductMedias.SingleOrDefault(t => t.ProductMediaId == id));
        }

        public override Result<int> Insert(ProductMedia item)
        {
            db.ProductMedias.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<ProductMedia>> List()
        {
            return result.GetListResult(db.ProductMedias.ToList());
        }

        public override Result<int> Update(ProductMedia item)
        {
            ProductMedia edited = db.ProductMedias.FirstOrDefault(t => t.ProductMediaId == item.ProductMediaId);
            edited.ProductId = item.ProductId;
            edited.Photo = item.Photo;
            edited.Video = item.Video;          
            return result.GetResult(db);
        }
    }
}
