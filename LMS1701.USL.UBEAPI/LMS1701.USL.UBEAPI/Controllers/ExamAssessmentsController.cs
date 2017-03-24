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
    public class ExamAssessmentsController : ApiController
    {
        private UserScoresLoginEntities db = new UserScoresLoginEntities();

        // GET: api/ExamAssessments
        public IQueryable<ExamAssessment> GetExamAssessments()
        {
            return db.ExamAssessments;
        }

        // GET: api/ExamAssessments/5
        [ResponseType(typeof(ExamAssessment))]
        public async Task<IHttpActionResult> GetExamAssessment(int id)
        {
            ExamAssessment examAssessment = await db.ExamAssessments.FindAsync(id);
            if (examAssessment == null)
            {
                return NotFound();
            }

            return Ok(examAssessment);
        }

        // PUT: api/ExamAssessments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExamAssessment(int id, ExamAssessment examAssessment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != examAssessment.ExamAssessmentID)
            {
                return BadRequest();
            }

            db.Entry(examAssessment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamAssessmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ExamAssessments
        [ResponseType(typeof(ExamAssessment))]
        public async Task<IHttpActionResult> PostExamAssessment(ExamAssessment examAssessment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamAssessments.Add(examAssessment);
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