using AutoMapper;
using Data.Models;
using Models;

namespace StudentApp2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Student, StudentModel>();
            CreateMap<Group, GroupModel>();
        }
    }
}
