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
        private readonly PaymentService _paymentService;
        private readonly TutorFeeService _feeService;
        private readonly TutorRepository _tutorRepo;
        private readonly StudentRepository _studentRepo;
        private readonly EnrollmentRepository _enrollmentRepo;

        public AdminService(
            PaymentService paymentService,
            TutorFeeService feeService,
            TutorRepository tutorRepo,
            StudentRepository studentRepo,
            EnrollmentRepository enrollmentRepo
            )
        {
            _paymentService = paymentService;
            _feeService = feeService;
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
                TotalPaymentsReceived = _paymentService.GetPaidPaymentsTotal(),
                TotalPaymentsPending = _paymentService.GetPendingPaymentsTotal(),
                TotalFeesPending = _feeService.GetPendingFeesTotal()
            };
        }
    }
}
