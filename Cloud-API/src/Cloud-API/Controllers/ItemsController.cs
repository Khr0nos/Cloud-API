using System;
using System.Diagnostics;
using System.Linq;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_API.Controllers {
    [Route("api/[controller]")]
    public class ItemsController : Controller {
        private ItemContext _context;

        public ItemsController(ItemContext context) {
            _context = context;
        }

        //GET /api/items
        [HttpGet]
        public IActionResult Get() {
            var items = _context.Items.ToList();
            return Json(items);
        }

        //GET /api/items/id
        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult Get(int id) {
            var res = _context.Items.FirstOrDefault(i => i.ID == id);
            if (res == default(Item)) {
                return NotFound();
            }
            return Json(res);
        }

        //POST api/items
        [HttpPost]
        public IActionResult Post([FromBody]Item item) {
            if (item == null) return BadRequest();
            if (ModelState.IsValid) {
                try {
                    item.Data = DateTime.Now;
                    _context.Items.Add(item);
                    _context.SaveChanges();
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    return BadRequest();
                }
                return Created($"/api/items/{item.ID}", item);
                //return CreatedAtRoute("GetItem", new { id = item.ID }, item);
            }

            return NoContent();
        }

        // PUT api/items/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Item nouItem) {
            if (nouItem == null) return BadRequest();
            var item = _context.Items.FirstOrDefault(i => i.ID == id);
            if (item == default(Item)) return NotFound();
            if (ModelState.IsValid) {
                try {
                    item.Update(nouItem);
                    _context.Items.Update(item);
                    _context.SaveChanges();
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    return BadRequest();
                }
                return NoContent();
            }
            return BadRequest();
        }

        // PATCH api/items/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]Item nouItem) {
            if (nouItem == null) return BadRequest();
            var item = _context.Items.FirstOrDefault(i => i.ID == id);
            if (item == default(Item)) return NotFound();
            if (ModelState.IsValid) {
                try {
                    if (nouItem.Equip != null) item.Equip = nouItem.Equip;
                    if (nouItem.Value != 0) item.Value = nouItem.Value;
                    if (nouItem.Aux != null) item.Aux = nouItem.Aux;
                    item.Data = DateTime.Now;
                    _context.Items.Update(item);
                    _context.SaveChanges();
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    return BadRequest();
                }
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE api/items/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var item = _context.Items.FirstOrDefault(i => i.ID == id);
            if (item == default(Item)) return NotFound();
            if (ModelState.IsValid) {
                _context.Items.Remove(item);
                _context.SaveChanges();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
