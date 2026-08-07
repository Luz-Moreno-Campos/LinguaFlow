using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linguaflow.DAL
{
    public class StudentRepository
    {
        private readonly LinguaFlowContext _context;

        public StudentRepository(LinguaFlowContext context)
        {
            _context = context;
        }

        public int CountTutors()
        {
            return _context.Students.Count();
        }
    }

}
