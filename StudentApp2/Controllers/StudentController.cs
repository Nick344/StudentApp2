using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;


namespace StudentApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UniversityDbContext dbContext;

        public StudentController(UniversityDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }
        [HttpPut("id")]
        public async Task<ActionResult<StudentModel>> UpdateStudent(int id,[FromBody] StudentModel updateStudent)
        { 
        var existingStudent = await dbContext.Student.FindAsync(id);
            
            if (existingStudent == null)
            {
                return NotFound("Student not found");
            }
            existingStudent.Name = updateStudent.Name;
            existingStudent.Age = updateStudent.Age;

          
                await dbContext.SaveChangesAsync();
          
            


            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<StudentModel>> AddStudent(String name, int age, int groupId)
        {
            var student = new Student
                { Name = name, Age = age, GroupId = groupId };

            dbContext.Student.Add(student);
            await dbContext.SaveChangesAsync();

            return StatusCode(201, student);
        }
        [HttpGet("id")]
        public async Task<ActionResult<StudentModel>> GetStudent(int id)
        {
            var student = await dbContext.Student.Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                var studentModel = new StudentModel() 
                { 
                    Id = student.Id,
                    Name = student.Name,
                    Age = student.Age,
                    GroupId = student.GroupId,
                    Group = new GroupModel() 
                    { 
                        GroupName = student.Group.GroupName, 
                        Id = student.Group.Id
                    }
                };
                return studentModel;
            }

        }
        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await dbContext.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

          else
            {
                dbContext.Student.Remove(student);
                dbContext.SaveChanges();
            }
            return NoContent();
        }
    }
}
