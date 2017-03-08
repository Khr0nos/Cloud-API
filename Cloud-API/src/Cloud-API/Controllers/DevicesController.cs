using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CloudAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NLog;

namespace CloudAPI.Controllers {
    /// <summary>
    /// API entry point to manage logic Devices. Devices controller of the API
    /// </summary>
    /// <remarks>This controllers returns response data formatted in JSON as default</remarks>
    [Route("api/[controller]")]
    [Authorize]
    public class devicesController : Controller {
        /// <summary>
        /// Represents the connection with the underlying database. Uses EntityFrameworkCore
        /// </summary>
        private readonly DatabaseContext db;
        /// <summary>
        /// Provides a logging interface for this controller of the API
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public devicesController(DatabaseContext context) {
            db = context;
        }

        // GET: api/devices
        /// <summary>
        /// Gets all devices definitions
        /// </summary>
        /// <remarks>Returns a JSON array of Devices</remarks>
        /// <returns>Devices Data Collection</returns>
        // <response code="200">Returns all Devices items</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<Devices>), 200)]
        public IActionResult Get() {
            logger.Info("GET All devices");
            //return Json(db.Devices);
            return Ok(db.Devices);
        }

        // GET api/devices/5
        /// <summary>
        /// Gets specific Device
        /// </summary>
        /// <param name="id">Device identifier</param>
        /// <returns>Devices</returns>
        // <response code="200">Returns selected Device</response>
        // <response code="404">Device not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Devices), 200)]
        [ProducesResponseType(typeof(JObject), 404)]
        public IActionResult Get(int id) {
            logger.Info($"GET Device with id={id}");
            var res = db.Devices.Find(id);
            if (res == null) return NotFound(new JObject { ["Device not found by id:"] = id });
            return Ok(res);
        }

        // POST api/devices
        /// <summary>
        /// Adds new Device
        /// </summary>
        /// <param name="nou">new logic Device definition to be added</param>
        /// <returns></returns>
        // <response code="201">Returns newly created item</response>
        // <response code="400">Data error</response>
        // <response code="409">Conflict, already existing item</response>
        [HttpPost]
        [ProducesResponseType(typeof(HistoricData), 201)]
        [ProducesResponseType(typeof(JObject), 400)]
        [ProducesResponseType(typeof(JObject), 409)]
        public IActionResult Post([FromBody] Devices nou) {
            logger.Info("POST Insert new Device");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (nou.DeviceInterval <= 0) {
                return BadRequest(new JObject {["Device interval must be positive"] = nou.DeviceInterval});
            }

            if (!db.AuxDeviceType.Any(dev => nou.IdauxDeviceType == dev.IdauxDeviceType)) {
                return BadRequest(new JObject { ["Device type not found"] = nou.IdauxDeviceType });
            }

            var lastDevice = db.Devices.LastOrDefault();
            if (lastDevice == default(Devices)) nou.Iddevice = 1;
            else nou.Iddevice = lastDevice.Iddevice + 1;
            nou.DeviceCreationDate = DateTime.Now;
            db.Devices.Add(nou);

            try {
                db.SaveChanges();
            } catch (DbUpdateException ex) {
                logger.Warn(ex, ex.Message);
                return DeviceExists(nou.Iddevice) ? StatusCode((int) HttpStatusCode.Conflict, new JObject { ["Device already exists"] = nou.Iddevice }) : BadRequest(nou);
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest(nou);
            }

            logger.Info("Device created correctly");
            return Created($"/api/devices/{nou.Iddevice}", nou);
        }

        // PUT api/devices/5
        /// <summary>
        /// Updates existing Device
        /// </summary>
        /// <param name="id">Device identifier</param>
        /// <param name="nou">Device to be updated</param>
        /// <returns></returns>
        // <response code="204">Device updated</response>
        // <response code="400">Data error</response>
        // <response code="404">Device not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(JObject), 404)]
        [ProducesResponseType(typeof(JObject), 400)]
        public IActionResult Put(int id, [FromBody] Devices nou) { //TODO no permetre canviar Date
            logger.Info($"PUT Update device with id={id}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != nou.Iddevice) return BadRequest(new JObject { ["ID from body different from URL parameter"] = nou.Iddevice });

            var dev = db.Devices.Find(id);
            if (dev == null) return NotFound(new JObject {["Device not found by id:"] = nou.Iddevice});

            Update(dev, nou);

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException ex) {
                logger.Warn(ex, ex.Message);
                if (!DeviceExists(id)) return NotFound(new JObject { ["Device not found"] = "Device could have been deleted" });
                return BadRequest(nou);
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest();
            }
            return NoContent();
        }

