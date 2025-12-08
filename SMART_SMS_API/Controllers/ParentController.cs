using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [Route("api/parent")]
    [ApiController]
    [Authorize] // require auth so we can read user id claim
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;
        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var parents = await _parentService.GetAllParentsAsync();
            return Ok(parents);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ParentRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? userId = null;
            if (Guid.TryParse(userIdClaim, out var uid)) userId = uid;

            var created = await _parentService.AddParentAsync(dto, userId);
            return Ok(created);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ParentRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? userId = null;
            if (Guid.TryParse(userIdClaim, out var uid)) userId = uid;

            var updated = await _parentService.UpdateParentAsync(id, dto, userId);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _parentService.DeleteParentAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
