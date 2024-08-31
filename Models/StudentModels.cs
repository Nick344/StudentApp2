using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int GroupId { get; set; }

        public GroupModel Group { get; set; }
    }

    public class StudentShortModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }

    public class CreateStudentModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int GroupId { get; set; }
    }

    public class UpdateStudentModel : CreateStudentModel
    {
        int Id { get; set; }
    }
}
