using Microsoft.AspNetCore.Mvc;

namespace LinguaFlow.MVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
