using DigitalArtistDatabase.DAL;
using DigitalArtistDatabase.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalArtistDatabase.Controllers
{
    public class FeaturedImagesController : Controller
    {
        private DADContext db = new DADContext();

        // GET: FeaturedImages/<num?>
        public ActionResult Index(int? id)
        {
            if (id == null) id = 10;
            return (IndexLimited((int)id));
        }

        private ViewResult IndexLimited(int num)
        {
            var posts = (from p in db.Posts
                         orderby p.DatePosted
                         select p).Take(num).Include("Pictures");

            var pfvms = new List<PictureFeaturedViewModel>();


            foreach (var p in posts)
            {
                //this line was for ALL images in a post
                //foreach(var i in p.Pictures) pfvms.Add(new PictureFeaturedViewModel { Image = i.Image, DatePosted = p.DatePosted, ViewCount = p.ViewCount});
                if (p.Pictures.Count > 0) pfvms.Add(new PictureFeaturedViewModel { Image = p.Pictures.ElementAt(0).Image, DatePosted = p.DatePosted, ViewCount = p.ViewCount });
            }


            ViewBag.Title = "Featured Images";
            return View(pfvms);
        }
    }

    /* REFERENCE: Maybe this page will help with image saving/loading
     * 
     * http://stackoverflow.com/questions/26347705/saving-images-to-database-with-asp-net-mvc-4-entity-framework
     */
}