using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linguaflow.DAL
{
    public class TutorFeeRepository
    {
        private readonly LinguaFlowContext _context;

        public TutorFeeRepository(LinguaFlowContext context)
        {
            _context = context;
        }

        public decimal SumPendingFees()
        {
            return _context.TutorFees
                .Where(f => f.Status == "Pending")
                .Sum(f => f.FeeAmount);
        }
    }

}
