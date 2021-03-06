using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entity;
using Ecommerce.Common;

namespace Ecommerce.Repository
{
    public class InvoiceRepository : DataRepository<Invoice, int>
    {
        private static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<Invoice> result = new ResultProcess<Invoice>();
        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<List<Invoice>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<Invoice> GetObjById(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(Invoice item)
        {
            db.Invoices.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Invoice>> List()
        {
            throw new NotImplementedException();
        }

        public override Result<int> Update(Invoice item)
        {
            throw new NotImplementedException();
        }
    }
}
