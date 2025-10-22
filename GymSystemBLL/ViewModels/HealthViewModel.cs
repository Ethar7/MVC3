using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;
using GymSystemG2AL.Entities.Enums;
using GymSystemG2AL.Repositories.Interfaces;

namespace GymSystemBLL.ViewModels
{
    public class HealthViewModel
    {
        [Required(ErrorMessage = "Required !")]
        [Range(1, 300, ErrorMessage = "height must be greater than 0")]
        public decimal Height { get; set; }
        [Required(ErrorMessage = "Required !")]
        [Range(1, 500, ErrorMessage = "Weight must be greater than 0 and less than 500")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Required !")]
        [StringLength(3, ErrorMessage = "Blood Type Must Be 3 Chars or Less")]
        public string BloodType { get; set; }
        
        public string? Note{ get; set; }
    }
}