using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinguaFlow.Models;
using Linguaflow.DAL;

namespace LinguaFlow.BLL
{
    public class TutorService
    {
        private readonly TutorRepository _tutorRepo;

        public TutorService(TutorRepository tutorRepo)
        {
            _tutorRepo = tutorRepo;
        }

        
        public int CountTutors()
        {
            return _tutorRepo.CountTutors();
        }

        
        public List<Tutor> GetTutors(string searchName, int? languageId)
        {
            return _tutorRepo.GetAll(searchName, languageId);
        }

        
        public void CreateTutor(Tutor tutor)
        {
            _tutorRepo.Add(tutor);
        }

    
        public void UpdateTutor(Tutor tutor)
        {
            _tutorRepo.Update(tutor);
        }

    
        public void DeleteTutor(int id)
        {
            _tutorRepo.Delete(id);
        }
    }
}


