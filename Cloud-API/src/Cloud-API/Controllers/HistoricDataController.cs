using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Cloud_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NLog;

namespace Cloud_API.Controllers {
    /// <summary>
    /// API entry point to manage Historic Data
    /// </summary>
    [Route("api/[controller]")]
    public class historicdataController : Controller {
        private readonly DatabaseContext db;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public historicdataController(DatabaseContext context) {
            db = context;
        }

        // GET: api/historicdata
        /// <summary>
        /// Gets all HistoricData Values
        /// </summary>
        /// <remarks>Returns a JSON array of Historic Data items</remarks>
        /// <returns>Historic Data Collection</returns>
        /// <response code="200">Returns all Historic Data items</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<HistoricData>), 200)]
        public IActionResult Get() {
            logger.Info("GET All Historic Data");
            return Ok(db.HistoricData);     //per defecte retorna JSON
            //return Json(db.HistoricData);
        }

        // GET api/historicdata/5
        /// <summary>
        /// Gets specific HistoricData
        /// </summary>
        /// <param name="id">HistoricData identifier</param>
        /// <returns>Historic Data</returns>
        /// <response code="200">Returns selected Historic Data item</response>
        /// <response code="404">Historic Data item not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(HistoricData),200)]
        [ProducesResponseType(typeof(JObject), 404)]
        public IActionResult Get(int id) {
            logger.Info($"GET Data with id={id}");
            var res = db.HistoricData.Find(id);
            if (res == null) return NotFound(new JObject { ["Data not found by id:"] = id });
            return Ok(res);
        }

        // POST api/historicdata
        /// <summary>
        /// Adds new HistoricData
        /// </summary>
        /// <param name="nou">new HistoricData to be added</param>
        /// <returns></returns>
        /// <response code="201">Returns newly created item</response>
        /// <response code="400">Data error</response>
        /// <response code="409">Conflict, already existing item</response>
        [HttpPost]
        [ProducesResponseType(typeof(HistoricData), 201)]
        [ProducesResponseType(typeof(JObject), 400)]
        [ProducesResponseType(typeof(JObject), 409)]
        public IActionResult Post([FromBody] HistoricData nou) {
            logger.Info("POST Insert new Data");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var device = db.Devices.Find(nou.Iddevice);
            if (device == null) {
                return BadRequest(new JObject {["Device not found"] = nou.Iddevice});
            }

            if (!device.DeviceEnabled) {
                return BadRequest(new JObject { ["Device not enabled"] = nou.Iddevice });
            }

            if (!db.AuxDataType.Any(dev => nou.IddataType == dev.IdauxDataType)) {
                return BadRequest(new JObject { ["Data type not found"] = nou.IddataType});
            }
            
            var lastData = db.HistoricData.LastOrDefault();
            if (lastData == default(HistoricData)) nou.IdhistoricData = 1;
            else nou.IdhistoricData = lastData.IdhistoricData + 1;
            nou.HistDataDate = DateTime.Now;
            db.HistoricData.Add(nou);

            try {
                db.SaveChanges();
            } catch (DbUpdateException ex) {
                logger.Warn(ex, ex.Message);
                return DataExists(nou.IdhistoricData) ? StatusCode((int) HttpStatusCode.Conflict, new JObject { ["Data exists already"] = nou.IdhistoricData}) : BadRequest(nou);
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest(nou);
            }

            if (!device.DeviceConnected) { //nou HistoricData inserit correctament en aquest punt
                ConnectDevice(device); //update nomes DeviceConnected del dispositiu
            }

            //TODO Return OK amb comanda de resposta del server

            logger.Info("Data added correctly");
            return Created($"/api/historicdata/{nou.IdhistoricData}", nou);
        }

        // PUT api/historicdata/5
        /// <summary>
        /// Updates existing HistoricData
        /// </summary>
        /// <param name="id">HistoricData identifier</param>
        /// <param name="nou">HistoricData to be updated</param>
        /// <returns></returns>
        /// <response code="204">Historic Data updated</response>
        /// <response code="400">Data error</response>
        /// <response code="404">Historic Data item not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(JObject), 404)]
        [ProducesResponseType(typeof(JObject), 400)]
        public IActionResult Put(int id, [FromBody] HistoricData nou) {
            logger.Info($"PUT Update data with id={id}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != nou.IdhistoricData) return BadRequest(new JObject { ["ID from body different from URL parameter"] = nou.IdhistoricData });

            var data = db.HistoricData.Find(id);
            if (data == null) return NotFound(new JObject { ["Data not found by id:"] = nou.Iddevice });

            Update(data, nou);

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException ex) {
                logger.Warn(ex, ex.Message);
                if (!DataExists(id)) return NotFound(new JObject { ["Data not found"] = "Data could have been deleted" });
                return BadRequest(nou);
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest(nou);
            }
            return NoContent();
        }

