﻿using System;
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
    public class ExamSettingsController : ApiController
    {
        private UserScoresLoginEntities db = new UserScoresLoginEntities();

        // GET: api/ExamSettings
        public List<Models.ExamSetting> GetExamSettings()
        {
            List<Models.ExamSetting> mdlExams = new List<Models.ExamSetting>();

            foreach (DAL.ExamSetting eStng in db.ExamSettings.ToList())
            {
                mdlExams.Add(AutoMapper.Mapper.Map<Models.ExamSetting>(eStng));
            }

            return mdlExams;
        }

        // GET: api/ExamSettings
        [Route("api/ExamSettings/GetSettings")]
        [ResponseType(typeof(List<KeyValuePair<int, string>>))]
        public List<KeyValuePair<int, string>> GetExamIDs()
        {
            List<ExamSetting> dbExams = db.ExamSettings.ToList();

            List<KeyValuePair<int, string>> examsInfo = new List<KeyValuePair<int, string>>();

            foreach (ExamSetting exm in dbExams)
            {
                examsInfo.Add(new KeyValuePair<int, string>(exm.ExamSettingsID, exm.ExamTemplateID));
            }

            return examsInfo;
        }

        // GET
        [Route("api/ExamSettings/GetSetting")]
        [ResponseType(typeof(ExamSetting))]
        public async Task<IHttpActionResult> GetExamSetting(int id)
        {
            ExamSetting examSetting = await db.ExamSettings.FindAsync(id);
            if (examSetting == null)
            {
                return NotFound();
            }

            Models.ExamSetting mdlExam = AutoMapper.Mapper.Map<Models.ExamSetting>(examSetting);

            return Ok(mdlExam);
        }
        

        // PUT
        [Route("api/ExamSettings/ModifySettings")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExamSetting(int id, Models.ExamSetting mdlExamSetting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mdlExamSetting.ExamSettingsID)
            {
                return BadRequest();
            }

            ExamSetting examSetting = AutoMapper.Mapper.Map<ExamSetting>(mdlExamSetting);

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
        [Route("api/ExamSettings/StoreSettings")]
        [ResponseType(typeof(ExamSetting))]
        public async Task<IHttpActionResult> PostExamSetting(Models.ExamSetting mdlExamSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamSetting examSetting = AutoMapper.Mapper.Map<ExamSetting>(mdlExamSettings);

            db.ExamSettings.Add(examSetting);
            await db.SaveChangesAsync();

            Models.ExamSetting mdlExamSetting = AutoMapper.Mapper.Map<Models.ExamSetting>(examSetting);
            return Ok(mdlExamSetting);
            //return CreatedAtRoute("DefaultApi", new { id = examSetting.ExamSettingsID }, examSetting);

        }

        [Route("api/ExamSettings/AssignExamToUser")]
        [ResponseType(typeof(ExamSetting))]
        public async Task<IHttpActionResult> AssignExam(string email, int examSettingID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User usr = await db.Users.SingleAsync(u => u.email == email);
            ExamSetting exmStng = await db.ExamSettings.SingleAsync(e => e.ExamSettingsID == examSettingID);

            if(usr.ExamSettings.Contains(exmStng))
            {
                return Ok();
            }
            db.Users.Attach(usr);
            usr.ExamSettings.Add(exmStng);
            db.Entry(usr).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
        }

        [Route("api/ExamSettings/AssignExamToBatch")]
        [ResponseType(typeof(ExamSetting))]
        public async Task<IHttpActionResult> AssignBatchExam(string batchId, int examSettingID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Batch> batches = db.Batches.ToList();
            List<ExamSetting> exmSettings = db.ExamSettings.ToList();

            Batch dbBatch = new Batch();
            ExamSetting dbSetting = new ExamSetting();

            foreach(Batch b in batches)
            {
                if(b.BatchID == batchId)
                {
                    dbBatch = b;
                    break;
                }
            }

            foreach (ExamSetting e in exmSettings)
            {
                if (e.ExamSettingsID == examSettingID)
                {
                    dbSetting = e;
                    break;
                }
            }


            //Batch batch = await db.Batches.SingleAsync(b => b.BatchID == batchId);
            //ExamSetting exmStng = await db.ExamSettings.SingleAsync(e => e.ExamSettingsID == examSettingID);

            if (dbBatch.ExamSettings.Contains(dbSetting))
            {
                return Ok();
            }

            //db.Batches.Attach(dbBatch);
            dbBatch.ExamSettings.Add(dbSetting);
            dbSetting.Batches.Add(dbBatch);
            //db.Entry(dbBatch).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
        }

        [Route("api/ExamSettings/AddQuestion")]
        [ResponseType(typeof(ExamSetting))]
        public async Task<IHttpActionResult> AddQuestion(Models.ExamQuestion mdlQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<ExamQuestion> tmpQuestions = await db.ExamQuestions.ToListAsync();

            bool questFound = false;

            foreach (ExamQuestion quest in tmpQuestions)
            {
                if (quest.SettingID == mdlQuestion.SettingID && quest.QuestionID == mdlQuestion.QuestionID)
                {
                    questFound = true;
                    break;
                }
            }

            if(questFound == true)
            {
                return BadRequest("This question is already in this exam setting");
            }

            ExamQuestion dbQuestion = AutoMapper.Mapper.Map<ExamQuestion>(mdlQuestion);
            dbQuestion = db.ExamQuestions.Add(dbQuestion);
            db.Entry(dbQuestion).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
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