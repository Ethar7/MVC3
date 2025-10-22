using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymSystemG2AL.Entities
{
    public class Member : GymUser
    {
        // Created at Exsisted in Base entity
        // I will use this column as JoinDate For Member +> Configurations 


        public string? Photo { get; set; }

        #region 1 : 1 RS Between Member HealthRecord
        // Nav Property

        public HealthRecord HealthRecord { get; set; }// One

        
        #endregion
    
        #region M : M Between MemberPlan

        public ICollection<MemberShip> MemberShips { get; set; }
        #endregion
        #region M : M Between MemberSessions

        public ICollection<MemberSession> MemberSessions{ get; set; }
        #endregion
    }
}