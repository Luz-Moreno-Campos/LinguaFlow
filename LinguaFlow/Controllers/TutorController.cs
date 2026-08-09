using LinguaFlow.BLL;
using LinguaFlow.Models;
using LinguaFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    public class TutorController : Controller
    {
        private readonly TutorService _tutorService;
        private readonly LanguageService _languageService;

        public TutorController(TutorService tutorService, LanguageService languageService)
        {
            _tutorService = tutorService;
            _languageService = languageService;
        }

       
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index(string searchName, int? searchLanguageId)
        {
            var tutors = _tutorService.GetTutors(searchName, searchLanguageId);

            var vm = new TutorIndexViewModel
            {
                SearchName = searchName,
                SearchLanguageId = searchLanguageId,
                Languages = tutors
                    .Select(t => t.Language)
                    .Distinct()
                    .ToList(),
                Tutors = tutors.Select(t => new TutorListViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    LanguageName = t.Language?.Name,
                    Availability = t.Availability
                }).ToList()
            };

            return View(vm);
        }

       
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var tutor = _tutorService.GetTutors(null, null)
                                .FirstOrDefault(t => t.Id == id);

            if (tutor == null)
                return NotFound();

            var vm = new TutorDetailViewModel
            {
                Id = tutor.Id,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                Bio = tutor.Bio,
                LanguageName = tutor.Language?.Name,
                Availability = tutor.Availability
            };

            return View(vm);
        }

      
        [AllowAnonymous]
        public IActionResult ByLanguage(int id)
        {
            var language = _languageService.GetById(id);
            if (language == null)
                return NotFound();

            var tutors = _tutorService.GetTutorsByLanguage(id);

            var vm = new TutorsByLanguageViewModel
            {
                Language = language,
                Tutors = tutors
            };

            return View(vm);
        }

        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new TutorCreateViewModel
            {
                Languages = _tutorService.GetTutors(null, null)
                                    .Select(t => t.Language)
                                    .Distinct()
                                    .ToList()
            };

            return View(vm);
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(TutorCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = _tutorService.GetTutors(null, null)
                                       .Select(t => t.Language)
                                       .Distinct()
                                       .ToList();
                return View(vm);
            }

            var tutor = new Tutor
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Bio = vm.Bio,
                LanguageId = vm.LanguageId,
                Availability = vm.Availability
            };

            _tutorService.CreateTutor(tutor);

            TempData["SuccessMessage"] = "Tutor created successfully.";
            return RedirectToAction("Index");
        }

        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var tutor = _tutorService.GetTutors(null, null)
                                .FirstOrDefault(t => t.Id == id);

            if (tutor == null)
                return NotFound();

            var vm = new TutorEditViewModel
            {
                Id = tutor.Id,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                Bio = tutor.Bio,
                LanguageId = tutor.LanguageId,
                Availability = tutor.Availability,
                Languages = _tutorService.GetTutors(null, null)
                                    .Select(t => t.Language)
                                    .Distinct()
                                    .ToList()
            };

            return View(vm);
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(TutorEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = _tutorService.GetTutors(null, null)
                                       .Select(t => t.Language)
                                       .Distinct()
                                       .ToList();
                return View(vm);
            }

            var tutor = new Tutor
            {
                Id = vm.Id,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Bio = vm.Bio,
                LanguageId = vm.LanguageId,
                Availability = vm.Availability
            };

            _tutorService.UpdateTutor(tutor);

            TempData["SuccessMessage"] = "Tutor updated successfully.";
            return RedirectToAction("Index");
        }

        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var tutor = _tutorService.GetTutors(null, null)
                                .FirstOrDefault(t => t.Id == id);

            if (tutor == null)
                return NotFound();

            var vm = new TutorDetailViewModel
            {
                Id = tutor.Id,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                Bio = tutor.Bio,
                LanguageName = tutor.Language?.Name,
                Availability = tutor.Availability
            };

            return View(vm);
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _tutorService.DeleteTutor(id);

            TempData["SuccessMessage"] = "Tutor deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
