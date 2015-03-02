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
        //private DADContext db = new DADContext();
        private IThumbnailRepository repository;

        public ThumbnailController()
        {
            repository = new ThumbnailRepository(new DADContext());
        }

        public ThumbnailController(IThumbnailRepository t)
        {
            repository = t;
        }

        // GET: Thumbnail
        public ActionResult Index()
        {
            return View(repository.GetThumbnails());
        }

        // GET: Thumbnail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thumbnail thumbnail = repository.GetThumbnail((int)id);
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
        public ActionResult Create(HttpPostedFileBase file)
        {
            Thumbnail thumbnail = new Thumbnail();
            if (ModelState.IsValid && file != null && file.ContentLength > 0)
            {
                thumbnail.Image = ImageUtility.ImagePostToByte(file);

                repository.AddThumbnail(thumbnail);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }

            //failure! go back to the Create view. There is no information that needs to be sent
            return View();
        }

        // GET: Thumbnail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thumbnail thumbnail = repository.GetThumbnail((int)id);
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
                repository.UpdateThumbnail(thumbnail);
                repository.SaveChanges();
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
            Thumbnail thumbnail = repository.GetThumbnail((int)id);
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
            Thumbnail thumbnail = repository.GetThumbnail(id);
            repository.DeleteThumbnail(id);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
