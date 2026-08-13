using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models.ViewModels
{
    public class TutorCreateViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        public string Bio { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a language.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a language.")]
        public int LanguageId { get; set; }

        public List<Language> Languages { get; set; } = new();

        [Required(ErrorMessage = "Availability is required.")]
        public string Availability { get; set; }
    }
}