        // PATCH api/historicdata/5
        /// <summary>
        /// Updates some HistoricData information
        /// </summary>
        /// <param name="id">HistoricData identifier</param>
        /// <param name="patch">HistoricData updated information</param>
        /// <returns></returns>
        /// <response code="204">HistoricData updated</response>
        /// <response code="400">Data error</response>
        /// <response code="404">HistoricData not found</response>
        /// <response code="403">Update not allowed</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(JObject), 404)]
        [ProducesResponseType(typeof(JObject), 400)]
        [ProducesResponseType(typeof(JObject), 403)]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<HistoricData> patch) {
            logger.Info("PATCH HistoricData");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dev = db.HistoricData.Find(id);
            if (dev == null) return NotFound(new JObject {["Data not found by id:"] = id});

            foreach (var op in patch.Operations) {
                if (op.OperationType != OperationType.Replace)
                    return BadRequest(new JObject {["Syntax error"] = "op field should be replace"});
                if (string.Equals(op.path, "HistDataDate", StringComparison.OrdinalIgnoreCase))
                    return StatusCode((int) HttpStatusCode.Forbidden,
                        new JObject {["Update error"] = "Data date cannot be modified"});
                if (string.Equals(op.path, "IdhistoricData", StringComparison.OrdinalIgnoreCase))
                    return StatusCode((int)HttpStatusCode.Forbidden,
                        new JObject { ["Update error"] = "Data id cannot be modified" });
                if (!op.path.StartsWith("/")) op.path = "/" + op.path;
            }

            var patched = db.HistoricData.Attach(dev);
            patch.ApplyTo(patched.Entity, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException ex) {
                logger.Warn(ex, ex.Message);
                return BadRequest(new JObject {["Data Update error"] = "Concurrency Error"});
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/historicdata/5
        /// <summary>
        /// Deletes specific HistoricData
        /// </summary>
        /// <param name="id">HistoricData identifier</param>
        /// <returns></returns>
        /// <response code="200">Historic Data item deleted</response>
        /// <response code="404">Historic Data item not found</response>
        /// <response code="400">Data error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(HistoricData), 200)]
        [ProducesResponseType(typeof(JObject), 404)]
        [ProducesResponseType(typeof(JObject), 400)]
        public IActionResult Delete(int id) {
            logger.Info("DELETE Data");
            var res = db.HistoricData.Find(id);
            if (res == null) return NotFound(new JObject { ["Data not found by id:"] = id });

            db.HistoricData.Remove(res);

            try {
                db.SaveChanges();
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest();
            }

            logger.Info($"Data with id={id} deleted");
            return Ok(res);
        }

        #region Auxiliar

        private bool DataExists(int iddata) {
            return db.HistoricData.Any(e => e.IdhistoricData == iddata);
        }

        private void ConnectDevice(Devices device) {
            logger.Info("Registering enabled device as connected");
            //device.DeviceConnected = true;
            //db.Devices.Update(device);
            var dev = db.Devices.Attach(device);
            dev.Entity.DeviceConnected = true;

            var histdev = new HistoricDevices {
                Iddevice = device.Iddevice,
                HistDeviceDate = DateTime.Now,
                IddeviceAction = 1,
                HistDeviceIpaddress = null,
                HistDeviceAux = null
            };

            var lastHistoricDevice = db.HistoricDevices.LastOrDefault();
            if (lastHistoricDevice == default(HistoricDevices)) histdev.IdhistoricDevices = 1;
            else histdev.IdhistoricDevices = lastHistoricDevice.IdhistoricDevices + 1;
            db.HistoricDevices.Add(histdev);

            try {
                db.SaveChanges();
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex, "Error registering device as connected");
            }
            logger.Info("Enabled device registered as connected");
        }

        private void Update(HistoricData old, HistoricData nou) {
            var updated = db.HistoricData.Attach(old);

            updated.Entity.Iddevice = nou.Iddevice;
            updated.Entity.HistDataValue = nou.HistDataValue;
            updated.Entity.IddataType = nou.IddataType;
            updated.Entity.HistDataToDevice = nou.HistDataToDevice;
            updated.Entity.HistDataAck = nou.HistDataAck;
            updated.Entity.HistDataAux = nou.HistDataAux;
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
