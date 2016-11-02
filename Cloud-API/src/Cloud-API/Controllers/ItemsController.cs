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

        //GET /api/items/2
        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult Get(string id) {
            var res = _context.Items.FirstOrDefault(i => i.Key == int.Parse(id));
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

        // PUT api/items/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // PATCH api/items/5
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] string value) { }

        // DELETE api/items/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}
