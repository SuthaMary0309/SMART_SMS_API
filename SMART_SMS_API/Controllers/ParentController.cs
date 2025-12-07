using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        // 🟣 Add new Parent (parameters-based)
        [HttpPost("add")]
        public async Task<IActionResult> AddParent([FromBody] ParentRequestDTO request)
        {
            var parent = await _parentService.AddParentAsync(request);
            
                parent.UserID = Guid.NewGuid(); // assign default UserID
                                                // save parent
            

            return Ok(parent);
        }

        // 🟢 Get all Parents
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllParents()
        {
            var parents = await _parentService.GetAllParentsAsync();
            return Ok(parents);
        }

        // 🟡 Get Parent by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetParentById(Guid id)
        {
            var parent = await _parentService.GetParentByIdAsync(id);
            if (parent == null)
                return NotFound(new { message = "Parent not found" });

            return Ok(parent);
        }

        // 🔵 Update Parent (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateParent(Guid id, [FromBody] ParentRequestDTO request)
        {
            var updated = await _parentService.UpdateParentAsync(id, request);
            if (updated == null)
                return NotFound(new { message = "Parent not found" });

            return Ok(updated);
        }

        // 🔴 Delete Parent
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteParent(Guid id)
        {
            var deleted = await _parentService.DeleteParentAsync(id);
            if (!deleted)
                return NotFound(new { message = "Parent not found or already deleted" });

            return Ok(new { message = "Parent deleted successfully" });
        }
    }
}
