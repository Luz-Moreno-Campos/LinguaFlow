using Linguaflow.DAL;
using LinguaFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinguaFlow.Models;

namespace LinguaFlowUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TutorController : Controller
    {
        private readonly LinguaFlowContext _context;

        public TutorController(LinguaFlowContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchName, int? searchLanguageId)
        {
            var vm = new TutorIndexViewModel
            {
                SearchName = null,
                SearchLanguageId = searchLanguageId,
                Languages = _context.Languages.ToList()
            };

            var query = _context.Tutors
                .Include(t => t.Language)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
                query = query.Where(t => t.FirstName.Contains(searchName)
                                      || t.LastName.Contains(searchName));

            if (searchLanguageId.HasValue)
                query = query.Where(t => t.LanguageId == searchLanguageId.Value);

            vm.Tutors = query
                .Select(t => new TutorListItemViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    LanguageName = t.Language.Name,
                    Availability = t.Availability
                })
                .ToList();

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var tutor = _context.Tutors
                .Include(t => t.Language)
                .FirstOrDefault(t => t.Id == id);

            if (tutor == null)
                return NotFound();

            var vm = new TutorDetailViewModel
            {
                Id = tutor.Id,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                Bio = tutor.Bio,
                LanguageName = tutor.Language.Name,
                Availability = tutor.Availability
            };

            return View(vm);
        }

        public IActionResult Create()
        {
            var vm = new TutorCreateViewModel
            {
                Languages = _context.Languages.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(TutorCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = _context.Languages.ToList();
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

            _context.Tutors.Add(tutor);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Tutor created successfully.";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var tutor = _context.Tutors.Find(id);

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
                Languages = _context.Languages.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(TutorEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = _context.Languages.ToList();
                return View(vm);
            }

            var tutor = _context.Tutors.Find(vm.Id);

            if (tutor == null)
                return NotFound();

            tutor.FirstName = vm.FirstName;
            tutor.LastName = vm.LastName;
            tutor.Bio = vm.Bio;
            tutor.LanguageId = vm.LanguageId;
            tutor.Availability = vm.Availability;

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Tutor updated successfully.";

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var tutor = _context.Tutors
                .Include(t => t.Language)
                .FirstOrDefault(t => t.Id == id);

            if (tutor == null)
                return NotFound();

            var vm = new TutorDetailViewModel
            {
                Id = tutor.Id,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                Bio = tutor.Bio,
                LanguageName = tutor.Language.Name,
                Availability = tutor.Availability
            };

            return View(vm); 
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var tutor = _context.Tutors.Find(id);

            if (tutor == null)
                return NotFound();

            _context.Tutors.Remove(tutor);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Tutor deleted successfully.";

            return RedirectToAction("Index");
        }

    }
}
