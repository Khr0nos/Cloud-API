using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CloudAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NLog;

namespace CloudAPI.Controllers {
    /// <summary>
    /// API entry point to manage Historic Data. Data controller of the API
    /// </summary>
    /// <remarks>This controllers returns response data formatted in JSON as default</remarks>
    [Route("api/[controller]")]
    public class historicdataController : Controller {
        /// <summary>
        /// Represents the connection with the underlying database. Uses EntityFrameworkCore
        /// </summary>
        private readonly DatabaseContext db;
        /// <summary>
        /// Provides a logging interface for this controller of the API
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Default constructor of the controller
        /// </summary>
        /// <remarks>This constructor shouldn't be used directly on code. Called by the runtime</remarks>
        /// <param name="context">Context connection of the database</param>
        public historicdataController(DatabaseContext context) {
            db = context;
        }

        // GET: api/historicdata
        /// <summary>
        /// Gets all HistoricData Values
        /// </summary>
        /// <remarks>Returns a JSON array of Historic Data items</remarks>
        /// <returns>
        /// One of the following cases
        /// <list type="table">
        /// <listheader>
        /// <term>Response code</term>
        /// <term>Returned value</term>
        /// </listheader>
        /// <item>
        /// <term><b>200 OK</b></term>
        /// <term>Historic Data Collection</term>
        /// </item>
        /// </list>
        /// </returns>
        // <response code="200">Returns all Historic Data items</response>
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
        /// <returns>
        /// One of the following cases
        /// <list type="table">
        /// <listheader>
        /// <term>Response code</term>
        /// <term>Returned value</term>
        /// </listheader>
        /// <item>
        /// <term><b>200 OK</b></term>
        /// <term>Historic Data</term>
        /// </item>
        /// <item>
        /// <term><b>404 Not Found</b></term>
        /// <term>Error information</term>
        /// </item>
        /// </list>
        /// </returns>
        // <response code="200">Returns selected Historic Data item</response>
        // <response code="404">Historic Data item not found</response>
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
        /// <returns>
        /// One of the following cases
        /// <list type="table">
        /// <listheader>
        /// <term>Response code</term>
        /// <term>Returned value</term>
        /// </listheader>
        /// <item>
        /// <term><b>201 Created</b></term>
        /// <term>Newly created Data</term>
        /// </item>
        /// <item>
        /// <term><b>400 Bad Request</b></term>
        /// <term>Error information</term>
        /// </item>
        /// <item>
        /// <term><b>409 Conflict</b></term>
        /// <term>Error information</term>
        /// </item>
        /// </list>
        /// </returns>
        // <response code="201">Returns newly created item</response>
        // <response code="400">Data error</response>
        // <response code="409">Conflict, already existing item</response>
        [HttpPost]
        [ProducesResponseType(typeof(JObject), 201)]
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

            logger.Info("Data added correctly");
            var ok = new JObject(new JProperty("Inserted data", JObject.FromObject(nou)),
                new JProperty("Interval", device.DeviceInterval));
            return Created($"/api/historicdata/{nou.IdhistoricData}", ok);
        }

        // PUT api/historicdata/5
        /// <summary>
        /// Updates existing HistoricData
        /// </summary>
        /// <param name="id">HistoricData identifier</param>
        /// <param name="nou">HistoricData to be updated</param>
        /// <returns>
        /// One of the following cases
        /// <list type="table">
        /// <listheader>
        /// <term>Response code</term>
        /// <term>Returned value</term>
        /// </listheader>
        /// <item>
        /// <term><b>204 No Content</b></term>
        /// <term>Nothing</term>
        /// </item>
        /// <item>
        /// <term><b>400 Bad Request</b></term>
        /// <term>Error information</term>
        /// </item>
        /// <item>
        /// <term><b>404 Not Found</b></term>
        /// <term>Error information</term>
        /// </item>
        /// </list>
        /// </returns>
        // <response code="204">Historic Data updated</response>
        // <response code="400">Data error</response>
        // <response code="404">Historic Data item not found</response>
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
        /// <returns>
        /// One of the following cases
        /// <list type="table">
        /// <listheader>
        /// <term>Response code</term>
        /// <term>Returned value</term>
        /// </listheader>
        /// <item>
        /// <term><b>204 No Content</b></term>
        /// <term>Nothing</term>
        /// </item>
        /// <item>
        /// <term><b>400 Bad Request</b></term>
        /// <term>Error information</term>
        /// </item>
        /// <item>
        /// <term><b>409 Conflict</b></term>
        /// <term>Error information</term>
        /// </item>
        /// </list>
        /// </returns>
        // <response code="204">HistoricData updated</response>
        // <response code="400">Data error</response>
        // <response code="404">HistoricData not found</response>
        // <response code="403">Update not allowed</response>
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

