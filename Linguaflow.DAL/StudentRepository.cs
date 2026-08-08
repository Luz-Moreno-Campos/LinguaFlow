using LinguaFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace Linguaflow.DAL
{
    public class StudentRepository
    {
        private readonly LinguaFlowContext _context;

        public StudentRepository(LinguaFlowContext context)
        {
            _context = context;
        }


        public int CountStudents()
        {
            return _context.Students.Count();
        }

        public Student GetById(int id)
        {
            return _context.Students
                           .FirstOrDefault(s => s.Id == id);
        }


        public List<Student> GetAll(string searchName, int? languageId)
        {

            // this loads students,  enrollments, courses,tutors and  language so we can filter by language, since language is reached through the tutor.

            var query = _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c.Tutors)
                            .ThenInclude(t => t.Language)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(s =>
                    s.FirstName.Contains(searchName) ||
                    s.LastName.Contains(searchName));
            }

            if (languageId.HasValue)
            {
                query = query.Where(s =>
                    s.Enrollments.Any(e =>
                        e.Course.Tutors.Any(t => t.LanguageId == languageId.Value)
                    ));
            }

            return query.ToList();
        }

        
        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        
        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

       
        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
