using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Common;
using Ecommerce.Entity;

namespace Ecommerce.Repository
{
    public class ProductRepository : DataRepository<Product, int>
    {
        private static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<Product> result = new ResultProcess<Product>();
        BrandRepository br = new BrandRepository();
               
        public override Result<int> Delete(int id)
        {
            Product deleted = db.Products.SingleOrDefault(t => t.ProductId == id);

            Brand brand = br.GetObjById(Convert.ToInt32(deleted.BrandId)).ProcessResult;
            brand.ProductCount--;
            br.Update(brand);

            db.Products.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<Product>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Products.OrderByDescending(t => t.ProductId).Take(Quantity).ToList());
        }

        public override Result<Product> GetObjById(int id)
        {
            return result.GetT(db.Products.SingleOrDefault(t => t.ProductId == id));
        }

        public override Result<int> Insert(Product item)
        {
            Brand brand = br.GetObjById(Convert.ToInt32(item.BrandId)).ProcessResult;
            brand.ProductCount++;
            br.Update(brand);

            db.Products.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Product>> List()
        {
            return result.GetListResult(db.Products.ToList());
        }

        public override Result<int> Update(Product item)
        {
            Product edited = db.Products.SingleOrDefault(t => t.ProductId == item.ProductId);
            edited.BrandId = item.BrandId;
            edited.CategoryId = item.CategoryId;
            edited.GenderId = item.GenderId;
            edited.Price = item.Price;
            edited.Stock = item.Stock;
            edited.Description = item.Description;
            edited.SupplierId = item.SupplierId;
            edited.ProductName = item.ProductName;                                
            return result.GetResult(db);
        }
    }
}
