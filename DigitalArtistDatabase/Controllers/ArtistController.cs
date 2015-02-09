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

        // GET: Artist
        public ActionResult Index()
        {
            return View();
        }

        //get: Artist/Profile/<id>
        public ActionResult ArtistPage(int? id)
        {
            //catch if the id value is null
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //get the artist with matching id
            Artist artist = db.Artists.Find(id);
            if (artist == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //get the posts linked to the artist via its id
            var posts = (from p in db.Posts
                         orderby p.DatePosted
                         where p.ArtistID == artist.ID
                         select p).Include("Comments").Include("Pictures");

            //create an ArtistViewModel which will be used in the view, using the information found by the preceding queries
            ArtistViewModel avm = new ArtistViewModel{ArtistName = artist.UserName};
            
            foreach(var p in posts)
            {
                PostViewModel pvm = new PostViewModel { Title = p.Title, DatePosted = p.DatePosted, Description = p.Description, ViewCount = p.ViewCount};
                
                //NOTE: The reason why this works is because of the Include() at the end of the linq select for posts.
                foreach (var i in p.Pictures)
                {
                    pvm.Pictures.Add(new PictureViewModel { Image = i.Image });
                }

                foreach (var c in p.Comments)
                {
                    //TODO: the artist name and thumbail need to be added somehow
                    pvm.Comments.Add(new CommentViewModel {Text = c.Text, DatePosted = c.DatePosted});
                }

                avm.Posts.Add(pvm);
            }

            return View(avm);
        }
    }
}