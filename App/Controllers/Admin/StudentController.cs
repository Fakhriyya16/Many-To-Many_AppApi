using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Students;
using Service.Services.Interfaces;

namespace App.Controllers.Admin
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto request)
        {
            await _studentService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { Response = "Succesfull" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mappedStudents = await _studentService.GetAllWithInclude();
            return Ok(mappedStudents);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int id, [FromBody] StudentEditDto request)
        {
            await _studentService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _studentService.GetByIdAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _studentService.GetByIdAsync(id));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveGroup([FromQuery] int studentId, [FromQuery] int groupId)
        {
            await _studentService.RemoveGroup(studentId, groupId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup([FromQuery] int studentId, [FromQuery] int groupId)
        {
            await _studentService.AddGroup(studentId, groupId);
            return Ok();
        }
    }
}
