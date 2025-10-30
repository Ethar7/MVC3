using System;
using System.ComponentModel.DataAnnotations;

namespace GymSystemBLL.ViewModels.SessionViewModel
{
    public class UpdateSessionViewModel
    {

        [Required(ErrorMessage = "Trainer is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "TrainerId must be a valid positive number.")]
        public int TrainerId { get; set; }


        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
