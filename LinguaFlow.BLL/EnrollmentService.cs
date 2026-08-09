using Linguaflow.DAL;
using LinguaFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.BLL
{
    public class EnrollmentService
    {
        private readonly EnrollmentRepository _repo;

        public EnrollmentService(EnrollmentRepository repo)
        {
            _repo = repo;
        }

        public List<Enrollment> GetAll()
        {
            return _repo.GetAll();
        }

        public Enrollment GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void UpdateStatus(int id, string status)
        {
            _repo.UpdateStatus(id, status);
        }
    }
}
