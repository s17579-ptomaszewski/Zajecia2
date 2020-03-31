using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zajecia2.Models
{

    public interface IStudentDbService
    {
        public IEnumerable<Student> GetStudents();
        public IEnumerable<Enrollment> GetStudentEnrollmentInfo(String id);
    }
}
