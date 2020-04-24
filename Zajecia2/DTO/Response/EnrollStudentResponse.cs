using System.ComponentModel.DataAnnotations;
using Zajecia2.Models;

namespace Zajecia2.DTO.Response
{
    public class EnrollStudentResponse
    {
        public int Semester { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
