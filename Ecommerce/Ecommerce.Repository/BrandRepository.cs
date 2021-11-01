using Ecommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Common;

namespace Ecommerce.Repository
{
    public class BrandRepository : DataRepository<Brand, int>
    {
        public static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<Brand> result = new ResultProcess<Brand>();
        public override Result<int> Delete(int id)
        {
            Brand deleted = db.Brands.SingleOrDefault(t => t.BrandId == id);
            db.Brands.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<Brand>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Brands.OrderByDescending(t => t.BrandId).Take(Quantity).ToList());
        }

        public override Result<Brand> GetObjById(int id)
        {
            return result.GetT(db.Brands.SingleOrDefault(t => t.BrandId == id));
        }

        public override Result<int> Insert(Brand item)
        {
            item.ProductCount = 0;
            db.Brands.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Brand>> List()
        {
            return result.GetListResult(db.Brands.ToList());
        }

        public override Result<int> Update(Brand item)
        {
            Brand edited = db.Brands.SingleOrDefault(t => t.BrandId == item.BrandId);
            edited.BrandName = item.BrandName;
            edited.Description = item.Description;
            edited.Photo = item.Photo;
            edited.ProductCount = item.ProductCount;
            return result.GetResult(db);
        }
    }
}
