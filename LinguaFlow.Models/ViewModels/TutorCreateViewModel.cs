using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models.ViewModels
{
    public class TutorCreateViewModel
    {

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Bio { get; set; } = string.Empty;

        public int LanguageId { get; set; }
        public List<Language> Languages { get; set; } = new();

        public string Availability { get; set; } = null!;
    }  

}
