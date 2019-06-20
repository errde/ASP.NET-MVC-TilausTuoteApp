using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausTuoteApp.Models;

namespace TilausTuoteApp.Controllers
{
    public class AsiakkaatController : Controller
    {
        TilausDBEntities db = new TilausDBEntities();

            public ActionResult Index()
            {
            if (Session["UserName"] == null)
                {
                    ViewBag.LoggedStatus = "Et ole kirjautunut";
                }
                else
                {
                    ViewBag.LoggedStatus = "Kirjautunut käyttäjä";
                }
            if (Session["UserName"] == null)
                {
                    return RedirectToAction("login", "home");
                }
                else
                {
                    TilausDBEntities db = new TilausDBEntities();
                    List<Asiakkaat> model = db.Asiakkaat.ToList();
                    return View(model);
                }
        }


        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null) return HttpNotFound();
            //ViewBag.Asiakkaat = new SelectList(db.Henkilot, "HenkilöID,Etunimi,Sukunimi,Osoite,Postinumero");
            return View(asiakkaat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598

        public ActionResult Edit([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                try { 
                    db.Entry(asiakkaat).State = EntityState.Modified;
                    db.SaveChanges();
                    //ViewBag.Asiakkaat = new SelectList(db.Asiakkaat, "AsiakasID, Nimi, Osoite, Postinumero", asiakkaat.AsiakasID);
                    return RedirectToAction("Index");
                    }
                catch
                    {
                    TempData["message"] = "Muokkaus epäonnistui! Tarkista, että antamasi postitoimipaikka löytyy postitoimipaikoista!";
                    return RedirectToAction("Index");
                    }
            }
            return View(asiakkaat);
        }

        public ActionResult Create()
        {
            //ViewBag.P = new SelectList(db.Region, "RegionID", "RegionDescription");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AskiakasID,Nimi,Osoite,Postinumero")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                db.Asiakkaat.Add(asiakkaat);
                db.SaveChanges();
                //ViewBag.HenkiloID = new SelectList(db.Henkilot, "HenkiloID, Etunimi, Sukunimi");
                return RedirectToAction("Index");
            }
            return View(asiakkaat);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null) return HttpNotFound();
            return View(asiakkaat);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            db.Asiakkaat.Remove(asiakkaat);
            try {
                db.SaveChanges();
                return RedirectToAction("Index");
                } catch
                {
                TempData["message2"] = "Asiakasta ei voi poistaa! Asiakkaalla on voimassa olevia tilauksia!";
                return RedirectToAction("Index");
                }
            }
        }
    }