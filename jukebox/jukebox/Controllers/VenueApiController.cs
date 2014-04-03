using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using jukebox.Models;

namespace jukebox.Controllers
{
    public class VenueApiController : ApiController
    {
        private ujukeEntities2 db = new ujukeEntities2();

        // GET api/VenueApi
        public IQueryable<string> GetVenues()
        {
            return db.Venues.OrderBy(c => c.VenueName).Select(c => c.VenueName);
            
        }

        // GET api/VenueApi/5
        public Venue GetVenue(int id)
        {
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return venue;
        }

        // PUT api/VenueApi/5
        public HttpResponseMessage PutVenue(int id, Venue venue)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != venue.VenueID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(venue).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/VenueApi
        public HttpResponseMessage PostVenue(Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Venues.Add(venue);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, venue);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = venue.VenueID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/VenueApi/5
        public HttpResponseMessage DeleteVenue(int id)
        {
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Venues.Remove(venue);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, venue);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}