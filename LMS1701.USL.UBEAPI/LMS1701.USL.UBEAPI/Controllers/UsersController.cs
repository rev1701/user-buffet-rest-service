using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LMS1701.USL.UBEAPI.DAL;

namespace LMS1701.USL.UBEAPI.Controllers
{
    public class UsersController : ApiController
    {
        private UserScoresLoginEntities db = new UserScoresLoginEntities();

        // GET: api/Users
        public List<Models.User> GetUsers()
        {
            List<Models.User> test = new List<Models.User>();
            foreach (DAL.User usr in db.Users.ToList())
            {
                test.Add(AutoMapper.Mapper.Map<Models.User>(usr));
            }

            return test;
        }

        // GET
        [Route("api/Users/GetUser")]
        [ResponseType(typeof( User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            Models.User usr = AutoMapper.Mapper.Map<Models.User>(user);

            return Ok(usr);
        }

        //GET
        [Route("api/Users/GetUser")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(string email)
        {
            List<User> users = await db.Users.ToListAsync();

            User user = null;

            foreach (User usrObj in users)
            {
                if (usrObj.email == email)
                {
                    user = usrObj;
                    break;
                }

            }

            if (user == null)
            {
                return NotFound();
            }

            Models.User usr = AutoMapper.Mapper.Map<Models.User>(user);

            return Ok(usr);
        }

        // PUT
        [ResponseType(typeof(void))]
        [Route("api/Users/EditUser")]
        public async Task<IHttpActionResult> PutUser(string email, Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User saveUser = null;

            try
            {
                saveUser = await db.Users.SingleAsync(u => u.email == user.email);
            }
            catch (Exception e)
            {
                return NotFound();
            }

            User tmpUser = AutoMapper.Mapper.Map<User>(user);

            tmpUser.UserPK = saveUser.UserPK;

            db.Users.Attach(saveUser);

            saveUser = tmpUser;

            db.Entry(saveUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST
        [ResponseType(typeof(User))]
        [Route("api/Users/CreateUser")]
        public async Task<IHttpActionResult> PostUser(Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User dbuser = AutoMapper.Mapper.Map<User>(user);

            dbuser = db.Users.Add(dbuser);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dbuser.UserPK }, user);
        }

        // DELETE
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserPK == id) > 0;
        }

        private bool UserExists(string email)
        {
            return db.Users.Count(e => e.email == email) > 0;
        }
    }
}