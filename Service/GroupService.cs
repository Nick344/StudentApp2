using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly UniversityDbContext _context;

        public GroupService(IMapper mapper, UniversityDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GroupModel> CreateGroup(CreateGroupModel model)
        {
            var group = _mapper.Map<Data.Models.Group>(model);
            _context.GroupsStudent.Add(group);
          await  _context.SaveChangesAsync();

            var groupModel = _mapper.Map<GroupModel>(group);
            return groupModel;
        }

        public async Task DeleteGroup(int id)
        {
            var group = await _context.GroupsStudent.FindAsync(id);

            if (group == null)
            {
                throw new KeyNotFoundException("Group not found");
            }
            else
            {
                _context.GroupsStudent.Remove(group);
                _context.SaveChanges();
            }
        }

        public async Task<GroupModel> GetGroup(int id)
        {
            var group = await _context.GroupsStudent.Include(x => x.Students)
                                                      .FirstOrDefaultAsync(x => x.Id == id);

            if (group == null)
            {
                throw new KeyNotFoundException("Group not found");
            }

            var groupModel = _mapper.Map<GroupModel>(group);

            return groupModel;
            
        }
       
    }

  
}


