using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        NorthwindOriginalContext db = new NorthwindOriginalContext();

        //hakee kaikki tuotteet
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            try
            {
                var tuotteet = db.Products.ToList();
                return Ok(tuotteet);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }
    }
}
