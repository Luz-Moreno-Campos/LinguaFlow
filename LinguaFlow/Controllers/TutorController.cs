using LinguaFlow.BLL;
using LinguaFlow.Models;
using LinguaFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TutorController : Controller
    {
        private readonly TutorService _service;

        public TutorController(TutorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(string searchName, int? searchLanguageId)
        {
            var tutors = _service.GetTutors(searchName, searchLanguageId);

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

        [HttpGet]
        public IActionResult Details(int id)
        {
            var tutor = _service.GetTutors(null, null)
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


        [HttpGet]
        public IActionResult Create()
        {
            var vm = new TutorCreateViewModel
            {
                Languages = _service.GetTutors(null, null)
                                    .Select(t => t.Language)
                                    .Distinct()
                                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(TutorCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = _service.GetTutors(null, null)
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

            _service.CreateTutor(tutor);

            TempData["SuccessMessage"] = "Tutor created successfully.";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var tutor = _service.GetTutors(null, null)
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
                Languages = _service.GetTutors(null, null)
                                    .Select(t => t.Language)
                                    .Distinct()
                                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(TutorEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = _service.GetTutors(null, null)
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

            _service.UpdateTutor(tutor);

            TempData["SuccessMessage"] = "Tutor updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var tutor = _service.GetTutors(null, null)
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

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.DeleteTutor(id);

            TempData["SuccessMessage"] = "Tutor deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
