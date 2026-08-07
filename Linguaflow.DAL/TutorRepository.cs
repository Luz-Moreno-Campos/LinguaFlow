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
    }

}
