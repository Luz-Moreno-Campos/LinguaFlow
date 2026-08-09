using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models.ViewModels
{
    public class ExploreTutorsViewModel
    {
        public Language Language { get; set; }
        public List<Course> Courses { get; set; }
        public List<Tutor> Tutors { get; set; }

    }
}
