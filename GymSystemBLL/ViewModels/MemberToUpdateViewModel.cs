using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemBLL.ViewModels;
using GymSystemG2AL.Entities;

namespace GymSystemBLL.ViewModels
{
    public class MemberToUpdateViewModel
    {
        public string Name { get; set; }
        public string? Photo { get; set; }
        
       
        [Required(ErrorMessage = "Email is required !")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 and 100 Chars !")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Phone is required !")]
        [Phone(ErrorMessage = "Invalid Phone Format")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(010|012|015|011)\d{8}$", ErrorMessage = "Invalid")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Required")]
        [Range(1, 9000, ErrorMessage = "Building Number Must Be Greater Than 0")]

        public int BuildingNumber { get; set; }
        

        [Required(ErrorMessage = "Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "street Must be between 2 and 30")]

        public string Street { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Can Contain Only letters !")]

        public string City { get; set; }

    }
}