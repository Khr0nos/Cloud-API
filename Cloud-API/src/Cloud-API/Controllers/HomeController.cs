﻿using System.Threading.Tasks;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cloud_API.Controllers {
    [Route("Home")]
    public class HomeController : Controller {
        private readonly DevicesContext dbcontext;

        public HomeController(DevicesContext context) {
            dbcontext = context;
        }

        // GET: /Home
        public async Task<ActionResult> Home() {
            var res = await dbcontext.Devices.FromSql("spDevices_GetAll").ToArrayAsync();
            ViewData["Devices"] = res;
            return View();
        }
    }
}
