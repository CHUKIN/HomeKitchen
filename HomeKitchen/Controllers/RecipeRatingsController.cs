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
    public class RecipeRatingsController : Controller
    {
        private KitchenContext db = new KitchenContext();

        // GET: RecipeRatings
        public ActionResult Index()
        {
            var recipeRatings = db.RecipeRatings.Include(r => r.Recipe).Include(r => r.User);
            return View(recipeRatings.ToList());
        }

        // GET: RecipeRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeRating recipeRating = db.RecipeRatings.Find(id);
            if (recipeRating == null)
            {
                return HttpNotFound();
            }
            return View(recipeRating);
        }

        // GET: RecipeRatings/Create
        public ActionResult Create()
        {
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login");
            return View();
        }

        // POST: RecipeRatings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,ReceipeId,Rating")] RecipeRating recipeRating)
        {
            if (ModelState.IsValid)
            {
                db.RecipeRatings.Add(recipeRating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name", recipeRating.ReceipeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", recipeRating.UserId);
            return View(recipeRating);
        }

        // GET: RecipeRatings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeRating recipeRating = db.RecipeRatings.Find(id);
            if (recipeRating == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name", recipeRating.ReceipeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", recipeRating.UserId);
            return View(recipeRating);
        }

        // POST: RecipeRatings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,ReceipeId,Rating")] RecipeRating recipeRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name", recipeRating.ReceipeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", recipeRating.UserId);
            return View(recipeRating);
        }

        // GET: RecipeRatings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeRating recipeRating = db.RecipeRatings.Find(id);
            if (recipeRating == null)
            {
                return HttpNotFound();
            }
            return View(recipeRating);
        }

        // POST: RecipeRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecipeRating recipeRating = db.RecipeRatings.Find(id);
            db.RecipeRatings.Remove(recipeRating);
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
