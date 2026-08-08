using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models.ViewModels
{
    public class TutorEditViewModel
    {

        public int Id { get; set; }

        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Bio { get; set; } = string.Empty;

        public int LanguageId { get; set; }
        public List<Language> Languages { get; set; } = new List<Language>();

        public string Availability { get; set; } 
    }
}
