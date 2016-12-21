﻿using System;
using System.Linq;
using System.Net;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Cloud_API.Controllers {
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
        /// <returns>Historic Data Collection</returns>
        [HttpGet]
        public ActionResult Get() {
            logger.Info("GET All Historic Data");
            return Json(db.HistoricData);
        }

        // GET api/historicdata/5
        /// <summary>
        /// Gets specific HistoricData
        /// </summary>
        /// <param name="id">HistoricData identifier</param>
        /// <returns>Historic Data</returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            logger.Info($"GET Data with id={id}");
            var res = db.HistoricData.Find(id);
            if (res == null) return NotFound();
            return Json(res);
        }

        // POST api/historicdata
        /// <summary>
        /// Adds new HistoricData
        /// </summary>
        /// <param name="nou">new HistoricData to be added</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] HistoricData nou) {
            logger.Info("POST Insert new Data");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.HistoricData.Add(nou);

            try {
                db.SaveChanges();
            } catch (DbUpdateException ex) {
                logger.Warn(ex, ex.Message);
                if (DataExists(nou.IdhistoricData)) {
                    return StatusCode((int) HttpStatusCode.Conflict, nou);
                }
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
                return BadRequest(nou);
            }

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
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] HistoricData nou) {
            logger.Info($"PUT Update data with id={id}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != nou.IdhistoricData) return BadRequest(nou);

            db.HistoricData.Update(nou);

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException ex) {
                logger.Warn(ex, ex.Message);
                if (!DataExists(id)) return NotFound();
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
            }
            return NoContent();
        }

        // DELETE api/historicdata/5
        /// <summary>
        /// Deletes specific HistoricData
        /// </summary>
        /// <param name="id">HistoricData identifier</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            logger.Info("DELETE Data");
            var res = db.HistoricData.Find(id);
            if (res == null) return NotFound();

            db.HistoricData.Remove(res);

            try {
                db.SaveChanges();
            } catch (Exception ex) {
                logger.Trace(ex);
                logger.Error(ex.Message);
            }

            logger.Info($"Data with id={id} deleted");
            return Ok(res);
        }

        #region Auxiliar

        private bool DataExists(int iddata) {
            return db.HistoricData.Any(e => e.IdhistoricData == iddata);
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