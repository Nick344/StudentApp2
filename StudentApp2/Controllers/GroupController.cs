using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace StudentApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UniversityDbContext dbContext;

        public GroupController(UniversityDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<GroupModel>> AddGroup(String name)
        {
            var group = new Group
            { GroupName = name};
            
            dbContext.GroupsStudent.Add(group);
            await dbContext.SaveChangesAsync();

            return Ok(group);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            var group = await dbContext.GroupsStudent.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            else
            {
                dbContext.GroupsStudent.Remove(group);
                dbContext.SaveChanges();
            }
            return NoContent();
        }

        [HttpGet("id")]
        public async Task<ActionResult<GroupModel>> GetGroup(int id)
        {
            var group = await dbContext.GroupsStudent.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == id);

            if (group == null)
            {
                return NotFound("Group not found"); 
            }

           
                var groupModel = new GroupModel()
                {
                    Id = id,
                    GroupName = group.GroupName,
                    Students = group.Students.Select(s => new StudentModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Age = s.Age,
                        GroupId = group.Id,
                    }).ToList()
                };
            

            return Ok(groupModel);








        }
    }
}
