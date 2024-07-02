using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.GroupTeachers
{
    public class GroupTeacherDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }

    }
}
