//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aryarajsinh_0516_Model.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderTable
    {
        public int OrderId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}
