using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models.ViewModels
{
    public class StudentDetailViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<StudentLanguageInfo> Languages { get; set; }
            = new List<StudentLanguageInfo>();
    }

    public class StudentLanguageInfo
    {
        public string LanguageName { get; set; }

        public List<string> Courses { get; set; } = new List<string>();

        public List<string> Tutors { get; set; } = new List<string>();
    }
}

