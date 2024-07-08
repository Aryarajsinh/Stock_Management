using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Model.ViewModel
{
    public class OrderTableViewModel
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public int TotalPrice { get; set; }

        public string EmailId { get; set; }

        public decimal price { get; set; }

        public DateTime date { get; set; }

        public string Formatteddate => date.ToString("dd-MM-yyyy");

    }
}
