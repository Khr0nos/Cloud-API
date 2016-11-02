using System;
using System.Collections.Generic;
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
            return Ok();
        }

        //POST api/items
        [HttpPost(Name = "SetItem")]
        public IActionResult Post([FromBody]Item item) {
            if (item == null) return BadRequest();
            if (ModelState.IsValid) {
                try {
                    _context.Items.Add(item);
                    _context.SaveChanges();
                } catch (Exception) {
                }
                return CreatedAtRoute("GetItem", new {id = item.Key}, item);
            }

            return NoContent();
        }
    }
}
