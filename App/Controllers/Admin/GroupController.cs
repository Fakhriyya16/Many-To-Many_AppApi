using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Groups;
using Service.Services.Interfaces;

namespace App.Controllers.Admin
{
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupCreateDto request)
        {
            await _groupService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { Response = "Successfull" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _groupService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _groupService.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int id, [FromBody] GroupEditDto request)
        {
            await _groupService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _groupService.DeleteAsync(id);
            return Ok();
        }

    }
}
