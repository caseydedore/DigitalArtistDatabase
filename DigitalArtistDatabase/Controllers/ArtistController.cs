using DigitalArtistDatabase.DAL;
using DigitalArtistDatabase.Models;
using System;
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
        public ActionResult Profile(int? id)
        {
            //catch if the id value is null
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            Artist rt = db.Artists.Find(id);

            return View(rt);
        }
    }
}