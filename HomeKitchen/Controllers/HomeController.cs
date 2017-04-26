using HomeKitchen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeKitchen.Controllers
{
    public class HomeController : Controller
    {
        KitchenContext db = new KitchenContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View(db.Categorys.Include(i=>i.Tags));
        }

        public ActionResult Account()
        {
            //return View(db.Users.Include(i=>i.UserProfile).FirstOrDefault(i=>i.Id==id));
            return View();
        }

        public ActionResult MyAccount()
        {
            return View();
        }

        public ActionResult Recipe(int id)
        {
            return View(db.Recipies.Include(i=>i.Tags).Include(j=>j.Steps).Include(k=>k.Ingredient).FirstOrDefault(q=>q.Id==id));
        }

        public ActionResult Favorites()
        {
            return View();
        }

        public ActionResult MyRecipies()
        {
            return View();
        }

        public ActionResult GetRecipies()
        {
            return Json(db.Recipies,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetRecipies(string[] tags)
        {

           
            return Json(db.Recipies,JsonRequestBehavior.AllowGet);
        }

       
    }
}