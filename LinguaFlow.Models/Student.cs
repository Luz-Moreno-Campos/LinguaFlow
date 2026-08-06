using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; } 

        public string LastName { get; set; } 

        public string Email { get; set; } 

        public string Password { get; set; } 

        
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

}
