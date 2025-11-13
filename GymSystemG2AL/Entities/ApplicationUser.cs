using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace GymSystemG2AL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}