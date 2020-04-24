using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zajecia2.DTO;
using Zajecia2.DTO.Request;
using Zajecia2.DTO.Response;
using Zajecia2.Models;
using Zajecia2.Services;

namespace Zajecia2.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private string SqlConn = " Data Source=db-mssql;Initial Catalog=s17579;Integrated Security=True";
        private  IEnrollmentDbServices _enrollmentDbServices;

        public EnrollmentsController(IEnrollmentDbServices enrollmentDbServices)
        {
            _enrollmentDbServices = enrollmentDbServices;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                var response = _enrollmentDbServices.EnrollStudent(request);
                return CreatedAtAction("EnrollStudent", response);
            }
            catch (ArgumentException)
            {
                return NotFound("Studies with given name not found");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Student with given index number already exists");
            }
        }

        [Route("promotions")]
        [HttpPost]
        [ActionName("PromoteStudents")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            try
            {
                var response = _enrollmentDbServices.PromoteStudents(request);
                return CreatedAtAction("PromoteStudents", response);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Invalid request");
            }
            catch (SqlException)
            {
                return NotFound("Studies with given name not found");
            }
            catch (ArgumentException)
            {
                return BadRequest("No values for response found");
            }
        }
    }

}
    
