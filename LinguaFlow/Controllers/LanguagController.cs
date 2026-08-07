using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LanguagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
