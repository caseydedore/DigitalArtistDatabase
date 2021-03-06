﻿using System;
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
        //now using a unit of work that access the context
        //private DADContext db = new DADContext();
        private IUnitOfWork unit;

        public PostController()
        {
            unit = new UnitOfWork();
        }

        public PostController(IUnitOfWork un)
        {
            //this is useful for creating a test controller with a fake unit of work
            unit = un;
        }

        // GET: Post
        public ActionResult Index()
        {
            var posts = //db.Posts.Include(p => p.Artist);
                unit.PostRepository.Get();
            return View(posts.ToList());
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = //db.Posts.Find(id);
                unit.PostRepository.GetByID(id);
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
            ViewBag.ArtistID = //new SelectList(db.Artists, "ID", "UserName");
                new SelectList(unit.ArtistRepository.Get(), "ID", "UserName");
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
            ViewBag.ArtistID = //new SelectList(db.Artists, "ID", "UserName", post.ArtistID);
                new SelectList(unit.ArtistRepository.Get(), "ID", "UserName", post.ArtistID);

            //some validation - probably should also happen with js
            bool isValidPost = post.Description != null || post.Title != null;
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

                foreach (HttpPostedFileBase f in files)
                {
                    if (f != null)
                    {
                        p.Pictures.Add(new Picture { PostID = p.ID, Image = ImageUtility.ImagePostToByte(f) });
                    }
                }

                //db.Posts.Add(p);
                //db.SaveChanges();
                unit.PostRepository.Insert(p);
                unit.Save();

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
            Post post = //db.Posts.Find(id);
                unit.PostRepository.GetByID(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = //new SelectList(db.Artists, "ID", "UserName", post.ArtistID);
                new SelectList(unit.ArtistRepository.Get(), "ID", "UserName", post.ArtistID);
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
                //db.Entry(post).State = EntityState.Modified;
                //db.SaveChanges();
                unit.PostRepository.Update(post);
                unit.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = //new SelectList(db.Artists, "ID", "UserName", post.ArtistID);
                new SelectList(unit.ArtistRepository.Get(), "ID", "UserName", post.ArtistID);
            return View(post);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = //db.Posts.Find(id);
                unit.PostRepository.GetByID(id);
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
            Post post = //db.Posts.Find(id);
                unit.PostRepository.GetByID(id);
            //db.Posts.Remove(post);
            //db.SaveChanges();
            unit.PostRepository.Delete(post);
            unit.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                unit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
