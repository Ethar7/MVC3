using System;

namespace GymSystemBLL.ViewModels
{
    public class TrainerViewModel
    {
        public int Id { get; set; }              
        public string Name { get; set; } = null!; 
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Special { get; set; } = null!; 
        public string HireDate { get; set; } = null!;       
        public string? Photo { get; set; }                  
    }
}
