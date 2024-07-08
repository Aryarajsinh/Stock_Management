using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Model.ViewModel
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [MaxLength(40, ErrorMessage = "Not Add More than 40 Characters")]
        [MinLength(3, ErrorMessage = "Please Add More than 3 Characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Description is Required")]
        [MaxLength(40, ErrorMessage = "Not Add More than 40 Characters")]
        [MinLength(3, ErrorMessage = "Please Select Type")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Product Type is Required")]
        [MaxLength(40, ErrorMessage = "Not Add More than 40 Characters")]
        [MinLength(3, ErrorMessage = "Please Add More than 3 Characters")]
        public string ProductType { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        [CheckingNagativeValue(ErrorMessage = "Please Add The Greater Than 0 Value")]
        [CheckingNagativeValueDecimalValues(ErrorMessage = "Please Add The Data In Integer Value")]

        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [CheckingNagativeValue(ErrorMessage = "Please Add The Greater Than 0 Value")]
        [CheckingNagativeValueDecimalValues(ErrorMessage ="Please Add The Data In Integer Value")]

        public decimal? price { get; set; }

    }
    public class CheckingNagativeValue : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                int Quantity = Convert.ToInt32(value);
                if (Quantity > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }
    }
    public class CheckingNagativeValueDecimalValues : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string a = Convert.ToString(value);
                if (a.Contains('.'))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }
    }
}

