using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_API.Controllers {
    [Route("api/values")]
    public class ValuesController : Controller {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get() {
            return new[] {"Hello", "world"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return (id*2).ToString();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Item value) {
            return NoContent();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {}

        // PATCH api/values/5
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] string value) {}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {}
    }
}
