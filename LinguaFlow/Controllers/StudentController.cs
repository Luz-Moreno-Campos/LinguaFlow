using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
