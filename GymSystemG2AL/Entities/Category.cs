using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities.Enums;

namespace GymSystemG2AL.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = null!;
        
        #region 1:M RS Between SessionCategory

        public ICollection<Session> Sessions { get; set; }
        #endregion
    }
}