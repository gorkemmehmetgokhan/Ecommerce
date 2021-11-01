using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entity;

namespace Ecommerce.Common
{
    public class Tools
    {
        public static DB_EcommerceEntities db = null;
       
        public static DB_EcommerceEntities GetConnection()
        {
            if (db == null)
            {
                db = new DB_EcommerceEntities();
            }

            return db;
        }
    }
}
