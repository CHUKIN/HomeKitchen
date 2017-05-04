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
    public class RecipeIngredientsController : Controller
    {
        private KitchenContext db = new KitchenContext();

        // GET: RecipeIngredients
        public ActionResult Index()
        {
            var recipeIngredients = db.RecipeIngredients.Include(r => r.Ingredient).Include(r => r.Measure).Include(r => r.Recipe);
            return View(recipeIngredients.ToList());
        }

        // GET: RecipeIngredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeIngredient recipeIngredient = db.RecipeIngredients.Find(id);
            if (recipeIngredient == null)
            {
                return HttpNotFound();
            }
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Create
        public ActionResult Create()
        {
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name");
            ViewBag.MeasureId = new SelectList(db.Measuries, "Id", "Name");
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name");
            return View();
        }

        // POST: RecipeIngredients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredientId,ReceipeId,MeasureId,Amount")] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid)
            {
                db.RecipeIngredients.Add(recipeIngredient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name", recipeIngredient.IngredientId);
            ViewBag.MeasureId = new SelectList(db.Measuries, "Id", "Name", recipeIngredient.MeasureId);
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeIngredient recipeIngredient = db.RecipeIngredients.Find(id);
            if (recipeIngredient == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name", recipeIngredient.IngredientId);
            ViewBag.MeasureId = new SelectList(db.Measuries, "Id", "Name", recipeIngredient.MeasureId);
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IngredientId,ReceipeId,MeasureId,Amount")] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeIngredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name", recipeIngredient.IngredientId);
            ViewBag.MeasureId = new SelectList(db.Measuries, "Id", "Name", recipeIngredient.MeasureId);
            ViewBag.ReceipeId = new SelectList(db.Recipies, "Id", "Name", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeIngredient recipeIngredient = db.RecipeIngredients.Find(id);
            if (recipeIngredient == null)
            {
                return HttpNotFound();
            }
            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecipeIngredient recipeIngredient = db.RecipeIngredients.Find(id);
            db.RecipeIngredients.Remove(recipeIngredient);
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
