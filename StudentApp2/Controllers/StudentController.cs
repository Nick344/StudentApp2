using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Service;


namespace StudentApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;


        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        [HttpPut("id")]
        [ProducesResponseType(typeof(StudentModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdateStudent(int id, CreateStudentModel model)
        {
            var updatedStudent = await studentService.UpdateStudent(id, model);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(StudentModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> AddStudent(CreateStudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdStudent = await studentService.CreateStudent(model);

            return StatusCode(201, createdStudent);
        }
        [HttpGet("id")]
        [ProducesResponseType(typeof(StudentModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> GetStudent(int id)
        {
            try
            {
                var student = await studentService.GetStudentsById(id);
                return Ok(student);
            }

            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await studentService.DeleteStudent(id);
            return NoContent();
        }

    }
}
    



