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
    public class ExamAssessmentsController : ApiController
    {
        private UserScoresLoginEntities db = new UserScoresLoginEntities();

        // GET: api/ExamAssessments
        public List<Models.ExamAssessment> GetExamAssessments()
        {
            List<Models.ExamAssessment> tmpExams = new List<Models.ExamAssessment>();
            foreach (DAL.ExamAssessment exm in db.ExamAssessments.ToArray())
            {
                tmpExams.Add(AutoMapper.Mapper.Map<Models.ExamAssessment>(exm));
            }

            return tmpExams;
        }

        // GET: api/ExamAssessments/5
        [Route("api/ExamAssessments/GetUserExams")]
        [ResponseType(typeof(ExamAssessment))]
        public async Task<IHttpActionResult> GetExamAssessments(string email)
        {

            var uExams = await db.ExamAssessments.ToArrayAsync();
            List<Models.ExamAssessment> userExams = new List<Models.ExamAssessment>();
            
            foreach (ExamAssessment eAssess in uExams)
            {
                if(eAssess.User.email == email)
                {
                    userExams.Add(AutoMapper.Mapper.Map<Models.ExamAssessment>(eAssess));
                }
            }

            if(userExams.Count() < 1)
            {
                return NotFound();
            }
                                                          
            return Ok(userExams);
        }


        [Route("api/ExamAssessments/GetExam")]
        [ResponseType(typeof(ExamAssessment))]
        public async Task<IHttpActionResult> GetExamAssessment(string email, int examSettingsId)
        {

            int userId = -1;

            foreach(User tmpUsr in await db.Users.ToArrayAsync())
            {
                if(tmpUsr.email == email)
                {
                    userId = tmpUsr.UserPK;
                }

                break;
            }

            Models.ExamAssessment mdlExamAssessment = null;

            foreach(ExamAssessment tmpExam in await db.ExamAssessments.ToArrayAsync())
            {

                if(tmpExam.UserID == userId && tmpExam.SettingsID == examSettingsId)
                {
                    mdlExamAssessment = AutoMapper.Mapper.Map<Models.ExamAssessment>(tmpExam);
                }

            }

            if (mdlExamAssessment == null)
                return NotFound();
            
            return Ok(mdlExamAssessment);
        }

        // PUT: api/ExamAssessments/5
        [Route("api/ExamAssessments/Edit")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExamAssessment(string email, Models.ExamAssessment mdlExamAssessment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamAssessment saveExamAssessment = null;

            try
            {
                saveExamAssessment = await db.ExamAssessments.SingleAsync(e => e.User.email == email);
            }
            catch (Exception e)
            {
                return NotFound();
            }

            ExamAssessment tmpExamAssessment = AutoMapper.Mapper.Map<ExamAssessment>(mdlExamAssessment);

            tmpExamAssessment.ExamAssessmentID = saveExamAssessment.ExamAssessmentID;

            db.ExamAssessments.Attach(saveExamAssessment);

            saveExamAssessment = tmpExamAssessment;

            db.Entry(saveExamAssessment).State = EntityState.Modified;

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

        // POST: api/ExamAssessments
        [Route("api/ExamAssessments/SaveExam")]
        [ResponseType(typeof(ExamAssessment))]
        public async Task<IHttpActionResult> PostExamAssessment(Models.ExamAssessment mdlExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamAssessment examAssessment = AutoMapper.Mapper.Map<ExamAssessment>(mdlExam);
            examAssessment = db.ExamAssessments.Add(examAssessment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = examAssessment.ExamAssessmentID }, examAssessment);
        }

        // DELETE: api/ExamAssessments/5
        [ResponseType(typeof(ExamAssessment))]
        public async Task<IHttpActionResult> DeleteExamAssessment(int id)
        {
            ExamAssessment examAssessment = await db.ExamAssessments.FindAsync(id);
            if (examAssessment == null)
            {
                return NotFound();
            }

            db.ExamAssessments.Remove(examAssessment);
            await db.SaveChangesAsync();

            return Ok(examAssessment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamAssessmentExists(int id)
        {
            return db.ExamAssessments.Count(e => e.ExamAssessmentID == id) > 0;
        }
    }
}