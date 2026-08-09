using Linguaflow.DAL;
using LinguaFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.BLL
{
    public class CourseService
    {
        private readonly CourseRepository _repo;

        public CourseService(CourseRepository repo)
        {
            _repo = repo;
        }

        public List<Course> GetAll()
        {
            return _repo.GetAll();
        }

        public Course GetById(int id)
        {
            return _repo.GetById(id);
        }
    }
}

