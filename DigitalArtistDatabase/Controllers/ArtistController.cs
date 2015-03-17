using DigitalArtistDatabase.DAL;
using DigitalArtistDatabase.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DigitalArtistDatabase.Controllers
{
    public class ArtistController : Controller
    {
        //private DADContext db = new DADContext();
        private IUnitOfWork unit;

        public ArtistController()
        {
            unit = new UnitOfWork();
        }

        public ArtistController(IUnitOfWork un)
        {
            unit = un;
        }

        //get: Artist/Index/
        public ActionResult Index()
        {
            var artists = unit.ArtistRepository.Get(includeProperties: "Thumbnail");

            return View(artists);
        }

        //get: Artist/Create/
        public ActionResult Create()
        {
            //so we can create a dropdown list in the view
            ViewBag.ThumbnailID = //new SelectList(db.Thumbnails, "ID", "ID");
                new SelectList(unit.ThumbnailRepository.Get(), "ID", "ID");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //post: Artist/Create/
        public ActionResult Create([Bind(Include = "UserName, Description, ThumbnailID")] Artist artist)
        {
            if (ModelState.IsValid == false) return View(artist);

            artist.DateJoined = DateTime.Now;

            unit.ArtistRepository.Insert(artist);
            unit.Save();

            return RedirectToAction("Index");
        }

        //get: Artist/ArtistPageAutoDirect/
        public ActionResult ArtistPageAutoDirect()
        {
            var i = Session[SessionConstants.loggedUserID];

            return RedirectToAction("ArtistPage", "Artist", new { id = i });
        }

        //get: Artist/ArtistPage/<id?>
        public ActionResult ArtistPage(int? id)
        {
            //catch if the id value is null
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //get the artist; also get the thumbnail that the artist has an FK and nav property for

            var artist = unit.ArtistRepository.Get(a => a.ID == (int)id, includeProperties: "Thumbnail, Posts");
            //var posts = unit.PostRepository.Get(p => p.ArtistID == (int)id);
            //var comments = unit.CommentRepository.Get(c => c.ArtistID == (int)id);
            
            if (artist == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //convert from collection to single Artist obj
            Artist art = artist.FirstOrDefault();
            //attach the posts and comment directly
            //art.Posts = posts.ToList();
            //art.Comments = comments.ToList();

            return View(art);
        }

        //get: Artist/Delete
        public ActionResult Delete()
        {
            ViewBag.ArtistID = new SelectList(unit.ArtistRepository.Get(), "ID", "ID");

            return View();
        }

        //post: Artist/Delete
        [HttpPost]
        public ActionResult Delete(Artist artist)
        {
            var posts = unit.PostRepository.Get(p => p.ArtistID == artist.ID);

            //IDEA: If a method of deletion was added into the repositories that allowed a filter (which will use IQueryable and not a collection), 
            //then we would not need to load posts into memory and use a foreach to delete...
            foreach (Post p in posts)
            {
                unit.PostRepository.Delete(p);
            }

            unit.ArtistRepository.Delete(artist);
            unit.Save();

            return RedirectToAction("Index");
        }
    }
}