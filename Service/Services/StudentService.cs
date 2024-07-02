using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Students;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IGroupStudentRepository _groupStudentRepo;
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public StudentService(IStudentRepository studentRepo,
                              IGroupStudentRepository groupStudentRepo,
                              IMapper mapper,
                              IGroupRepository groupRepository)
        {
            _studentRepo = studentRepo;
            _groupStudentRepo = groupStudentRepo;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task AddGroup(int? studentId, int? groupId)
        {
            if (studentId == null) throw new ArgumentNullException();

            if (groupId == null) throw new ArgumentNullException();

            var groupStudents = await _groupStudentRepo.GetAllByStudentId((int)studentId);

            if (groupStudents.Count() != 0)
            {
                var groups = groupStudents.Select(m => m.Group).ToList();

                foreach (var group in groups)
                {
                    if (group.Id != groupId)
                    {
                        await _groupStudentRepo.CreateAsync(new GroupStudents { GroupId = (int)groupId, StudentId = (int)studentId });
                        return;
                    }
                }
            }
            else
            {
                await _groupStudentRepo.CreateAsync(new GroupStudents { GroupId = (int)groupId, StudentId = (int)studentId });
            }


        }

        public async Task CreateAsync(StudentCreateDto model)
        {
            var data = _mapper.Map<Student>(model);
            await _studentRepo.CreateAsync(data);

            foreach (var id in model.GroupIds)
            {
                var group = await _groupRepository.GetById(id);
                if (group.Capacity > group.GroupStudents.Select(m => m.SoftDelete).Count())
                {
                    await _groupStudentRepo.CreateAsync(new GroupStudents
                    {
                        StudentId = data.Id,
                        GroupId = id
                    });
                }
            }
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var student = await _studentRepo.GetById((int)id);

            if (student is null) throw new NotFoundException("Student was not found");

            await _studentRepo.DeleteAsync(student);
        }

        public async Task EditAsync(int? id, StudentEditDto model)
        {
            if (id is null) throw new ArgumentNullException();

            var student = await _studentRepo.GetById((int)id);

            if (student is null) throw new NotFoundException("Student was not found");

            _mapper.Map(model, student);
            await _studentRepo.EditAsync(student);
        }

        public async Task<IEnumerable<StudentDto>> GetAllWithInclude()
        {
           var students =  await _studentRepo.FindAllWithIncludes()
                .Include(m => m.GroupStudents)
                .ThenInclude(m=>m.Group)
                .ToListAsync();
            var mappedStudents = _mapper.Map<List<StudentDto>>(students);
            return mappedStudents;
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            return _mapper.Map<StudentDto>(await _studentRepo.GetById(id));
        }

        public async Task RemoveGroup(int? studentId, int? groupId)
        {
            if (studentId == null) throw new ArgumentNullException();

            if (groupId == null) throw new ArgumentNullException();

            var groupStudents = await _groupStudentRepo.GetAllByStudentId((int)studentId);

            var groups = groupStudents.Select(m => m.Group).ToList();

            foreach (var group in groups)
            {
                if (group.Id == groupId)
                {
                    await _groupStudentRepo.DeleteAsync(await _groupStudentRepo.FindRow((int)studentId,(int)groupId));
                }
            }
        }
    }
}
