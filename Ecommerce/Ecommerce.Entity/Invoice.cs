//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> AddressId { get; set; }
        public Nullable<int> PaymentTypeId { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Payment Payment { get; set; }
    }
}