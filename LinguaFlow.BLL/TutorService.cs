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
        private readonly TutorRepository _repo;

        public TutorService(TutorRepository repo)
        {
            _repo = repo;
        }

        
        public int CountTutors()
        {
            return _repo.CountTutors();
        }

        
        public List<Tutor> GetTutors(string searchName, int? languageId)
        {
            return _repo.GetAll(searchName, languageId);
        }

        
        public void CreateTutor(Tutor tutor)
        {
            _repo.Add(tutor);
        }

    
        public void UpdateTutor(Tutor tutor)
        {
            _repo.Update(tutor);
        }

    
        public void DeleteTutor(int id)
        {
            _repo.Delete(id);
        }
    }
}


