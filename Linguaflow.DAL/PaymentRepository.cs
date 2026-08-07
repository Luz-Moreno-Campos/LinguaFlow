using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linguaflow.DAL
{
    public class PaymentRepository
    {
        private readonly LinguaFlowContext _context;

        public PaymentRepository(LinguaFlowContext context)
        {
            _context = context;
        }

     
        public decimal SumPaidPayments()
        {
            return _context.Payments
                .Where(p => p.Status == "Paid")
                .Sum(p => p.Amount);
        }

        public decimal SumPendingPayments()
        {
            return _context.Payments
                .Where(p => p.Status == "Pending")
                .Sum(p => p.Amount);
        }
    }
}
