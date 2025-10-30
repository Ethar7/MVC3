using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;

namespace GymSystemG2AL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenaricRepository<Session>
    {
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();

        int GetCountOfBookedSlots(int sessionId);

        Session? GetSessionWithTrainerAndCategory(int sessionId);
    }
}