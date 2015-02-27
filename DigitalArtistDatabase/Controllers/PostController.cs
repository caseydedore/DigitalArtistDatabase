using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DigitalArtistDatabase.DAL;
using DigitalArtistDatabase.Models;
using System.IO;

namespace DigitalArtistDatabase.Controllers
{
    public class PostController : Controller
    {
        private DADContext db = new DADContext();

        // GET: Post
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Artist);
            return View(posts.ToList());
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            //so we can create a dropdown list in the view
            ViewBag.ArtistID = new SelectList(db.Artists, "ID", "UserName");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,ArtistID,Pictures")] PostCreateViewModel post, IEnumerable<HttpPostedFileBase> files)
        {
            //this will help us create a dropdown list for artists. IT is temporary, as eventually the artist ID will be the artist that is logged in
            ViewBag.ArtistID = new SelectList(db.Artists, "ID", "UserName", post.ArtistID);

            //some validation - probably should also happen with js
            bool isValidPost = post.Description.Length > 0 || post.Title.Length > 0;
            if (isValidPost)
            {
                isValidPost = false;
                foreach (HttpPostedFileBase f in files)
                {
                    if (f != null && f.ContentLength > 0) isValidPost = true;
                }
            }
            if (!isValidPost) return View(post);

            //some validation
            //if (file == null || file.ContentLength <= 0 || post.Description.Length <= 0 || post.Title.Length <= 0) return View(post);

            if (ModelState.IsValid)
            {
                //migrate data from the view model to an actual post object for the database, which can be saved
                Post p = new Post { ArtistID = post.ArtistID, Title = post.Title, Description = post.Description, DatePosted = DateTime.Now, Pictures = new List<Picture>()};

                int iteration = 0;
                foreach (HttpPostedFileBase f in files)
                {
                    if (f != null)
                    {
                        //save the pic with generated url, add the url to the database compatible object (the filename is extracted outside of this block)
                        string timeStamp = DateTime.UtcNow.ToString();
                        timeStamp = new string(timeStamp.Where(c => char.IsDigit(c)).ToArray());
                        string fileName = f.FileName;
                        string fileUrl = p.ArtistID.ToString() + timeStamp + iteration++.ToString() + fileName;
                        string path = AppDomain.CurrentDomain.BaseDirectory + "Pictures/Uploads/" + fileUrl;
                        f.SaveAs(path);
                        //the database will need the picture url and post id
                        p.Pictures.Add(new Picture { ImageURL = "~/Pictures/Uploads/" + fileUrl, PostID = p.ID });
                    }
                }

                db.Posts.Add(p);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ID", "UserName", post.ArtistID);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,DatePosted,ArtistID")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ID", "UserName", post.ArtistID);
            return View(post);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
