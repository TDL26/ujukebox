using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jukebox.Models;

namespace jukebox.Controllers
{
    public class VTController : Controller
    {
        private ujukeEntities2 db = new ujukeEntities2();

        //
        // GET: /VT/

        public ActionResult Index()
        {
            var venuetracks = db.VenueTracks.Include(v => v.Artist).Include(v => v.Genre).Include(v => v.Track).Include(v => v.Venue).Include(v => v.Vote);
            return View(venuetracks.ToList().OrderBy(c => c.Track).Select(c => c.Track));


            //return db.Times.OrderBy(c => c.TimesAvailable).Select(c => c.TimesAvailable);

        }

        // GET: /VT/.....

        public ActionResult Venue1()
        {
            var venuetracks = db.VenueTracks.Include(v => v.Artist).Include(v => v.Genre).Include(v => v.Track).Include(v => v.Venue).Where(v => v.VenueID == 1).Include(v => v.Vote);
            
                        
            return View(venuetracks.ToList());
        }

        //   /vt/venue/?id=2&gen=2
        public ActionResult Venue(int id, int gen)
        {
            var venuetracks = db.VenueTracks.Include(v => v.Artist).Include(v => v.Genre).Where(v => v.GenreID == gen).Include(v => v.Track).Include(v => v.Venue).Where(v => v.VenueID == id).Include(v => v.Vote);
            
            return View(venuetracks.ToList());
        }



        //
        // GET: /VT/Details/5

        public ActionResult Details(int id = 0)
        {
            VenueTrack venuetrack = db.VenueTracks.Find(id);
            if (venuetrack == null)
            {
                return HttpNotFound();
            }
            return View(venuetrack);
        }

        //
        // GET: /VT/Create

        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.TrackID = new SelectList(db.Tracks, "TrackID", "TrackName");
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName");
            ViewBag.VoteID = new SelectList(db.Votes, "VoteID", "VoteID");
            return View();
        }

        //
        // POST: /VT/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VenueTrack venuetrack)
        {
            if (ModelState.IsValid)
            {
                db.VenueTracks.Add(venuetrack);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", venuetrack.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", venuetrack.GenreID);
            ViewBag.TrackID = new SelectList(db.Tracks, "TrackID", "TrackName", venuetrack.TrackID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", venuetrack.VenueID);
            ViewBag.VoteID = new SelectList(db.Votes, "VoteID", "VoteNumber", venuetrack.VoteID);
            return View(venuetrack);
        }

        //
        // GET: /VT/Edit/5

        public ActionResult Edit(int id = 0)
        {
            VenueTrack venuetrack = db.VenueTracks.Find(id);
            if (venuetrack == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", venuetrack.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", venuetrack.GenreID);
            ViewBag.TrackID = new SelectList(db.Tracks, "TrackID", "TrackName", venuetrack.TrackID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", venuetrack.VenueID);
            ViewBag.VoteID = new SelectList(db.Votes, "VoteID", "VoteID", venuetrack.VoteID);
            return View(venuetrack);
        }

        //
        // POST: /VT/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VenueTrack venuetrack)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venuetrack).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", venuetrack.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", venuetrack.GenreID);
            ViewBag.TrackID = new SelectList(db.Tracks, "TrackID", "TrackName", venuetrack.TrackID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", venuetrack.VenueID);
            ViewBag.VoteID = new SelectList(db.Votes, "VoteID", "VoteID", venuetrack.VoteID);
            return View(venuetrack);
        }

        //
        // GET: /VT/Delete/5

        public ActionResult Delete(int id = 0)
        {
            VenueTrack venuetrack = db.VenueTracks.Find(id);
            if (venuetrack == null)
            {
                return HttpNotFound();
            }
            return View(venuetrack);
        }

        //
        // POST: /VT/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VenueTrack venuetrack = db.VenueTracks.Find(id);
            db.VenueTracks.Remove(venuetrack);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}