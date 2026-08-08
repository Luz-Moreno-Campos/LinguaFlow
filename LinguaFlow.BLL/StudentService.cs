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
        private readonly StudentRepository _repo;

        public StudentService(StudentRepository repo)
        {
            _repo = repo;
        }

        public int CountStudents()
        {
            return _repo.CountStudents();
        }

        public Student GetStudentById(int id)
        {
            return _repo.GetById(id);
        }

        public List<Student> GetStudents(string searchName, int? languageId)
        {
            return _repo.GetAll(searchName, languageId);
        }

        
        public void CreateStudent(Student student)
        {
            _repo.Add(student);
        }

        
        public void UpdateStudent(Student student)
        {
            _repo.Update(student);
        }

        
        public void DeleteStudent(int id)
        {
            _repo.Delete(id);
        }
    }
}

