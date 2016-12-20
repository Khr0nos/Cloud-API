using System;
using System.Linq;
using System.Net;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

// ReSharper disable FormatStringProblem

namespace Cloud_API.Controllers {
    [Route("api/[controller]")]
    public class DevicesController : Controller {
        private readonly DevicesContext db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DevicesController(DevicesContext context) {
            db = context;
        }

        // GET: api/Devices
        [HttpGet]
        public ActionResult Get() {
            logger.Info("GET All Devices");
            //var res = await dbcontext.Devices.FromSql("spDevices_GetAll").ToArrayAsync();
            return Json(db.Devices);
        }

        // GET api/Devices/5
        [HttpGet("{id}")]
        public ActionResult Get(string id) {
            logger.Info($"GET Device with id={id}");
            int num;
            if (int.TryParse(id, out num)) {
                //var res = await db.Devices.FromSql("spDevices_GetDevice @p0", id).ToArrayAsync();
                //if (res.Length == 0) return NotFound();
                var res = db.Devices.Find(num);
                if (res == null) return NotFound();
                return Json(res);
            }
            return BadRequest(id);
        }

        // POST api/Devices
        [HttpPost]
        public ActionResult Post([FromBody]Devices nou) {
            logger.Info("POST Insert new Device");
            //if (nou == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //var res = await db.Devices.FromSql(
            //    "spDevices_InsertDevice @p0 @p1 @p2 @p3 @p4 @p5 @p6 @p7 @p8 @p9 @p10 @p11", nou.Iddevice,
            //    nou.DeviceName, nou.IdauxDeviceType, nou.DeviceEnabled, nou.DeviceConnected,
            //    nou.DeviceNeedLogin, nou.DeviceInterval, nou.DeviceCreationDate, nou.DeviceUsername,
            //    nou.DevicePassword, nou.IddeviceProtocol, nou.DeviceAux).ToArrayAsync();
            db.Devices.Add(nou);

            try {
                db.SaveChanges();
            } catch (DbUpdateException ex) {
                logger.Warn(ex.Message);
                if (DeviceExists(nou.Iddevice)) {
                    return StatusCode((int) HttpStatusCode.Conflict);
                }
            } catch (Exception ex) {
                logger.Error(ex, ex.Message);
                return BadRequest(nou);
            }
            return Created($"/api/Devices/{nou.Iddevice}", nou);
        }

        // PUT api/Devices/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {}

        // DELETE api/Devices/5
        [HttpDelete("{id}")]
        public void Delete(int id) {}

        private bool DeviceExists(int iddevice) {
            return db.Devices.Any(e => e.Iddevice == iddevice);
        }
    }
}
