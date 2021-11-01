using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Common;
using Ecommerce.Entity;

namespace Ecommerce.Repository
{
    public class GenderRepository : DataRepository<Gender, int>
    {
        public static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<Gender> result = new ResultProcess<Gender>();
        public override Result<int> Delete(int id)
        {
            Gender deleted = db.Genders.SingleOrDefault(t => t.GenderId == id);
            db.Genders.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<Gender>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<Gender> GetObjById(int id)
        {
            return result.GetT(db.Genders.SingleOrDefault(t => t.GenderId == id));
        }

        public override Result<int> Insert(Gender item)
        {
            db.Genders.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Gender>> List()
        {
            return result.GetListResult(db.Genders.ToList());
        }

        public override Result<int> Update(Gender item)
        {
            Gender edited = db.Genders.SingleOrDefault(t => t.GenderId == item.GenderId);
            edited.GenderType = item.GenderType;           
            return result.GetResult(db);
        }
    }
}
