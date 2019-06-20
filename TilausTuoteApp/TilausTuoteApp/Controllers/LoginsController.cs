using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausTuoteApp.Models;

namespace TilausTuoteApp.Controllers
{
    public class LoginsController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Et ole kirjautunut";
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut käyttäjä";
            }
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginId,UserName,PassWord")] Logins logins)
        {
            if (ModelState.IsValid)
            {
                db.Logins.Add(logins);
                db.SaveChanges();
                return Redirect("/Home/Login");
            }
            return View();
        }
    }
}
