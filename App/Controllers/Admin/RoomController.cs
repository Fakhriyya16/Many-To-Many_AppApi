using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Rooms;
using Service.Services.Interfaces;

namespace App.Controllers.Admin
{

    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]RoomCreateDto request)
        {
            await _roomService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { Response = "succesfull" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _roomService.GetAllAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int id, [FromBody] RoomEditDto request)
        {
            await _roomService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _roomService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _roomService.GetByIdAsync(id));
        }
    }
}
