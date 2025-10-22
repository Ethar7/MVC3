using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities.Enums;

namespace GymSystemG2AL.Entities
{
    public class Trainer : GymUser
    {
        // Created at Exsisted in Base entity
        // I will use this column as JoinDate For Member +> Configurations 
        public Specialization Special { get; set; }
        
        #region 1:M RS Between SessionTrainer
        public ICollection<Session> TrainerSessions{ get; set; }
        #endregion
    }
}