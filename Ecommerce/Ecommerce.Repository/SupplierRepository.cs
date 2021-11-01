using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Common;
using Ecommerce.Entity;

namespace Ecommerce.Repository
{
    public class SupplierRepository : DataRepository<Supplier, int>
    {
        public static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<Supplier> result = new ResultProcess<Supplier>();
        public override Result<int> Delete(int id)
        {
            Supplier deleted = db.Suppliers.SingleOrDefault(t => t.SupplierId == id);
            db.Suppliers.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<Supplier>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<Supplier> GetObjById(int id)
        {
            return result.GetT(db.Suppliers.SingleOrDefault(t => t.SupplierId == id));
        }

        public override Result<int> Insert(Supplier item)
        {
            db.Suppliers.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Supplier>> List()
        {
            return result.GetListResult(db.Suppliers.ToList());
        }

        public override Result<int> Update(Supplier item)
        {
            Supplier edited = db.Suppliers.SingleOrDefault(t => t.SupplierId == item.SupplierId);
            edited.CompanyName = item.CompanyName;
            return result.GetResult(db);
        }
    }
}
