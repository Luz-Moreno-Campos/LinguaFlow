using LinguaFlow.BLL;
using LinguaFlowUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LinguaFlowUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LanguageService _languageService;

        public HomeController(ILogger<HomeController> logger, LanguageService languageService)
        {
            _logger = logger;
            _languageService = languageService;
        }

        public IActionResult Index()
        {
            var languages = _languageService.GetAll();
            return View(languages);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
