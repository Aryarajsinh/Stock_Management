using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Model.ViewModel
{
    public class CartModel
    {
        public int CartId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        public Nullable<int> supplierId { get; set; }
    }
}
