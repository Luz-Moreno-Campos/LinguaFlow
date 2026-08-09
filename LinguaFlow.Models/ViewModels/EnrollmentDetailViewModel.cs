using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models.ViewModels
{
    public class EnrollmentDetailsViewModel
    {
        public int Id { get; set; }

        public string StudentName { get; set; }
        public string TutorName { get; set; }
        public string CourseTitle { get; set; }

        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }

        public decimal Payment { get; set; }
        public decimal TutorFee { get; set; }
    }
}

