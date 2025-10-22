using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;
namespace GymSystemG2AL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        Plan? GetById(int id);

        IEnumerable<Plan> GetAll();

        int Update(Plan Plan);  
    }
}