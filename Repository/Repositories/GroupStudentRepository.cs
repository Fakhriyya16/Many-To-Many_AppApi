using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class GroupStudentRepository : BaseRepository<GroupStudents>, IGroupStudentRepository
    {
        public GroupStudentRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<GroupStudents> FindRow(int studentId, int groupId)
        {
            return await _entities.Where(m => m.GroupId == groupId && m.StudentId == studentId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GroupStudents>> GetAllByStudentId(int studentId)
        {
            return await _entities.Where(m=>m.StudentId == studentId).Include(m=>m.Group).ToListAsync();
        }

    }
}
