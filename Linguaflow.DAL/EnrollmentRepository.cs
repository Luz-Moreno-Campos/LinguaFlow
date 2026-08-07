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

        public int CountTutors()
        {
            return _context.Enrollments.Count();
        }
    }

}
