using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemBLL.ViewModels.SessionViewModel;
using GymSystemG2AL.Entities;

namespace GymSystemBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int sessionId);
    }
}