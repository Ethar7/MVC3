using System;
using System.ComponentModel.DataAnnotations;

namespace GymSystemBLL.ViewModels.SessionViewModel
{
    public class CreateSessionViewModel
    {
        [StringLength(100, ErrorMessage = "Session name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trainer is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "TrainerId must be a valid positive number.")]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a valid positive number.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 100.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
