using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities.Enums;

namespace GymSystemG2AL.Entities
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        #region 1:M RS Between SessionCategory
        // FK
        public int CategoryId { get; set; }
        public Category SessionCategory { get; set; } // one
        #endregion
    
        #region 1:M RS Between SessionTrainer
        //Fk

        public int TrainerId { get; set; }
        public Trainer SessionTrainer { get; set; }
        #endregion

        #region M : M Between MemberSessions

        public ICollection<MemberSession> SessionMembers{ get; set; }
        #endregion
    }
}