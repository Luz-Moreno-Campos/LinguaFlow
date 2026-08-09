using LinguaFlow.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linguaflow.DAL
{
    public class CourseRepository
    {

        private readonly LinguaFlowContext _context;

        public CourseRepository(LinguaFlowContext context)
        {
            _context = context;
        }

        public List<Course> GetAll()
        {
            return _context.Courses
                .Include(c => c.Tutors)        
                .Include(c => c.Enrollments)   
                .ToList();
        }

        public Course GetById(int id)
        {
            return _context.Courses
                .Include(c => c.Tutors)
                .Include(c => c.Enrollments)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
