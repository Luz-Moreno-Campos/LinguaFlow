using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string CourseName { get; set; } 

        public string? Description { get; set; } = "The description for this course will be available soon";

        public decimal Price { get; set; }

        // Many-to-many with Tutor
        public ICollection<Tutor> CourseTutors { get; set; } = new List<Tutor>();

        // Relationship with Enrollment
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

}
