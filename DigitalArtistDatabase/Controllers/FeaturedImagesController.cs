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

        // GET: FeaturedImages
        public ActionResult Index(int? num)
        {
            if (num == null) num = 10;
            return (IndexLimited((int)num));
        }

        private ViewResult IndexLimited(int num)
        {
            var posts = (from p in db.Posts
                         orderby p.DatePosted
                         select p).Include("Pictures").Take(num);

            var pfvms = new List<PictureFeaturedViewModel>();

            foreach (var p in posts)
            {
                foreach(var i in p.Pictures) pfvms.Add(new PictureFeaturedViewModel { Image = i.Image, DatePosted = p.DatePosted, ViewCount = p.ViewCount});
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