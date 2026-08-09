using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models.ViewModels
{
    public class EnrollmentIndexViewModel
    {
       
        public string StudentSearch { get; set; }
        public string TutorSearch { get; set; }
        public int? CourseSearch { get; set; }   
        public string StatusSearch { get; set; }

      
        public List<Course> Courses { get; set; }

       
        public List<EnrollmentListViewModel> Enrollments { get; set; }
    }

}
