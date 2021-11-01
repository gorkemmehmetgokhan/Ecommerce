using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Common;
using Ecommerce.Entity;

namespace Ecommerce.Repository
{
    public class CategoryRepository : DataRepository<Category, int>
    {
        private static DB_EcommerceEntities db = Tools.GetConnection();
        
        ResultProcess<Category> result = new ResultProcess<Category>();
        public override Result<int> Delete(int id)
        {
            Category deleted = db.Categories.SingleOrDefault(t => t.CategoryId == id);
            db.Categories.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<Category>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Categories.OrderByDescending(t => t.CreatedDate).Take(Quantity).ToList());
        }

        public override Result<Category> GetObjById(int id)
        {
            Category c = db.Categories.SingleOrDefault(t => t.CategoryId == id);
            return result.GetT(c);
        }

        public override Result<int> Insert(Category item)
        {
            db.Categories.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Category>> List()
        {
            List<Category> CatList = db.Categories.ToList();
            return result.GetListResult(CatList);
        }

        public override Result<int> Update(Category item)
        {
            Category c = db.Categories.SingleOrDefault(t => t.CategoryId == item.CategoryId);
            c.CategoryName = item.CategoryName;
            c.Description = item.Description;
            c.ParentId = item.ParentId;
            return result.GetResult(db);
        }
    }
}
