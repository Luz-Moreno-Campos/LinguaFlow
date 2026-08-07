using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TutorFeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
