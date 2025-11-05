
using Microsoft.AspNetCore.Mvc;


namespace GymSysteG2PL.Controllers
{
    

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
}