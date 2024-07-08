using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Model.ViewModel
{
    public class RegisterModel
    {

        [MaxLength(40, ErrorMessage = "Not Add More Than 40 Characters")]
        [MinLength(2, ErrorMessage = "Please Add More Than 2 Characters ")]
        [Required(ErrorMessage = "Username is Required Field")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage ="Please Add Valid Email")]
        [MaxLength(40, ErrorMessage = "Not Add More Than 40 Characters")]
        [MinLength(2, ErrorMessage = "Please Add More Than 2 Characters ")]
        [Required(ErrorMessage ="Email is Required Field")]
        public string EmailId { get; set; }


        [MaxLength(40, ErrorMessage = "Not Add More Than 40 Characters")]
        [MinLength(2, ErrorMessage = "Please Add More Than 2 Characters ")]
        [Required(ErrorMessage = "Password is Required Field")]
        
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password is Required Field")]
        [Compare("Password", ErrorMessage ="Please Add the same As Password Fields")]
        public string ConfirmPassword { get; set; }
    }
}
