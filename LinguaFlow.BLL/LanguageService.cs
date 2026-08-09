using Linguaflow.DAL;
using LinguaFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.BLL
{
    public class LanguageService
    {
        private readonly LanguageRepository _languageRepo;

        public LanguageService(LanguageRepository languageRepo)
        {
            _languageRepo = languageRepo;
        }

        public List<Language> GetAll()
        {
            return _languageRepo.GetAll();
        }

        public Language GetById(int id)
        {
            return _languageRepo.GetById(id);
        }
    }
}
