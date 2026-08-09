using LinguaFlow.BLL;
using LinguaFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EnrollmentController : Controller
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly CourseService _courseService;

        public EnrollmentController(EnrollmentService enrollmentService, CourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
        }

        public IActionResult Index(string student, string tutor, int? course, string status)
        {
            var enrollments = _enrollmentService.GetAll();
            var courses = _courseService.GetAll(); // carga cursos

          
            if (!string.IsNullOrWhiteSpace(student))
                enrollments = enrollments
                    .Where(e => (e.Student.FirstName + " " + e.Student.LastName)
                    .Contains(student))
                    .ToList();

            if (!string.IsNullOrWhiteSpace(tutor))
                enrollments = enrollments
                    .Where(e => e.Course.Tutors
                        .Any(t => (t.FirstName + " " + t.LastName).Contains(tutor)))
                    .ToList();

            if (course.HasValue)
                enrollments = enrollments
                    .Where(e => e.CourseId == course.Value)
                    .ToList();

            if (!string.IsNullOrWhiteSpace(status))
                enrollments = enrollments
                    .Where(e => e.Status == status)
                    .ToList();

            var vm = new EnrollmentIndexViewModel
            {
                StudentSearch = student,
                TutorSearch = tutor,
                CourseSearch = course,
                StatusSearch = status,

                Courses = courses, 

                Enrollments = enrollments.Select(e => new EnrollmentListViewModel
                {
                    Id = e.Id,
                    StudentName = e.Student.FirstName + " " + e.Student.LastName,
                    TutorName = e.Course.Tutors.First().FirstName + " " + e.Course.Tutors.First().LastName,
                    CourseTitle = e.Course.Title,
                    EnrollmentDate = e.EnrollmentDate,
                    Status = e.Status
                }).ToList()
            };

            return View(vm);
        }


        public IActionResult Details(int id)
        {
            var e = _enrollmentService.GetById(id);

            if (e == null)
                return NotFound();

            var vm = new EnrollmentDetailsViewModel
            {
                Id = e.Id,
                StudentName = e.Student.FirstName + " " + e.Student.LastName,
                TutorName = e.Course.Tutors.First().FirstName + " " + e.Course.Tutors.First().LastName,
                CourseTitle = e.Course.Title,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status,
                Payment = e.Payment?.Amount ?? 0, 
                TutorFee = e.TutorFee.FeeAmount 
            };

            return View(vm);
        }



        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            _enrollmentService.UpdateStatus(id, status);

            TempData["SuccessMessage"] = "Status updated successfully.";
            return RedirectToAction("Index");
        }
    }
}
