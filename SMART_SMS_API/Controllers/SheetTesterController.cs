using Microsoft.AspNetCore.Mvc;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/test-sheet")]
    public class SheetTestController : ControllerBase
    {
        private readonly GoogleSheetsService _sheet;

        public SheetTestController(GoogleSheetsService sheet)
        {
            _sheet = sheet;
        }

        [HttpPost("write")]
        public async Task<IActionResult> Write()
        {
            await _sheet.AppendRowAsync("Sheet1", new List<object> { "Mary", "Maths", 95 });
            return Ok("Row inserted");
        }

        [HttpGet("read")]
        public async Task<IActionResult> Read()
        {
            var rows = await _sheet.ReadRowsAsync("Sheet1!A1:D100");
            return Ok(rows);
        }
    }

}
