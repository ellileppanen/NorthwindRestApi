using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private NorthwindOriginalContext db;

        public UsersController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }

        [HttpGet]
        public ActionResult GetAll() 
        {
            var users = db.Users;

            foreach(var user in users) 
            { 
                user.Password = null;
            }
            return Ok(users);

        }

        //uuden lisääminen
        [HttpPost]
        public ActionResult PostCreateNew([FromBody] User u)
        {
            try
            {
                db.Users.Add(u);
                db.SaveChanges();
                return Ok("Lisättin käyttäjä " + u.UserName);
            }
            catch (Exception e)
            {
                return BadRequest("Lisääminen ei onnistunut. Tässä lisää tietoa: " + e);
            }
        }

        //muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditUser(int id, [FromBody] User user)
        {
            var käyttäjä = db.Users.Find(id);
            if (käyttäjä != null)
            {
                käyttäjä.FirstName = user.FirstName;
                käyttäjä.LastName = user.LastName;
                käyttäjä.Email = user.Email;
                käyttäjä.UserName = user.UserName;
                käyttäjä.Password = user.Password;
                käyttäjä.AccesslevelId = user.AccesslevelId;


                db.SaveChanges();
                return Ok("Muokattu käyttäjää " + käyttäjä.UserName);
            }
            else
            {
                return NotFound("Käyttäjää ei löytynyt id:llä: " + id);
            }
        }

        //käyttäjän poistaminen
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var käyttäjä = db.Users.Find(id);

                if (käyttäjä != null)
                {
                    db.Users.Remove(käyttäjä);
                    db.SaveChanges();
                    return Ok("Käyttäjä " + käyttäjä.LastName + " " + käyttäjä.FirstName + " poistettiin onnistuneesti.");
                }
                else
                {
                    return NotFound("Käyttäjää id:llä " + id + " ei löytynyt.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }

    }
}
