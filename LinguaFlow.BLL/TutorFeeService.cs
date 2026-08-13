using Linguaflow.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaFlow.BLL
{
    public class TutorFeeService
    {
        private readonly TutorFeeRepository _tutorFeeRepository;

        public TutorFeeService(TutorFeeRepository tutorFeeRepository)
        {
            _tutorFeeRepository = tutorFeeRepository;
        }

        public decimal GetPendingFeesTotal()
        {
            return _tutorFeeRepository.SumPendingFees();
        }
    }
}