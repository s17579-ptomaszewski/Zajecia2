using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zajecia2.Models;

namespace Zajecia2.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private string SqlConn = "Data Source=db-mssql;Initial Catalog=s17579;Integrated Security=True";
        private readonly IStudentDbService _studentDbService;

        public StudentsController(IStudentDbService studentDbService)
        {
            _studentDbService = studentDbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_studentDbService.GetStudents());
        }

        [HttpGet("{IndexNumber}")]
        public IActionResult GetStudentEnrollmentInfo(String IndexNumber)
        {
            return Ok(_studentDbService.GetStudentEnrollmentInfo(IndexNumber));
        }

        [HttpPost]

        public IActionResult addStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 200000)}";

            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult editStudent(int id)
        {
            return Ok("Aktualizaca dokończona");
        }

        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }


    }
}