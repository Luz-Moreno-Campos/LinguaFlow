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
                    .ThenInclude(c => c.Tutors)
                .ToList();
        }

    
        public Enrollment GetById(int id)
        {
            return _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Tutors)
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
    }

}
