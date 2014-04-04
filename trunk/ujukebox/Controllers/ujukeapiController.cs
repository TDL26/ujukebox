using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ujukebox.Models;

namespace ujukebox.Controllers
{
    public class ujukeapiController : ApiController
    {
        private ujukeboxdbEntities1 db = new ujukeboxdbEntities1();

        // GET api/ujukeapi
        public IQueryable<Track> GetTracks()
        {
            return db.Tracks;
        }

        //  // GET api/PlaylistApi
        //public IQueryable<string> GetVote()
        //{
        //    return db.Tracks.OrderBy(c => c.Vote.ToString()).Select(c => c.Vote.ToString());
        //}

        // GET api/ujukeapi/5
        [ResponseType(typeof(Track))]
        public IHttpActionResult GetTrack(int id)
        {
            Track track = db.Tracks.Find(id);
            if (track == null)
            {
                return NotFound();
            }

            return Ok(track);
            //var instructor = (from i in _instructors
            //                  where i.ID == id
            //                  select i).FirstOrDefault();
            //return instructor;
        }

        //public void PutUpdateTrack(Track track)                                  // listing will be in request body
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int index = db.FindIndex(t => t.TickerSymbol.ToUpper() == db.Title.ToUpper());
        //        if (index == -1)
        //        {
        //            throw new HttpResponseException(HttpStatusCode.NotFound);               // 404
        //        }
        //        else
        //        {
        //            track.RemoveAt(index);
        //            track.Add(db.Tracks);
        //            // default is to return 200 OK
        //        }
        //    }
        //    else
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }
        //}

        // PUT api/ujukeapi/5
        public IHttpActionResult PutTrack(int id, Track track)
        {
            //var realinstructor = (from i in _instructors
            //                  where i.ID == id
            //                  select i).FirstOrDefault();
            //realinstructor.Name = instructor.Name;
            //return realinstructor;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != track.ID)
            {
                return BadRequest();
            }

            db.Entry(track).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackExists(id))
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

        // POST api/ujukeapi
        [ResponseType(typeof(Track))]
        public IHttpActionResult PostTrack(Track track)
        {

            //var realinstructor = (from i in _instructors
            //                  where i.ID == id
            //                  select i).FirstOrDefault();
            //_instructors.Remove(realinstructor);
           


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tracks.Add(track);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = track.ID }, track);

            //var newId = _instructors.Count + 1;
            //instructor.ID = newId;
            //_instructors.Add(instructor);
            //return instructor;
        }

        // DELETE api/ujukeapi/5
        [ResponseType(typeof(Track))]
        public IHttpActionResult DeleteTrack(int id)
        {
            Track track = db.Tracks.Find(id);
            if (track == null)
            {
                return NotFound();
            }

            db.Tracks.Remove(track);
            db.SaveChanges();

            return Ok(track);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrackExists(int id)
        {
            return db.Tracks.Count(e => e.ID == id) > 0;
        }
    }
}