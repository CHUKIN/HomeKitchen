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
    [Authorize(Roles = "Admin")]
    public class TagRecipesController : Controller
    {
        private KitchenContext db = new KitchenContext();

        // GET: TagRecipes
        public ActionResult Index()
        {
            var tagRecipies = db.TagRecipies.Include(t => t.Recipe).Include(t => t.Tag);
            return View(tagRecipies.ToList());
        }

        // GET: TagRecipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TagRecipe tagRecipe = db.TagRecipies.Find(id);
            if (tagRecipe == null)
            {
                return HttpNotFound();
            }
            return View(tagRecipe);
        }

        // GET: TagRecipes/Create
        public ActionResult Create()
        {
            ViewBag.RecipeId = new SelectList(db.Recipies, "Id", "Name");
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Name");
            return View();
        }

        // POST: TagRecipes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeId,TagId")] TagRecipe tagRecipe)
        {
            if (ModelState.IsValid)
            {
                db.TagRecipies.Add(tagRecipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RecipeId = new SelectList(db.Recipies, "Id", "Name", tagRecipe.RecipeId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", tagRecipe.TagId);
            return View(tagRecipe);
        }

        // GET: TagRecipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TagRecipe tagRecipe = db.TagRecipies.Find(id);
            if (tagRecipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecipeId = new SelectList(db.Recipies, "Id", "Name", tagRecipe.RecipeId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", tagRecipe.TagId);
            return View(tagRecipe);
        }

        // POST: TagRecipes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeId,TagId")] TagRecipe tagRecipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tagRecipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecipeId = new SelectList(db.Recipies, "Id", "Name", tagRecipe.RecipeId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", tagRecipe.TagId);
            return View(tagRecipe);
        }

        // GET: TagRecipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TagRecipe tagRecipe = db.TagRecipies.Find(id);
            if (tagRecipe == null)
            {
                return HttpNotFound();
            }
            return View(tagRecipe);
        }

        // POST: TagRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TagRecipe tagRecipe = db.TagRecipies.Find(id);
            db.TagRecipies.Remove(tagRecipe);
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
