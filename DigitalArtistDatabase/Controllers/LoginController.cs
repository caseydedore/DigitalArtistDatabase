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
    public class LoginController : Controller
    {
        private IUnitOfWork unit;

        public LoginController()
        {
            unit = new UnitOfWork();
        }

        // GET: Login
        public ActionResult Login()
        {
            ViewBag.ArtistID = new SelectList(unit.ArtistRepository.Get(), "ID", "UserName");
            return View();
        }

        //POST: Login/<id>
        [HttpPost]
        public ActionResult Login(int? ArtistID)
        {
            if (ArtistID == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //upon success, return a view to the artist profile page
            Session[SessionConstants.loggedUserID] = ArtistID;
            return RedirectToAction("ArtistPage", "Artist", new { id = ArtistID });
        }
    }
}