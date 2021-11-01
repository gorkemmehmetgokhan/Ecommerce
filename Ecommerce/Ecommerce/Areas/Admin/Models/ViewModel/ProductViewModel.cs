using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Entity;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Models.ViewModel
{
    public class ProductViewModel
    {
        public Product Product { get; set; }      
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
        public IEnumerable<SelectListItem> SuppList { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
    }
}