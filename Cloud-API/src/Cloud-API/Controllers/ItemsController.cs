using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_API.Controllers {
    [Route("api/items")]
    public class ItemsController : Controller {
        private ItemContext _context;

        public ItemsController(ItemContext context) {
            _context = context;
        }

        //GET /api/items
        [HttpGet]
        public IActionResult Get() {
            var items = _context.Items.ToList();
            return new JsonResult(items);
        }

        //GET /api/items/id
        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult Get(int id) {
            var res = _context.Items.FirstOrDefault(i => i.Key == id);
            //var res = _context.Items.ToList().Find(i => i.Key == int.Parse(id));
            if (res == default(Item)) {
                return NotFound();
            }
            return new JsonResult(res);
        }

        //POST api/items
        [HttpPost(Name = "AddItem")]
        public IActionResult Post([FromBody]Item item) {
            if (item == null) return BadRequest();
            if (ModelState.IsValid) {
                try {
                    _context.Items.Add(item);
                    _context.SaveChanges();
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    return BadRequest();
                }
                return CreatedAtRoute("GetItem", new {id = item.Key}, item);
            }

            return NoContent();
        }

        // PUT api/items/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            var item = _context.Items.FirstOrDefault(i => i.Key == id);
            if (item == default(Item)) return NotFound();
            if (ModelState.IsValid) {
                item.Name = value;
                _context.Items.Update(item);
                return Ok();
            }
            return BadRequest();
        }

        // PATCH api/items/id
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] string value) { }

        // DELETE api/items/id
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}
