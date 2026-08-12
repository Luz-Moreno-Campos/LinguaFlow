using LinguaFlow.BLL;
using LinguaFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinguaFlowUI.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentPanelController : Controller
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly StudentService _studentService;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentPanelController(
            EnrollmentService enrollmentService,
            StudentService studentService,
            UserManager<IdentityUser> userManager)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _userManager = userManager;
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
                TutorName = e.Tutor.FirstName + " " + e.Tutor.LastName,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status
            }).ToList();

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ConfirmEnrollment(int enrollmentId, bool accept)
        {
            var enrollment = _enrollmentService.GetById(enrollmentId);

            if (enrollment == null)
                return NotFound();

            string newStatus = accept ? "Confirmed" : "Cancelled";

            _enrollmentService.UpdateStatus(enrollmentId, newStatus);

            return RedirectToAction("MyCourses");
        }

        [HttpGet]
        public IActionResult MyProfile()
        {
            var email = User.Identity?.Name;
            var student = _studentService.GetByEmail(email);

            if (student == null)
                return NotFound();

            var vm = new StudentProfileViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            };

            return View(vm);
        }

     
        [HttpGet]
        public IActionResult EditProfile()
        {
            var email = User.Identity?.Name;
            var student = _studentService.GetByEmail(email);

            if (student == null)
                return NotFound();

            var vm = new StudentEditProfileViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            };

            return View(vm);
        }

      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(StudentEditProfileViewModel model)
        {
            if (!string.IsNullOrEmpty(model.NewPassword) && string.IsNullOrEmpty(model.CurrentPassword))
            {
                ModelState.AddModelError("CurrentPassword", "Current password is required to set a new password.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var email = User.Identity?.Name;
            var student = _studentService.GetByEmail(email);

            if (student == null)
                return NotFound();

            // Updating password
            if (!string.IsNullOrEmpty(model.CurrentPassword) && !string.IsNullOrEmpty(model.NewPassword))
            {
                var identityUser = await _userManager.FindByEmailAsync(email);
                if (identityUser != null)
                {
                    var result = await _userManager.ChangePasswordAsync(identityUser, model.CurrentPassword, model.NewPassword);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }

                student.Password = model.NewPassword;
            }

  
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;

            _studentService.UpdateStudent(student);

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction(nameof(MyProfile));
        }
    }
}