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
using System.Web.Http.Cors;


namespace LMS1701.USL.UBEAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        [ResponseType(typeof(User))]
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
        [Route("api/Users/CheckUser")]
        [ResponseType(typeof(User))]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> CheckUser(string email, string password)
        {
            User dbUser = await db.Users.FirstOrDefaultAsync(u => u.email == email && u.password == password);

            if (dbUser == null)
            {
                return BadRequest("User not found");
            }

            Models.User usr = AutoMapper.Mapper.Map<Models.User>(dbUser);

            return Ok(usr);
        }

        private  Models.UserGradebook GetGradebook(string email)
        {
            Models.UserGradebook usergrades = new Models.UserGradebook();
            var user = db.Users.Where(x => x.email == email).ToList().FirstOrDefault();
            var batchIDList =  db.Rosters.Where(x => x.UserID == user.UserPK).Select(y => y.BatchID).ToList();
            List<Batch> batches = new List<Batch>();

            foreach (var id in batchIDList)
            {
                batches.Add(db.Batches.Where(x => x.BatchPK == id).FirstOrDefault());
            }
            var examlist =  db.ExamAssessments.Where(x => x.UserID == user.UserPK).ToList();

            foreach (var batch in batches)
            {
                bool examInBatch = false;
                foreach (var exam in examlist)
                {
                    usergrades.gradebook.Add(AutoMapper.Mapper.Map<Models.ExamAssessment>(exam));

                    if (examInBatch == false)
                    {
                        foreach (var setting in batch.ExamSettings.ToList())
                        {
                            if (exam.ExamSetting.ExamSettingsID == setting.ExamSettingsID)
                            {
                                usergrades.Batches.Add(usergrades.gradebook.Count - 1, batch.Name);
                                examInBatch = true;
                            }
                        }
                    }
                }
                if (examInBatch == false)
                {
                    usergrades.Batches.Add(usergrades.gradebook.Count - 1, "No Batch");
                }
            }

            usergrades.user = AutoMapper.Mapper.Map<Models.User>(user);

            return usergrades;
        }

        [Route("api/Users/GetAllUserGradebooks")]
        [ResponseType(typeof(List<Models.UserGradebook>))]

        public async Task<IHttpActionResult> GetAllUserGradebook()
        {
            List<Models.UserGradebook> AllGradeBooks = new List<Models.UserGradebook>();
            List<string> EmailList = db.Users.Select(x => x.email).ToList();

            foreach(string email in EmailList)
            {
                AllGradeBooks.Add(GetGradebook(email));
            }
            return Ok(AllGradeBooks);
        }

        [Route("api/Users/GetUserGradebook")]
        [ResponseType(typeof(Models.UserGradebook))]

        public async Task<IHttpActionResult> GetUserGradebook(string email)
        {          
            return Ok(GetGradebook(email));
        }
        //GET
        [Route("api/Users/GetUser")]
        [ResponseType(typeof(User))]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetUser(string email)
        {
            List<User> users = await db.Users.Where(x=>x.email==email).ToListAsync();

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