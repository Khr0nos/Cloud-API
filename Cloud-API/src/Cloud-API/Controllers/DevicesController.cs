using System.Threading.Tasks;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

// ReSharper disable FormatStringProblem

namespace Cloud_API.Controllers
{
    [Route("api/[controller]")]
    public class DevicesController : Controller {
        private readonly DevicesContext dbcontext;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DevicesController(DevicesContext context) {
            dbcontext = context;
        }

        // GET: api/Devices
        [HttpGet]
        public ActionResult Get() {
            logger.Info("GET All Devices");
            //var res = await dbcontext.Devices.FromSql("spDevices_GetAll").ToArrayAsync();
            return Json(dbcontext.Devices);
        }

        // GET api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id) {
            logger.Info($"GET Device with id={id}");
            int num;
            if (int.TryParse(id, out num)) {
                var res = await dbcontext.Devices.FromSql("spDevices_GetDevice @p0", id).ToArrayAsync();
                if (res.Length == 0) return NotFound();
                return Json(res);
            }
            return BadRequest();
        }

        // POST api/Devices
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Devices nou) {
            if (nou == null) return BadRequest();
            if (ModelState.IsValid) {
                var res = await dbcontext.Devices.FromSql(
                    "spDevices_InsertDevice @p0 @p1 @p2 @p3 @p4 @p5 @p6 @p7 @p8 @p9 @p10 @p11", nou.Iddevice,
                    nou.DeviceName, nou.IdauxDeviceType, nou.DeviceEnabled, nou.DeviceConnected,
                    nou.DeviceNeedLogin, nou.DeviceInterval, nou.DeviceCreationDate, nou.DeviceUsername,
                    nou.DevicePassword, nou.IddeviceProtocol, nou.DeviceAux).ToArrayAsync();
                if (res.Length <= 0) return BadRequest();
                return Created($"/api/Devices/{nou.Iddevice}", nou);
            }
            return NoContent();
        }

        // PUT api/Devices/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {}

        // DELETE api/Devices/5
        [HttpDelete("{id}")]
        public void Delete(int id) {}
    }
}
