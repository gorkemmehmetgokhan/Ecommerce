using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entity;
using Ecommerce.Common;

namespace Ecommerce.Repository
{
    public class OrderDetailRepository : DataRepository<OrderDetail, int>
    {
        private static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<OrderDetail> result = new ResultProcess<OrderDetail>();
        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
            //List<OrderDetail> OdList = db.OrderDetails.Where(t => t.OrderId == id).ToList();
            //db.OrderDetails.RemoveRange(OdList);
            //return result.GetResult(db);
        }

        public override Result<List<OrderDetail>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<OrderDetail> GetObjById(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(OrderDetail item)
        {
            db.OrderDetails.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<OrderDetail>> List()
        {
            return result.GetListResult(db.OrderDetails.ToList());
        }

        public override Result<int> Update(OrderDetail item)
        {
            OrderDetail od = db.OrderDetails.SingleOrDefault(t => t.OrderId == item.OrderId & t.ProductId == item.ProductId);
            od.Quantity = item.Quantity;
            od.Price = item.Price;
            return result.GetResult(db);
        }

        public Result<int> OrderDetailDelete(int OrdId, int ProId)
        {

            OrderDetail od = db.OrderDetails.SingleOrDefault(t => t.OrderId == OrdId & t.ProductId == ProId);
            db.OrderDetails.Remove(od);
            return result.GetResult(db);

        }
        public Result<List<OrderDetail>> GetListByOrdId(int Id)
        {
            return result.GetListResult(db.OrderDetails.Where(t => t.OrderId == Id).ToList());
        }
        public Result<OrderDetail> GetOrderDetailsByTwoId(int OID, int PID)
        {
            OrderDetail od = db.OrderDetails.SingleOrDefault(t => t.OrderId == OID & t.ProductId == PID);
            return result.GetT(od);
        }
    }
}
