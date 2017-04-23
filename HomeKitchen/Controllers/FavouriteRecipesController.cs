using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeKitchen.Models;

namespace HomeKitchen.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class FavouriteRecipesController : Controller
    {
        private KitchenContext db = new KitchenContext();

        // GET: FavouriteRecipes
        public ActionResult Index()
        {
            var favouriteRecipies = db.FavouriteRecipies.Include(f => f.Recipe).Include(f => f.User);
            return View(favouriteRecipies.ToList());
        }

        // GET: FavouriteRecipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteRecipe favouriteRecipe = db.FavouriteRecipies.Find(id);
            if (favouriteRecipe == null)
            {
                return HttpNotFound();
            }
            return View(favouriteRecipe);
        }

        // GET: FavouriteRecipes/Create
        public ActionResult Create()
        {
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login");
            return View();
        }

        // POST: FavouriteRecipes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,ReceipeId")] FavouriteRecipe favouriteRecipe)
        {
            if (ModelState.IsValid)
            {
                db.FavouriteRecipies.Add(favouriteRecipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name", favouriteRecipe.ReceipeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", favouriteRecipe.UserId);
            return View(favouriteRecipe);
        }

        // GET: FavouriteRecipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteRecipe favouriteRecipe = db.FavouriteRecipies.Find(id);
            if (favouriteRecipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name", favouriteRecipe.ReceipeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", favouriteRecipe.UserId);
            return View(favouriteRecipe);
        }

        // POST: FavouriteRecipes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,ReceipeId")] FavouriteRecipe favouriteRecipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favouriteRecipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name", favouriteRecipe.ReceipeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", favouriteRecipe.UserId);
            return View(favouriteRecipe);
        }

        // GET: FavouriteRecipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteRecipe favouriteRecipe = db.FavouriteRecipies.Find(id);
            if (favouriteRecipe == null)
            {
                return HttpNotFound();
            }
            return View(favouriteRecipe);
        }

        // POST: FavouriteRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FavouriteRecipe favouriteRecipe = db.FavouriteRecipies.Find(id);
            db.FavouriteRecipies.Remove(favouriteRecipe);
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
