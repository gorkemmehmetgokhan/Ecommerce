using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entity;
using Ecommerce.Common;

namespace Ecommerce.Repository
{
    public class PaymentRepository
    {
        private static DB_EcommerceEntities db = Tools.GetConnection();
        public static List<Payment> List()
        {
            return db.Payments.ToList();
        }
    }
}
