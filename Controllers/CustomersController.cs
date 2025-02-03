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
            try
            {
                var asiakkaat = db.Customers.ToList();
                return Ok(asiakkaat);
            }
            catch (Exception ex) 
            { 
                return BadRequest("Tapahtui virhe. Lue lisää: " +ex.InnerException);
            }
        }

        //hakee yhden asiakkaan pääavaimella
        [HttpGet("{id}")]
        public ActionResult GetOneCustomerById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null)
                {
                    return Ok(asiakas);
                }
                else
                {
                    //return NotFound("Asiakasta id:llä " + id + " ei löydy.");
                    return NotFound($"Asiakasta id:llä {id} ei löydy."); //string interpolation
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }

        //uuden lisääminen
        [HttpPost]
        public ActionResult AddNew([FromBody]Customer cust)
        {
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return Ok("Lisättiin uusi asiakas: "+ cust.CompanyName + " from:" + cust.Country);
            }
            catch (Exception ex) 
            { 
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }

    }
}
