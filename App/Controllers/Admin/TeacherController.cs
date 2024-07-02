using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Teachers;
using Service.Services.Interfaces;

namespace App.Controllers.Admin
{
 
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;           
        }


        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _teacherService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto request)
        {
            await _teacherService.Create(request);
            return CreatedAtAction(nameof(Create), new {Response = "Data succesfully created"});
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery]int id, [FromBody] TeacherEditDto request)
        {
            await _teacherService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _teacherService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _teacherService.GetById(id));
        }
    }
}
