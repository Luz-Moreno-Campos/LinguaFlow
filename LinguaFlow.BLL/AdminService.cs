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
        private readonly TutorRepository _tutorRepo;
        private readonly StudentRepository _studentRepo;
        private readonly EnrollmentRepository _enrollmentRepo;

        public AdminService(
            PaymentRepository paymentRepo,
            TutorFeeRepository feeRepo,
            TutorRepository tutorRepo,
            StudentRepository studentRepo,
            EnrollmentRepository enrollmentRepo
            )
        {
            _paymentRepo = paymentRepo;
            _feeRepo = feeRepo;
            _tutorRepo = tutorRepo;
            _studentRepo = studentRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        public AdminHomeViewModel GetAdminHomeMetrics()
        {
            return new AdminHomeViewModel
            {
                TotalTutors = _tutorRepo.CountTutors(),
                TotalStudents = _studentRepo.CountStudents(),
                TotalEnrollments = _enrollmentRepo.CountEnrollments(),
                TotalPaymentsReceived = _paymentRepo.SumPaidPayments(),
                TotalPaymentsPending = _paymentRepo.SumPendingPayments(),
                TotalFeesPending = _feeRepo.SumPendingFees()
            };
        }
    }
}

