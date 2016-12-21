using System.Threading.Tasks;
using Cloud_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cloud_API.Controllers {
    [Route("[controller]")]
    public class HomeController : Controller {
        //private readonly DatabaseContext db;

        //public HomeController(DatabaseContext context) {
        //    db = context;
        //}

        // GET: /Home
        public ActionResult Home() {
            //var res = await db.Devices.FromSql("spDevices_GetAll").ToArrayAsync();
            //ViewData["Devices"] = res;
            return View();
        }
    }
}
