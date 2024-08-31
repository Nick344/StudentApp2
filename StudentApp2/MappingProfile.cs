using AutoMapper;
using Data.Models;
using Models;

namespace StudentApp2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Student, StudentModel>().ReverseMap();
            CreateMap<CreateStudentModel, Student>();
            CreateMap<Data.Models.Group, GroupModel>().ReverseMap();
            CreateMap<CreateGroupModel, Data.Models.Group>();
        }
    }
}
