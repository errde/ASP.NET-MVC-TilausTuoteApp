using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TilausTuoteApp.Models;

namespace TilausTuoteApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        public ActionResult Index()
        {
            return RedirectToAction("./");
        }

        public ActionResult Index2()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Et ole kirjautunut";
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut käyttäjä";
            }
                TilausDBEntities db = new TilausDBEntities();
                List<Tuotteet> model = db.Tuotteet.ToList();
                db.Dispose();

                return View(model);
            }
        }
    }