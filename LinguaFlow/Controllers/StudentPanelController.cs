using LinguaFlow.BLL;
using LinguaFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentPanelController : Controller
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly StudentService _studentService;

        public StudentPanelController(EnrollmentService enrollmentService, StudentService studentService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
        }

      
        public IActionResult MyCourses()
        {
            var email = User.Identity?.Name;
            var student = _studentService.GetByEmail(email);

            var enrollments = _enrollmentService.GetByStudentId(student.Id);

            var vm = enrollments.Select(e => new StudentCourseViewModel
            {
                EnrollmentId = e.Id,
                CourseTitle = e.Course.Title,
                TutorName = e.Course.Tutors.First().FirstName + " " + e.Course.Tutors.First().LastName,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status
            }).ToList();

            return View(vm);
        }

    }



}
