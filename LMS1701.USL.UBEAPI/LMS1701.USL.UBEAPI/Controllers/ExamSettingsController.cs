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
    public class ExamSettingsController : ApiController
    {
        private UserScoresLoginEntities db = new UserScoresLoginEntities();

        // GET: api/ExamSettings
        public IQueryable<ExamSetting> GetExamSettings()
        {
            return db.ExamSettings;
        }

        // GET: api/ExamSettings/5
        [ResponseType(typeof(ExamSetting))]
        public async Task<IHttpActionResult> GetExamSetting(int id)
        {
            ExamSetting examSetting = await db.ExamSettings.FindAsync(id);
            if (examSetting == null)
            {
                return NotFound();
            }

            return Ok(examSetting);
        }

        // PUT: api/ExamSettings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExamSetting(int id, ExamSetting examSetting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != examSetting.ExamSettingsID)
            {
                return BadRequest();
            }

            db.Entry(examSetting).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamSettingExists(id))
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

        // POST: api/ExamSettings
        [ResponseType(typeof(ExamSetting))]
        public async Task<IHttpActionResult> PostExamSetting(ExamSetting examSetting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamSettings.Add(examSetting);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = examSetting.ExamSettingsID }, examSetting);
        }

        // DELETE: api/ExamSettings/5
        [ResponseType(typeof(ExamSetting))]
        public async Task<IHttpActionResult> DeleteExamSetting(int id)
        {
            ExamSetting examSetting = await db.ExamSettings.FindAsync(id);
            if (examSetting == null)
            {
                return NotFound();
            }

            db.ExamSettings.Remove(examSetting);
            await db.SaveChangesAsync();

            return Ok(examSetting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamSettingExists(int id)
        {
            return db.ExamSettings.Count(e => e.ExamSettingsID == id) > 0;
        }
    }
}