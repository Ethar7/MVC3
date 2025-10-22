using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;
using GymSystemG2AL.Repositories.Interfaces;

namespace GymSystemG2AL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymSystemDBContext _dbContext;
        public PlanRepository(GymSystemDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public IEnumerable<Plan> GetAll()
        {
            return _dbContext.Plans.ToList();
        }

        public Plan? GetById(int id)
        {
            return _dbContext.Plans.Find(id);
        }

        public int Update(Plan Plan)
        {
            _dbContext.Plans.Update(Plan);
            return _dbContext.SaveChanges();
        }
    }
}