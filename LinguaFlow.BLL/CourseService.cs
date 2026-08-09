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
        private readonly CourseRepository _courseRepo;

        public CourseService(CourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public List<Course> GetAll()
        {
            return _courseRepo.GetAll();
        }

        public Course GetById(int id)
        {
            return _courseRepo.GetById(id);
        }
    }
}

