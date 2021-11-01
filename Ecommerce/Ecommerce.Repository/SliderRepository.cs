using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entity;
using Ecommerce.Common;

namespace Ecommerce.Repository
{
    public class SliderRepository : DataRepository<Slider, int>
    {
        public static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<Slider> result = new ResultProcess<Slider>();
        public override Result<int> Delete(int id)
        {
            Slider deleted = db.Sliders.SingleOrDefault(t => t.SliderId == id);
            db.Sliders.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<Slider>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Sliders.OrderByDescending(t => t.SliderId).Take(Quantity).ToList());
        }

        public override Result<Slider> GetObjById(int id)
        {
            return result.GetT(db.Sliders.SingleOrDefault(t => t.SliderId == id));
        }

        public override Result<int> Insert(Slider item)
        {
            db.Sliders.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Slider>> List()
        {
            return result.GetListResult(db.Sliders.ToList());
        }

        public override Result<int> Update(Slider item)
        {
            Slider edited = db.Sliders.SingleOrDefault(t => t.SliderId == item.SliderId);         
            edited.CategoryId = item.CategoryId;    
            edited.SliderHeader = item.SliderHeader;
            edited.Description = item.Description;
            edited.Photo = item.Photo;
            return result.GetResult(db);
        }
    }
}
