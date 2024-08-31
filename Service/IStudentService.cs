using Data.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
    public interface IStudentService
    {
        Task<StudentModel> GetStudentsById(int id);
        Task<StudentModel> UpdateStudent (int id ,CreateStudentModel student);
        Task DeleteStudent (int id);
        Task<StudentModel> CreateStudent(CreateStudentModel model);
    }
}
