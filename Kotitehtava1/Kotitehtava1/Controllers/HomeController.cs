using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kotitehtava1.Models;

namespace Kotitehtava1.Controllers
{
    public class HomeController : Controller
    {
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
            return View();
        }

        public ActionResult About()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Et ole kirjautunut";
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut käyttäjä";
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Et ole kirjautunut";
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut käyttäjä";
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
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
        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
           TilausDBEntities db = new TilausDBEntities();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "Kirjautunut";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Et ole kirjautunut";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Et ole kirjautunut";
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut käyttäjä";
            }
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }
    }
}