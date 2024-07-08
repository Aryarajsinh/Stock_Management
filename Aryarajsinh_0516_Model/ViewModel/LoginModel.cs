using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Model.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is Required Field")]
        [MaxLength(40,ErrorMessage ="Not Add More Than 40 Characters")]
        [MinLength(2,ErrorMessage ="Please Add More Than 2 Characters ")]
        [EmailAddress(ErrorMessage ="Please Add Valid Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required Field")]
        [MaxLength(40, ErrorMessage = "Not Add More Than 40 Characters")]
        [MinLength(2, ErrorMessage = "Please Add More Than 2 Characters ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is Required Field")]
        public string Role { get; set; }
        
    }
}
