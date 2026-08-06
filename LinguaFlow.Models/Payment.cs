using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models
{
    public class Payment
    {
        public int Id { get; set; }
       
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
      
        public decimal Amount { get; set; }
 
        public string Status { get; set; } = "Pending";

        public string Method { get; set; } = string.Empty;
 
        public DateTime CreatedAt { get; set; } 

        public DateTime? PaidAt { get; set; }
    }
}

