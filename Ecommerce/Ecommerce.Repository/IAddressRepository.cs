using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Common;
using Ecommerce.Entity;

namespace Ecommerce.Repository
{
    public class IAddressRepository : DataRepository<InvoiceAddress, int>
    {
        public static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<InvoiceAddress> result = new ResultProcess<InvoiceAddress>();
        public override Result<int> Delete(int id)
        {
            InvoiceAddress deleted = db.InvoiceAddresses.SingleOrDefault(t => t.AddressId == id);
            db.InvoiceAddresses.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<InvoiceAddress>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<InvoiceAddress> GetObjById(int id)
        {
            return result.GetT(db.InvoiceAddresses.SingleOrDefault(t => t.AddressId == id));

        }

        public override Result<int> Insert(InvoiceAddress item)
        {
            db.InvoiceAddresses.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<InvoiceAddress>> List()
        {
            return result.GetListResult(db.InvoiceAddresses.ToList());
        }

        public static List<InvoiceAddress> ListAddress()
        {
            return db.InvoiceAddresses.ToList();
        }

        public override Result<int> Update(InvoiceAddress item)
        {
            InvoiceAddress edited = db.InvoiceAddresses.SingleOrDefault(t => t.AddressId == item.AddressId);
            edited.AddressHeader = item.AddressHeader;
            edited.UserId = item.UserId;
            edited.Description = item.Description;
            edited.Phone = item.Phone;
            return result.GetResult(db);
        }
    }
}