                var path = op.path;
                if (path.StartsWith("/")) path = path.Substring(1);

                if (string.Equals(path, "HistDataDate", StringComparison.OrdinalIgnoreCase))
                    return StatusCode((int) HttpStatusCode.Forbidden,
                        new JObject {["Update error"] = "Data date cannot be modified"});

                if (string.Equals(path, "IdhistoricData", StringComparison.OrdinalIgnoreCase))
                    return StatusCode((int)HttpStatusCode.Forbidden,
                        new JObject { ["Update error"] = "Data id cannot be modified" });

                if (!Validate(op)) return BadRequest(new JObject {["Update data error"] = "Incorrect value"});

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
        /// <returns>
        /// One of the following cases
        /// <list type="table">
        /// <listheader>
        /// <term>Response code</term>
        /// <term>Returned value</term>
        /// </listheader>
        /// <item>
        /// <term><b>200 OK</b></term>
        /// <term>Deleted Data</term>
        /// </item>
        /// <item>
        /// <term><b>400 Bad Request</b></term>
        /// <term>Error information</term>
        /// </item>
        /// <item>
        /// <term><b>404 Not Found</b></term>
        /// <term>Error information</term>
        /// </item>
        /// </list>
        /// </returns>
        // <response code="200">Historic Data item deleted</response>
        // <response code="404">Historic Data item not found</response>
        // <response code="400">Data error</response>
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
        /// <summary>
        /// Checks if some data already exists
        /// </summary>
        /// <param name="iddata">data identifier</param>
        /// <returns>True if data already exists, False otherwise</returns>
        private bool DataExists(int iddata) {
            return db.HistoricData.Any(e => e.IdhistoricData == iddata);
        }
        /// <summary>
        /// Register device as connected
        /// </summary>
        /// <param name="device">device to be updated</param>
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
        /// <summary>
        /// Update all HistoricData info
        /// </summary>
        /// <remarks>This method updates all data info except protected fields that shouldn't be modified</remarks>
        /// <param name="old">Old HistoricData info to be updated</param>
        /// <param name="nou">New HistoricData info</param>
        private void Update(HistoricData old, HistoricData nou) {
            var updated = db.HistoricData.Attach(old);

            updated.Entity.Iddevice = nou.Iddevice;
            updated.Entity.HistDataValue = nou.HistDataValue;
            updated.Entity.IddataType = nou.IddataType;
            updated.Entity.HistDataToDevice = nou.HistDataToDevice;
            updated.Entity.HistDataAck = nou.HistDataAck;
            updated.Entity.HistDataAux = nou.HistDataAux;
        }
        /// <summary>
        /// Validate Data for Patch requests
        /// </summary>
        /// <param name="op">Patch Data to be validated</param>
        /// <returns></returns>
        private bool Validate(Operation<HistoricData> op) { //TODO validacio a la interficie de client, preferiblement
            if (string.Equals(op.path, "iddevice", StringComparison.OrdinalIgnoreCase)) {
                if (op.value is int == false) return false;
                if ((int) op.value <= 0) return false;
            }
            if (string.Equals(op.path, "iddatatype", StringComparison.OrdinalIgnoreCase)) {
                if (op.value is int == false) return false;
                if ((int)op.value <= 0) return false;
            }
            if (string.Equals(op.path, "histdatatodevice", StringComparison.OrdinalIgnoreCase)) {
                if (op.value is bool == false) return false;
            }
            if (string.Equals(op.path, "histdataack", StringComparison.OrdinalIgnoreCase)) {
                if (op.value is bool == false) return false;
            }
            return true;
        }

        #endregion

        #region Overrides of Controller
        // <summary>
        // Releases all resources used by the controller
        // </summary>
        // <param name="disposing">Indicates if the database context should be disposed as well</param>
        protected override void Dispose(bool disposing) {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
