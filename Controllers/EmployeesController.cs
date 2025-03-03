using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private NorthwindOriginalContext db;

        public EmployeesController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }

        //hakee kaikki työntekijät
        [HttpGet]
        public ActionResult GetAllEmployees()
        {
            try
            {
                var työntekijät = db.Employees.ToList();
                return Ok(työntekijät);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }

        //hakee yhden työntekijän pääavaimella
        [HttpGet("{id}")]
        public ActionResult GetOneEmployeeById(int id)
        {
            try
            {
                var työntekijä = db.Employees.Find(id);
                if (työntekijä != null)
                {
                    return Ok(työntekijä);
                }
                else
                {
                    //return NotFound("Työntekijää id:llä " + id + " ei löydy.");
                    return NotFound($"Työntekijää id:llä {id} ei löydy."); //string interpolation
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }

        //uuden lisääminen
        [HttpPost]
        public ActionResult AddNew([FromBody] Employee emp)
        {
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return Ok("Lisättiin uusi työntekijä: " + emp.LastName + " " + emp.FirstName + ", id:llä " + emp.EmployeeId);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }

        //työntekijän poistaminen
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var työntekijä = db.Employees.Find(id);

                if (työntekijä != null)
                {
                    db.Employees.Remove(työntekijä);
                    db.SaveChanges();
                    return Ok("Työntekijä " + työntekijä.LastName + " " + työntekijä.FirstName + " poistettiin onnistuneesti.");
                }
                else
                {
                    return NotFound("Työntekijää id:llä " + id + " ei löytynyt.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }

        //työntekijän muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditEmployee(int id, [FromBody] Employee employee)
        {
            var emp = db.Employees.Find(id);
            if (emp != null)
            {
                emp.LastName = emp.LastName;
                emp.FirstName = emp.FirstName;
                emp.Title = emp.Title;
                emp.TitleOfCourtesy = emp.TitleOfCourtesy;
                emp.BirthDate = emp.BirthDate;
                emp.HireDate = emp.HireDate;
                emp.Address = emp.Address;
                emp.City = emp.City;
                emp.Region = emp.Region;
                emp.PostalCode = emp.PostalCode;
                emp.Country = emp.Country;
                emp.HomePhone = emp.HomePhone;
                emp.Extension = emp.Extension;
                emp.Notes = emp.Notes;

                db.SaveChanges();
                return Ok("Muokattu työntekijää " + emp.LastName + " " + emp.FirstName);
            }
            else
            {
                return NotFound("Työntekijää ei löytynyt id:llä: " + id);
            }
        }

        //hakee nimen osalla
        [HttpGet("empname/{ename}")]
        public ActionResult GetByName(string ename)
        {
            try
            {
                var emp = db.Employees.Where(e => e.LastName.Contains(ename) || e.FirstName.Contains(ename));
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
