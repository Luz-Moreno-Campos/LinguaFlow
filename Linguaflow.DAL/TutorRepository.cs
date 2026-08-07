using LinguaFlow.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linguaflow.DAL
{
    public class TutorRepository
    {
        private readonly LinguaFlowContext _context;

        public TutorRepository(LinguaFlowContext context)
        {
            _context = context;
        }

        
        public int CountTutors()
        {
            return _context.Tutors.Count();
        }

        
        public List<Tutor> GetAll(string searchName, int? languageId)
        {
            var query = _context.Tutors
                .Include(t => t.Language)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(t =>
                    t.FirstName.Contains(searchName) ||
                    t.LastName.Contains(searchName));
            }

            if (languageId.HasValue)
            {
                query = query.Where(t => t.LanguageId == languageId.Value);
            }

            return query.ToList();
        }

        
        public void Add(Tutor tutor)
        {
            _context.Tutors.Add(tutor);
            _context.SaveChanges();
        }

        
        public void Update(Tutor tutor)
        {
            _context.Tutors.Update(tutor);
            _context.SaveChanges();
        }

        
        public void Delete(int id)
        {
            var tutor = _context.Tutors.Find(id);
            if (tutor == null) return;

            _context.Tutors.Remove(tutor);
            _context.SaveChanges();
        }
    }
}



