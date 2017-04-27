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
            Recipe recipe = new Recipe();
            var rec = db.Recipies.Find(id);
            recipe.Name = rec.Name;
            recipe.Id = rec.Id;
            recipe.CookingTime = rec.CookingTime;
            recipe.DateOfCreation = rec.DateOfCreation;
            recipe.PhotoUrl = rec.PhotoUrl;
            recipe.Preview = rec.Preview;
            recipe.User = new User();
            User us = db.Users.Find(rec.UserId);
            recipe.User.Login = us.Login;

            var ingredients = db.RecipeIngredients.Where(i=>i.ReceipeId==rec.Id).ToList();
            for(int i=0;i<ingredients.Count();i++)
            {
                ingredients[i].Ingredient = db.Ingredients.Find(ingredients[i].IngredientId);
                ingredients[i].Measure = db.Measuries.Find(ingredients[i].MeasureId);
            }
            recipe.RecipeIngredient = ingredients;
            // return Json(recipe,JsonRequestBehavior.AllowGet);
            return View(recipe);
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

        public ActionResult NewRecipe()
        {
            return View();
        }





    }
}