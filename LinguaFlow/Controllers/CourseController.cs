using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
