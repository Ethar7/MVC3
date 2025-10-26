using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.PlanViewModel
{
    public class UpdatePlanViewModel
    {
        public string PlanName { get; set; } = null!;
        [Required(ErrorMessage = "required")]
        [StringLength(200, MinimumLength =5, ErrorMessage ="You Must Enter Description in Range 5 : 200 Chars !")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "required")]
        [Range(1 ,365 , ErrorMessage = "Days Must be Greater Than 1 and less than 365")]
        public int DurationDays { get; set; }
        [Required(ErrorMessage = "required")]
        [Range(1 ,10000 , ErrorMessage = "Price Must be Greater Than 1")]

        public decimal Price{ get; set; }
    }
}