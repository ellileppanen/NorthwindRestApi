using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private NorthwindOriginalContext db;

        public ProductsController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }

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
        //hakee yhden tuotteen pääavaimella
        [HttpGet("{id}")]
        public ActionResult GetOneProductsById(int id)
        {
            try
            {
                var tuote = db.Products.Find(id);
                if (tuote != null)
                {
                    return Ok(tuote);
                }
                else
                {
                    //return NotFound("Tuotetta id:llä " + id + " ei löydy.");
                    return NotFound($"Tuotetta id:llä {id} ei löydy."); //string interpolation
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }
        //uuden lisääminen
        [HttpPost]
        public ActionResult AddNew([FromBody] Product prod)
        {
            try
            {
                db.Products.Add(prod);
                db.SaveChanges();
                return Ok("Lisättiin uusi tuote: " + prod.ProductName + "id:llä" + prod.ProductId);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }
        //tuotteen poistaminen
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var tuote = db.Products.Find(id);

                if (tuote != null)
                {
                    db.Products.Remove(tuote);
                    db.SaveChanges();
                    return Ok("Tuote " + tuote.ProductName + " poistettiin onnistuneesti.");
                }
                else
                {
                    return NotFound("Tuotetta id:llä " + id + " ei löytynyt.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }
        //tuotteen muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditCustomer(int id, [FromBody] Product product)
        {
            var tuote = db.Products.Find(id);
            if (tuote != null)
            {
                tuote.ProductName = tuote.ProductName;
                tuote.SupplierId = tuote.SupplierId;
                tuote.CategoryId = tuote.CategoryId;
                tuote.QuantityPerUnit = tuote.QuantityPerUnit;
                tuote.UnitPrice = tuote.UnitPrice;
                tuote.UnitsInStock = tuote.UnitsInStock;
                tuote.UnitsOnOrder = tuote.UnitsOnOrder;
                tuote.ReorderLevel = tuote.ReorderLevel;
                tuote.Discontinued = tuote.Discontinued;

                db.SaveChanges();
                return Ok("Muokattu tuotetta " + tuote.ProductName);
            }
            else
            {
                return NotFound("Tuotetta ei löytynyt id:llä: " + id);
            }
        }
        //hakee nimen osalla
        [HttpGet("productname/{pname}")]
        public ActionResult GetByName(string pname)
        {
            try
            {
                var prod = db.Products.Where(p => p.ProductName.Contains(pname));
                return Ok(prod);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
