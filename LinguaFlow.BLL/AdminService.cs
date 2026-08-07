using Linguaflow.DAL;
using LinguaFlow.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.BLL
{
    public class AdminService
    {
        private readonly PaymentRepository _paymentRepo;
        private readonly TutorFeeRepository _feeRepo;

        public AdminService(
            PaymentRepository paymentRepo,
            TutorFeeRepository feeRepo)
        {
            _paymentRepo = paymentRepo;
            _feeRepo = feeRepo;
        }

        public AdminHomeViewModel GetAdminHomeMetrics()
        {
            return new AdminHomeViewModel
            {
                TotalPaymentsReceived = _paymentRepo.SumPaidPayments(),
                TotalPaymentsPending = _paymentRepo.SumPendingPayments(),
                TotalFeesPending = _feeRepo.SumPendingFees()
            };
        }
    }
}

