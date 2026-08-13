using Linguaflow.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.BLL
{
    public class PaymentService
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentService(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public decimal GetPaidPaymentsTotal()
        {
            return _paymentRepository.SumPaidPayments();
        }

        public decimal GetPendingPaymentsTotal()
        {
            return _paymentRepository.SumPendingPayments();
        }
    }
}
