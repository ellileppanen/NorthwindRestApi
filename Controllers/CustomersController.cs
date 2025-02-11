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

        //perinteinen tapa
        //NorthwindOriginalContext db = new NorthwindOriginalContext();

        //dependency injektio tapa
        private NorthwindOriginalContext db;

        public CustomersController(NorthwindOriginalContext dbparametri) 
        { 
            db= dbparametri;
        }

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
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
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
        public ActionResult AddNew([FromBody] Customer cust)
        {
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return Ok("Lisättiin uusi asiakas: " + cust.CompanyName + " from:" + cust.Country);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }

        //asiakkaan poistaminen
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try { 
            var asiakas = db.Customers.Find(id);

            if (asiakas != null)
            {
                db.Customers.Remove(asiakas);
                db.SaveChanges();
                return Ok("Asiakas " + asiakas.CompanyName + " poistettiin onnistuneesti.");
            }
            else 
            {
                return NotFound("Asiakasta id:llä " + id + " ei löytynyt.");
            }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }
        //asiakkaan muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditCustomer(string id, [FromBody] Customer customer)
        {
            var asiakas = db.Customers.Find(id);
            if (asiakas != null)
            {
                asiakas.CompanyName = customer.CompanyName;
                asiakas.ContactName = customer.ContactName;
                asiakas.Address = customer.Address;
                asiakas.City = customer.City;
                asiakas.Region = customer.Region;
                asiakas.PostalCode = customer.PostalCode;
                asiakas.Country = customer.Country;
                asiakas.Phone = customer.Phone;
                asiakas.Fax = customer.Fax;

                db.SaveChanges();
                return Ok("Muokattu asiakasta " + asiakas.CompanyName);
            }
            else 
            { 
                return NotFound("Asiakasta ei löytynyt id:llä: " + id);
            }
        }
        //hakee nimen osalla
        [HttpGet("companyname/{cname}")]
        public ActionResult GetByName(string cname)
        {
            try
            {
                var cust = db.Customers.Where(c => c.CompanyName.Contains(cname));
                return Ok(cust);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
