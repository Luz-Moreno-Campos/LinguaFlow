using LinguaFlow.BLL;
using LinguaFlow.Models;
using LinguaFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly CourseService _courseService;
        private readonly StudentService _studentService;
        private readonly TutorService _tutorService;

        public EnrollmentController(
            EnrollmentService enrollmentService,
            CourseService courseService,
            StudentService studentService,
            TutorService tutorService)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _studentService = studentService;
            _tutorService = tutorService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string student, string tutor, int? course, string status)
        {
            var enrollments = _enrollmentService.GetAll();
            var courses = _courseService.GetAll();

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
                    TutorName =  e.Tutor.FirstName + " " + e.Tutor.LastName,
                    CourseTitle = e.Course.Title,
                    EnrollmentDate = e.EnrollmentDate,
                    Status = e.Status
                }).ToList()
            };

            return View(vm);
        }


        [Authorize(Roles = "Student")]
        [HttpPost]
        public IActionResult Enroll(int tutorId, int courseId)
        {
            var userEmail = User.Identity?.Name;

            Console.WriteLine("USER EMAIL: " + userEmail);
            var student = _studentService.GetByEmail(userEmail);

            if (student == null)
                return Unauthorized();

            if (_enrollmentService.IsEnrolled(student.Id, courseId, tutorId))
            {
                TempData["EnrollmentError"] = "You are already enrolled in this course.";
                return Redirect(Request.Headers.Referer.ToString());
            }

            
            var course = _courseService.GetById(courseId);

           
            var enrollment = new Enrollment
            {
                StudentId = student.Id,
                TutorId = tutorId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                Status = "Pending",

             
                Payment = new Payment
                {
                    Amount = course.Price,
                    Status = "Pending",
                    Method = "Pending",
                    CreatedAt = DateTime.UtcNow
                },

          
                TutorFee = new TutorFee
                {
                    TutorId = tutorId,
                    FeeAmount = course.Price * 0.70m, 
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow
                }
            };

            _enrollmentService.CreateEnrollment(enrollment);

            return RedirectToAction("Success", new { tutorId, courseId });
        }


        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult Success(int tutorId, int courseId)
        {
            var tutor = _tutorService.GetById(tutorId);
            var course = _courseService.GetById(courseId);

            var vm = new EnrollmentSuccessViewModel
            {
                Tutor = tutor,
                Course = course
            };

            return View(vm);
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var e = _enrollmentService.GetById(id);

            if (e == null)
                return NotFound();

            var vm = new EnrollmentDetailsViewModel
            {
                Id = e.Id,
                StudentName = e.Student.FirstName + " " + e.Student.LastName,
                TutorName = e.Tutor.FirstName + " " + e.Tutor.LastName,
                CourseTitle = e.Course.Title,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status,
                Payment = e.Payment?.Amount ?? 0,
                TutorFee = e.TutorFee?.FeeAmount ?? 0
            };

            return View(vm);
        }

      
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            _enrollmentService.UpdateStatus(id, status);

            TempData["SuccessMessage"] = "Status updated successfully.";
            return RedirectToAction("Index");
        }
    }
}
