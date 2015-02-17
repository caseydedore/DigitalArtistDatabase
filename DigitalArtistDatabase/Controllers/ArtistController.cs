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
        private DADContext db = new DADContext();

        //get: Artist/Index/
        public ActionResult Index()
        {
            var artists = (from a in db.Artists
                           select a);

            return View(artists);
        }

        //get: Artist/ArtistPage/<id?>
        public ActionResult ArtistPage(int? id)
        {
            //catch if the id value is null
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //get the artist with matching id
            //Artist artist = db.Artists.Find(id);

            var artist = (from a in db.Artists
                             where a.ID == (int)id
                             select a).FirstOrDefault();
            
            if (artist == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //get the posts linked to the artist via its id
            var posts = (from p in db.Posts
                         orderby p.DatePosted
                         where p.ArtistID == artist.ID
                         select p).Include("Comments").Include("Pictures");

            foreach (var p in posts)
            {
                artist.Posts.Add(p);
            }

            return View(artist);
        }
    }
}