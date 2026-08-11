using LinguaFlow.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linguaflow.DAL
{
    public class EnrollmentRepository
    {
        private readonly LinguaFlowContext _context;

        public EnrollmentRepository(LinguaFlowContext context)
        {
            _context = context;
        }

        public int CountEnrollments()
        {
            return _context.Enrollments.Count();
        }


        public List<Enrollment> GetAll()
        {
            return _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                 .Include(e => e.Tutor)
                .ToList();
        }

        public void CreateEnrollment(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
        }
        public Enrollment GetById(int id)
        {
            return _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Tutor)
                .Include(e => e.Course)
                .Include(e => e.Payment)
                .Include(e => e.TutorFee) 
                .FirstOrDefault(e => e.Id == id);
        }


        public void UpdateStatus(int id, string status)
        {
            var enrollment = _context.Enrollments.Find(id);

            if (enrollment != null)
            {
                enrollment.Status = status;
                _context.SaveChanges();
            }
        }

        public bool Exists(int studentId, int courseId, int tutorId)
        {
            return _context.Enrollments
                .Any(e => e.StudentId == studentId
                       && e.CourseId == courseId
                       && e.TutorId == tutorId
                       && e.Status != "Cancelled");
        }

    }
}
