using System;
using System.Linq;
using System.Net;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Cloud_API.Controllers {
    [Route("api/[controller]")]
    public class devicesController : Controller {
        private readonly DatabaseContext db;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public devicesController(DatabaseContext context) {
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
        public ActionResult Post([FromBody] Devices nou) {
            logger.Info("POST Insert new Device");
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
                logger.Warn(ex, ex.Message);
                if (DeviceExists(nou.Iddevice)) {
                    return StatusCode((int) HttpStatusCode.Conflict, nou);
                }
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest(nou);
            }

            logger.Info("Device created correctly");
            return Created($"/api/devices/{nou.Iddevice}", nou);
        }

        // PUT api/Devices/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devices nou) {
            logger.Info($"PUT Update device with id={id}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != nou.Iddevice) return BadRequest(nou);

            //db.Entry(nou).State = EntityState.Modified;
            db.Devices.Update(nou);

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException ex) {
                logger.Warn(ex, ex.Message);
                if (!DeviceExists(id)) return NotFound();
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
            }
            return NoContent();
        }

        // DELETE api/Devices/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            logger.Info("DELETE Device");
            var res = db.Devices.Find(id);
            if (res == null) return NotFound();

            db.Devices.Remove(res);

            try {
                db.SaveChanges();
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
            }

            logger.Info($"Device with id={id} deleted");
            return Ok(res);
        }

        #region Auxiliar

        private bool DeviceExists(int iddevice) {
            return db.Devices.Any(e => e.Iddevice == iddevice);
        }

        #endregion

        #region Overrides of Controller

        protected override void Dispose(bool disposing) {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
