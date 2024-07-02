using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupStudentRepository : IBaseRepository<GroupStudents>
    {
        Task<IEnumerable<GroupStudents>> GetAllByStudentId(int studentId);
        Task<GroupStudents> FindRow(int studentId,int groupId);
    }
}
