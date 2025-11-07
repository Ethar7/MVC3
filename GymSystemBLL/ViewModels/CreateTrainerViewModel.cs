using System;
using GymSystemG2AL.Entities.Enums;

namespace GymSystemBLL.ViewModels
{
    public class CreateTrainerViewModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Gender Gender { get; set; }          
        public Specialization Special { get; set; } 
        public DateOnly DateOfBirth { get; set; }

        public DateTime HireDate { get; set; } = DateTime.Now;

        
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
