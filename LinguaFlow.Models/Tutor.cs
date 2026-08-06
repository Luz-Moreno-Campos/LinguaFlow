using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinguaFlow.Models
{
    public class Tutor
    {
        public int Id { get; set; }

        public string FirstName { get; set; } 

        public string LastName { get; set; } 

        public string? Bio { get; set; } = string.Empty;

        // Tutor teaches only one language
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public string Availability { get; set; }

    
        // Many-to-many : tutor delivers many courses, and courses can be delivered by many teachers
        public ICollection<Course> Courses { get; set; } = new List<Course>();


        //One-to-many: a tutor can have many fees, each fee belongs to one tutor
        public ICollection<TutorFee> TutorFees { get; set; } = new List<TutorFee>();

    }

}
