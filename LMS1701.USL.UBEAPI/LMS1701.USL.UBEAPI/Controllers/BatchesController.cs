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
    public class BatchesController : ApiController
    {
        private UserScoresLoginEntities db = new UserScoresLoginEntities();

        // GET: api/Batches
        public List<Models.Batch> GetBatches()
        {
            List<Models.Batch> mdlBatches = new List<Models.Batch>();

            foreach (DAL.Batch btc in db.Batches.ToList())
            {
                mdlBatches.Add(AutoMapper.Mapper.Map<Models.Batch>(btc));
            }

            return mdlBatches;
        }


        // GET
        [Route("api/Batches/GetBatch")]
        [ResponseType(typeof(Batch))]
        public async Task<IHttpActionResult> GetBatch(int id)
        {
            Batch batch = await db.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            Models.Batch mdlBatch = AutoMapper.Mapper.Map<Models.Batch>(batch);

            return Ok(mdlBatch);
        }

        [Route("api/Batches/GetBatchGradebook")]
        [ResponseType(typeof(Object))]

        public async Task<IHttpActionResult> GetBackGradebook(int id)
        {
            //need batch roster with grades for each exam the batch has taken

            return Ok(new Object());
        }


        // GET
        [Route("api/Batches/GetBatch")]
        [ResponseType(typeof(Batch))]
        public async Task<IHttpActionResult> GetBatch(string batchID)
        {
            List<Batch> batches = await db.Batches.ToListAsync();

            Batch tmpBatch = null;

            foreach (Batch btc in batches)
            {
                if (btc.BatchID == batchID)
                {
                    tmpBatch = btc;
                    break;
                }
            }


            if (tmpBatch == null)
            {
                return NotFound();
            }

            Models.Batch mdlBatch = AutoMapper.Mapper.Map<Models.Batch>(tmpBatch);


            return Ok(mdlBatch);
        }

        // GET
        [Route("api/Batches/GetBatches")]
        [ResponseType(typeof(Batch))]
        public async Task<IHttpActionResult> GetBatches(string email)
        {
            List<Models.Batch> batches = new List<Models.Batch>();

            User dbUser = await db.Users.FirstOrDefaultAsync(u => u.email == email);

            if(dbUser == null)
            {
                BadRequest("User Not Found");
            }

            foreach(Roster rstr in dbUser.Rosters)
            {
                batches.Add(AutoMapper.Mapper.Map<Models.Batch>(rstr.Batch));
            }


            return Ok(batches);
        }

        // PUT
        [Route("api/Batches/EditBatch")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBatch(string id, Models.Batch mdlBatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Batch saveBatch = null;

            try
            {
                saveBatch = await db.Batches.SingleAsync(b => b.BatchID == mdlBatch.BatchID);
            }
            catch (Exception e)
            {
                return NotFound();
            }

            Batch tmpBatch = AutoMapper.Mapper.Map<Batch>(mdlBatch);

            tmpBatch.BatchPK = saveBatch.BatchPK;

            db.Batches.Attach(saveBatch);

            saveBatch = tmpBatch;

            db.Entry(saveBatch).State = EntityState.Modified;

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

        [Route("api/Batches/CreateBatch")]
        // POST: api/Batches
        [ResponseType(typeof(Batch))]
        public async Task<IHttpActionResult> PostBatch(Models.Batch mdlBatch)
        {
            Batch dbBatch = AutoMapper.Mapper.Map<Batch>(mdlBatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbBatch = db.Batches.Add(dbBatch);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dbBatch.BatchPK }, dbBatch);
        }

        // DELETE: api/Batches/5
        [ResponseType(typeof(Batch))]
        public async Task<IHttpActionResult> DeleteBatch(int id)
        {
            Batch batch = await db.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            db.Batches.Remove(batch);
            await db.SaveChangesAsync();

            return Ok(batch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BatchExists(int id)
        {
            return db.Batches.Count(e => e.BatchPK == id) > 0;
        }

        private bool BatchExists(string id)
        {
            return db.Batches.Count(e => e.BatchID == id) > 0;
        }
    }
}