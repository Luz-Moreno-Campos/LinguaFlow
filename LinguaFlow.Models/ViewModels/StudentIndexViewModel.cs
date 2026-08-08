
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.Models.ViewModels
{
    public class StudentIndexViewModel
    {
        public string SearchName { get; set; }
        public int? SearchLanguageId { get; set; }

        public List<Language> Languages { get; set; } = new();
        public List<StudentListViewModel> Students { get; set; } = new();
    }
}


