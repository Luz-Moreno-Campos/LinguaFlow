using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models
{
    public class TutorFee
    {
        public int Id { get; set; }

        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }

        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }

       
        public decimal FeeAmount { get; set; }

   
        public string Status { get; set; } = "Pending"; // Pending, Paid, Overdue

    
        public DateTime CreatedAt{ get; set; } 

 
        public DateTime? PaidAt { get; set; }
    }

}
