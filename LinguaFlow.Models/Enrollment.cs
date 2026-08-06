using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

      
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
    
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Active";

    }
}
