using Microsoft.AspNetCore.Mvc;

namespace LinguaFlow.MVC.Controllers
{
    public class TutorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
