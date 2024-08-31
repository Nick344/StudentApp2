using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GroupModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public ICollection<StudentModel> Students { get; set; }
    };

    public class CreateGroupModel
    { 
    public string GroupName { get; set; }
    }
}
