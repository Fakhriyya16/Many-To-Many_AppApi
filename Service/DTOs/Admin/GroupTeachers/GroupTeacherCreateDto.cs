using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.GroupTeachers
{
    public class GroupTeacherCreateDto
    {
        public int TeacherId { get; set; }
        public int GroupId { get; set; }

    }
}
