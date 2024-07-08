using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Model.ViewModel
{
    public class FilterDataOrderModel
    {
        public DateTime StartDate { get; set; }

        public DateTime Enddate { get; set; }

        public string ProductName { get; set; }
        public string StartDateFormatted => StartDate.ToString("yyyy-MM-dd");
        public string EndDateFormatted => Enddate.ToString("yyyy-MM-dd");
    }
    public class FilterDataOrderModels
    {
        public DateTime StartDate { get; set; }

        public DateTime Enddate { get; set; }

        public string StartDateFormatted => StartDate.ToString("yyyy-MM-dd");
        public string EndDateFormatted => Enddate.ToString("yyyy-MM-dd");

        public int UserId { get; set; }
    }
}
