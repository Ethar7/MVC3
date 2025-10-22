using GymSystemG2AL.Entities.Enums;

namespace GymSystemBLL.ViewModels
{
    public class TrainerToUpdateViewModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Specialization Special{ get; set; }
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
