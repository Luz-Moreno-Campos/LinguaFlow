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
    }
}
