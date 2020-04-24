using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zajecia2.DTO;
using Zajecia2.DTO.Request;
using Zajecia2.DTO.Response;

namespace Zajecia2.Services
{
   public interface IEnrollmentDbServices
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);

        PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request);
    }
}
