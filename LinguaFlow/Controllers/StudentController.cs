using LinguaFlow.BLL;
using LinguaFlow.Models;
using LinguaFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index(string searchName, int? searchLanguageId)
        {
            var students = _studentService.GetStudents(searchName, searchLanguageId);

            var vm = new StudentIndexViewModel
            {
                SearchName = searchName,
                SearchLanguageId = searchLanguageId,
                Languages = students
                    .SelectMany(s => s.Enrollments
                        .Where(e => e.Tutor != null && e.Tutor.Language != null)
                        .Select(e => e.Tutor.Language))
                    .Distinct()
                    .ToList(),

                Students = students.Select(s => new StudentListViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    LanguageNames = s.Enrollments
                        .Where(e => e.Tutor != null && e.Tutor.Language != null)
                        .Select(e => e.Tutor.Language.Name)
                        .Distinct()
                        .ToList()
                }).ToList()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var student = _studentService.GetStudents(null, null)
                                  .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            var vm = new StudentDetailViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,

               
                Languages = student.Enrollments
                    .Where(e => e.Tutor != null && e.Tutor.Language != null)
                    .GroupBy(e => e.Tutor.Language.Name)
                    .Select(g => new StudentLanguageInfo
                    {
                        LanguageName = g.Key,

                        Courses = g.Select(e => e.Course.Title)
                                   .Distinct()
                                   .ToList(),

                        Tutors = g.Select(e => $"{e.Tutor.FirstName} {e.Tutor.LastName}")
                                  .Distinct()
                                  .ToList()
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new StudentCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var student = new Student
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                Password = vm.Password
            };

            _studentService.CreateStudent(student);

            TempData["SuccessMessage"] = "Student created successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _studentService.GetStudents(null, null)
                                  .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            var vm = new StudentEditViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Password = student.Password
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var student = _studentService.GetStudentById(vm.Id);

            if (student == null)
                return NotFound();

            student.FirstName = vm.FirstName;
            student.LastName = vm.LastName;
            student.Email = vm.Email;

            _studentService.UpdateStudent(student);

            TempData["SuccessMessage"] = "Student updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _studentService.GetStudents(null, null)
                                  .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            var vm = new StudentDetailViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,

               
                Languages = student.Enrollments
                    .Where(e => e.Tutor != null && e.Tutor.Language != null)
                    .GroupBy(e => e.Tutor.Language.Name)
                    .Select(g => new StudentLanguageInfo
                    {
                        LanguageName = g.Key,
                        Courses = g.Select(e => e.Course.Title).Distinct().ToList(),
                        Tutors = g.Select(e => $"{e.Tutor.FirstName} {e.Tutor.LastName}")
                                  .Distinct()
                                  .ToList()
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentService.DeleteStudent(id);

            TempData["SuccessMessage"] = "Student deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}