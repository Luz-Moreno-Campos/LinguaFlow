using Linguaflow.DAL;
using LinguaFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinguaFlowUI.Controllers
{
    [Authorize(Roles = "Tutor")]
    public class TutorController : Controller
    {
        private readonly LinguaFlowContext _context;

        public TutorController(LinguaFlowContext context)
        {
            _context = context;
        }

      
        public async Task<IActionResult> Index(string searchName, int? searchLanguageId)
        {
            var vm = new TutorIndexViewModel
            {
                SearchName = searchName,
                SearchLanguageId = searchLanguageId,
                Languages = await _context.Languages.ToListAsync()
            };

            var query = _context.Tutors
                .Include(t => t.Language)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
                query = query.Where(t => t.FirstName.Contains(searchName)
                                      || t.LastName.Contains(searchName));

            if (searchLanguageId.HasValue)
                query = query.Where(t => t.LanguageId == searchLanguageId.Value);

            vm.Tutors = await query
                .Select(t => new TutorListItemViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    LanguageName = t.Language.Name,
                    Availability = t.Availability
                })
                .ToListAsync();

            return View(vm);
        }

     
        public async Task<IActionResult> Details(int id)
        {
            var tutor = await _context.Tutors
                .Include(t => t.Language)
                .FirstOrDefaultAsync(t => t.Id == id);

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

   
        public async Task<IActionResult> Delete(int id)
        {
            var tutor = await _context.Tutors.FindAsync(id);

            if (tutor == null)
                return NotFound();

            _context.Tutors.Remove(tutor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}


