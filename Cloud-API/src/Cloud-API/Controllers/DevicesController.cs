using System.Threading.Tasks;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cloud_API.Controllers {
    [Route("api/[controller]")]
    public class DevicesController : Controller {
        private readonly DevicesContext _context;

        public DevicesController(DevicesContext context) {
            _context = context;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<JsonResult> Get() {
            var res = await _context.Devices.FromSql("spDevices_GetAll").ToArrayAsync();
            return Json(res);
        }

        // GET api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id) {
            var res = await _context.Devices.FromSql("spDevices_GetDevice @p0", id).ToArrayAsync();
            if (res.Length == 0) return NotFound();
            return Json(res);
        }

        // POST api/Devices
        [HttpPost]
        public void Post([FromBody] string value) {}

        // PUT api/Devices/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {}

        // DELETE api/Devices/5
        [HttpDelete("{id}")]
        public void Delete(int id) {}
    }
}
