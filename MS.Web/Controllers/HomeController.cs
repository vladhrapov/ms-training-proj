using MS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //ViewData["Hello"] = "Hello";
            ViewData.Add(new KeyValuePair<string, object>("Hello", "Hello"));
            ViewBag.Greeting = "Greeting";
            return View();
        }

        [HttpGet]
        public ActionResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                //ToDo: We can send mail here
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }
        }
    }
}