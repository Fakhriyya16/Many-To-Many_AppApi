using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int EducationId { get; set; }
        public Education Education { get; set; }
        public ICollection<GroupStudents> GroupStudents { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public ICollection<GroupTeachers> GroupTeachers { get; set; }
    }
}
