using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //alustetaan tietokantayhteys
        NorthwindOriginalContext db = new NorthwindOriginalContext();

        //hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetAllCustomers()
        { 
            var asiakkaat = db.Customers.ToList();
            return Ok(asiakkaat);
        }
    }
}
