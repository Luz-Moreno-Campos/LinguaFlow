using Microsoft.AspNetCore.Mvc;

namespace LinguaFlow.MVC.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
