using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemBLL.ViewModels;
using GymSystemG2AL.Entities;

namespace GymSystemBLL.Services.Interfaces
{
    
    public interface IAnalyticService
    {
        // Get Analytics Data
        AnalyticsViewModel GetAnalyticsViewData();
    }
}