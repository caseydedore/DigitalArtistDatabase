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

namespace DigitalArtistDatabase.Controllers
{
    public class ThumbnailController : Controller
    {
        private DADContext db = new DADContext();

        // GET: Thumbnail
        public ActionResult Index()
        {
            return View(db.Thumbnails.ToList());
        }

        // GET: Thumbnail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thumbnail thumbnail = db.Thumbnails.Find(id);
            if (thumbnail == null)
            {
                return HttpNotFound();
            }
            return View(thumbnail);
        }

        // GET: Thumbnail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Thumbnail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Image")] Thumbnail thumbnail)
        {
            if (ModelState.IsValid)
            {
                db.Thumbnails.Add(thumbnail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thumbnail);
        }

        // GET: Thumbnail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thumbnail thumbnail = db.Thumbnails.Find(id);
            if (thumbnail == null)
            {
                return HttpNotFound();
            }
            return View(thumbnail);
        }

        // POST: Thumbnail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Image")] Thumbnail thumbnail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thumbnail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thumbnail);
        }

        // GET: Thumbnail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thumbnail thumbnail = db.Thumbnails.Find(id);
            if (thumbnail == null)
            {
                return HttpNotFound();
            }
            return View(thumbnail);
        }

        // POST: Thumbnail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thumbnail thumbnail = db.Thumbnails.Find(id);
            db.Thumbnails.Remove(thumbnail);
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
