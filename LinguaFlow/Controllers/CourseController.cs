using Microsoft.AspNetCore.Mvc;

namespace LinguaFlow.MVC.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
