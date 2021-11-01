using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Common;
using Ecommerce.Entity;

namespace Ecommerce.Repository
{
    public class UserRepository : DataRepository<User, int>
    {
        private static DB_EcommerceEntities db = Tools.GetConnection();
        ResultProcess<User> result = new ResultProcess<User>();
        public override Result<int> Delete(int id)
        {
            User u = db.Users.SingleOrDefault(t => t.UserId == id);
            db.Users.Remove(u);
            return result.GetResult(db);
        }

        public override Result<List<User>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Users.OrderByDescending(t => t.UserId).Take(Quantity).ToList());
        }

        public override Result<User> GetObjById(int id)
        {
            return result.GetT(db.Users.SingleOrDefault(t => t.UserId == id));
        }

        public override Result<int> Insert(User item)
        {
            db.Users.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<User>> List()
        {
            return result.GetListResult(db.Users.ToList());
        }

        public override Result<int> Update(User item)
        {
            User u = db.Users.SingleOrDefault(t => t.UserId == item.UserId);
            u.Fullname = item.Fullname;
            u.Email = item.Email;
            u.Password = item.Password;
            u.UserRoleId = item.UserRoleId;
            u.Photo = item.Photo;
            u.Username = item.Username;
            u.Gender = item.Gender;
            return result.GetResult(db);
        }
    }
}
