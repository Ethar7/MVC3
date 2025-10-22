using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymSystemG2AL.Entities
{
    public class MemberShip : BaseEntity
    {
        public int MemberId { get; set; }

        public Member Member{ get; set; }
        public int PlanId { get; set; }

        public Plan Plan { get; set; }

        // startDate == CreatedAt => BaseEntity

        public DateTime EndDate { get; set; }

        public string Status
        {
            get
            {
                if (EndDate <= DateTime.Now)
                {
                    return "Expired !";
                }
                else
                    return "Active";
            }
        }
    }
}