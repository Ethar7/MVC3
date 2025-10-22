using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities.Enums;

namespace GymSystemG2AL.Entities
{
    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; }

        public string? Note { get; set; }

        // LastUpdate => Use Column Where Existed in BaseEntity [UpdatedAt]
    }

}