using Linguaflow.DAL;
using LinguaFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.BLL
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepo;

        public StudentService(StudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public int CountStudents()
        {
            return _studentRepo.CountStudents();
        }

        public Student GetStudentById(int id)
        {
            return _studentRepo.GetById(id);
        }

        public List<Student> GetStudents(string searchName, int? languageId)
        {
            return _studentRepo.GetAll(searchName, languageId);
        }

        
        public void CreateStudent(Student student)
        {
            _studentRepo.Add(student);
        }

        
        public void UpdateStudent(Student student)
        {
            _studentRepo.Update(student);
        }

        
        public void DeleteStudent(int id)
        {
            _studentRepo.Delete(id);
        }
    }
}

