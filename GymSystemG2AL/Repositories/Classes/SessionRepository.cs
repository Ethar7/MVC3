using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;
using GymSystemG2AL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymSystemG2AL.Repositories.Classes
{
    public class SessionRepository : GenaricRepository<Session>, ISessionRepository
    {
        private readonly GymSystemDBContext _dbContext;

        public SessionRepository(GymSystemDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return _dbContext.Sessions.Include(X => X.SessionTrainer)
                                      .Include(X => X.SessionCategory)
                                      .ToList();
        }

        public int GetCountOfBookedSlots(int sessionId)
        {

            return _dbContext.MemberSessions.Count(X => X.SessionId == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions.Include(X => X.SessionTrainer)
                                      .Include(X => X.SessionCategory)
                                      .FirstOrDefault(X => X.Id == sessionId);
        }
    }
}
