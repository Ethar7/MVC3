
using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace GymSysteG2PL.Controllers
{

    public class HomeController : Controller
    {

        private readonly IAnalyticService _analyticService;
        public HomeController(IAnalyticService analyticService)
        {
            _analyticService = analyticService;
        }
        public IActionResult Index()
        {
            var Data = _analyticService.GetAnalyticsViewData();
            return View(Data);
        }
    }
}