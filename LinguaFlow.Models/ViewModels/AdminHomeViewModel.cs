namespace LinguaFlow.Models.ViewModels
{
    public class AdminHomeViewModel
    {
        public int TotalTutors { get; set; }
        public int TotalStudents { get; set; }
        public int TotalEnrollments { get; set; }
        public decimal TotalPaymentsReceived { get; set; }

        public decimal TotalPaymentsPending { get; set; }

        public decimal TotalFeesPending { get; set; }
    }

}
