using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Service;

namespace StudentApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> AddGroup(CreateGroupModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdGroup = await _groupService.CreateGroup(model);

            return StatusCode(201, createdGroup);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteGroup(int id)
        {
           await  _groupService.DeleteGroup(id);
            return NoContent();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(GroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> GetGroup(int id)
        {
  
            return Ok(await _groupService.GetGroup(id));

        }
    }
}
