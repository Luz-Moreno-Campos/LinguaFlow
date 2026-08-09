using Linguaflow.DAL;
using LinguaFlow.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.BLL
{
    public class EnrollmentService
    {
        private readonly EnrollmentRepository _enrollmentRepo;

        public EnrollmentService(EnrollmentRepository enrollmentRepo)
        {
            _enrollmentRepo = enrollmentRepo;
        }

        public List<Enrollment> GetAll()
        {
            return _enrollmentRepo.GetAll();
        }

        public Enrollment GetById(int id)
        {
            return _enrollmentRepo.GetById(id);
        }
     

        public void CreateEnrollment(Enrollment enrollment)
        {
            _enrollmentRepo.CreateEnrollment(enrollment);
        }


        public void UpdateStatus(int id, string status)
        {
            _enrollmentRepo.UpdateStatus(id, status);
        }
    }
}
