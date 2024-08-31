using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class StudentService : IStudentService
    {

        private readonly UniversityDbContext _context;
        private readonly IMapper _mapper;
        public StudentService(UniversityDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentModel> GetStudentsById(int id)
        {
            var student = await _context.Student.Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                throw new KeyNotFoundException("Student not found");
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

        public async Task<StudentModel> UpdateStudent(int id,CreateStudentModel model)
        {
            

            var existingStudent = await _context.Student.FirstOrDefaultAsync(x => x.Id == id);
            

            existingStudent.Name = model.Name;
            existingStudent.Age = model.Age;

            await _context.SaveChangesAsync();

            var studentModel = _mapper.Map<StudentModel>(existingStudent);
            return studentModel;
        }

        public async Task<StudentModel> CreateStudent(CreateStudentModel model)
        {
            var student = _mapper.Map<Student>(model);

            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            var studentModel = _mapper.Map<StudentModel>(student);
            return studentModel;
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                throw new KeyNotFoundException("Student not found");
            }

            else
            {
                _context.Student.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