        // PATCH api/devices/5
        /// <summary>
        /// Updates some Device information
        /// </summary>
        /// <param name="id">Device identifier</param>
        /// <param name="patch">Device updated information</param>
        /// <returns></returns>
        // <response code="204">Device updated</response>
        // <response code="400">Data error</response>
        // <response code="404">Device not found</response>
        // <response code="403">Update not allowed</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(JObject), 404)]
        [ProducesResponseType(typeof(JObject), 400)]
        [ProducesResponseType(typeof(JObject), 403)]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Devices> patch) {
            logger.Info("PATCH Device");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dev = db.Devices.Find(id);
            if (dev == null) return NotFound(new JObject {["Device not found by id:"] = id});

            foreach (var op in patch.Operations) {
                if (op.OperationType != OperationType.Replace)
                    return BadRequest(new JObject {["Syntax error"] = "op field should be replace"});
                if (string.Equals(op.path, "DeviceCreationDate", StringComparison.OrdinalIgnoreCase))
                    return StatusCode((int) HttpStatusCode.Forbidden,
                        new JObject {["Update error"] = "Device Creation date cannot be modified"});
                if (string.Equals(op.path, "Iddevice", StringComparison.OrdinalIgnoreCase))
                    return StatusCode((int)HttpStatusCode.Forbidden,
                        new JObject { ["Update error"] = "Device id cannot be modified" });
                if (!op.path.StartsWith("/")) op.path = "/" + op.path;
            }

            var patched = db.Devices.Attach(dev);
            patch.ApplyTo(patched.Entity, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException ex) {
                logger.Warn(ex, ex.Message);
                return BadRequest(new JObject {["Device Update error"] = "Concurrency Error"});
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/devices/5
        /// <summary>
        /// Deletes specific Device
        /// </summary>
        /// <param name="id">Device identifier</param>
        /// <returns></returns>
        // <response code="200">Device deleted</response>
        // <response code="400">Data error</response>
        // <response code="404">Device not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(HistoricData), 200)]
        [ProducesResponseType(typeof(JObject), 404)]
        [ProducesResponseType(typeof(JObject), 400)]
        public IActionResult Delete(int id) {
            logger.Info("DELETE Device");
            var res = db.Devices.Find(id);
            if (res == null) return NotFound(new JObject { ["Device not found by id:"] = id });

            db.Devices.Remove(res);

            try {
                db.SaveChanges();
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest();
            }

            logger.Info($"Device with id={id} deleted");
            return Ok(res);
        }

        #region Auxiliar

        private bool DeviceExists(int iddevice) {
            return db.Devices.Any(e => e.Iddevice == iddevice);
        }

        private void Update(Devices old, Devices nou) {
            var updated = db.Devices.Attach(old);
            
            updated.Entity.DeviceName = nou.DeviceName;
            updated.Entity.IdauxDeviceType = nou.IdauxDeviceType;
            updated.Entity.DeviceEnabled = nou.DeviceEnabled;
            updated.Entity.DeviceConnected = nou.DeviceConnected;
            updated.Entity.DeviceNeedLogin = nou.DeviceNeedLogin;
            updated.Entity.DeviceInterval = nou.DeviceInterval;
            updated.Entity.DeviceUsername = nou.DeviceUsername;
            updated.Entity.DevicePassword = nou.DevicePassword;
            updated.Entity.IddeviceProtocol = nou.IddeviceProtocol;
            updated.Entity.DeviceAux = nou.DeviceAux;

        }

        #endregion

        #region Overrides of Controller
        /// <summary>
        /// Releases all resources used by the controller
        /// </summary>
        /// <param name="disposing">Indicates if the database context should be disposed as well</param>
        protected override void Dispose(bool disposing) {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
