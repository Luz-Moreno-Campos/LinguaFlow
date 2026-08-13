using LinguaFlow.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linguaflow.DAL
{
    public class LanguageRepository
    {

        private readonly LinguaFlowContext _context;

        public LanguageRepository(LinguaFlowContext context)
        {
            _context = context;
        }

        public List<Language> GetAll()
        {
            return _context.Languages
                .ToList();
        }

        public Language GetById(int id)
        {
            return _context.Languages
                .Include(l => l.Tutors)
                .FirstOrDefault(l => l.Id == id);
        }
    }
}
