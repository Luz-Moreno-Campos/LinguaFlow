using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models.ViewModels
{
    public class TutorListItemViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string LanguageName { get; set; }   // viene de Language.Name
        public string Availability { get; set; }
    }

    public class TutorIndexViewModel
    {
        public string SearchName { get; set; }
        public int? SearchLanguageId { get; set; }

        public List<Language> Languages { get; set; } = new();
        public List<TutorListItemViewModel> Tutors { get; set; } = new();
    }


}
