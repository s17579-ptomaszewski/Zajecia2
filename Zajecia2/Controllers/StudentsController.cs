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
        /*
         * HttpGet
         * HttpPost
         * HttpPut
         * HttpPath
         * HttpDelete
         */

        [HttpGet]
        public string GetStudents(string orderBy)
        {
            return $"Kowalski, Malewski, Nowak sortowanie={orderBy}";
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

        [HttpGet("{id}")]
        public IActionResult getStudentbyId(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            if(id == 2)
            {
                return Ok("Malewski");
            }

            return NotFound($"nie znaleziono studenta o id {id}");
        }

        }
    }