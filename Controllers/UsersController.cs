using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly NorthwindOriginalContext db = new NorthwindOriginalContext();

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

    }
}
