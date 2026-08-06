using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    public class LanguagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
