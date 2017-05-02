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
            return View(db);
        }

        public ActionResult Search()
        {
            return View(db.Categorys);
        }

        public ActionResult Account(int? id)
        {
            if(id==null)
            {
                id = db.Users.FirstOrDefault(i=>i.Login==User.Identity.Name).Id;
            }
            return View(db.Users.Find(id));
        }

        public ActionResult MyAccount()
        {
            return View(db.Users.FirstOrDefault(i=>i.Login==User.Identity.Name));
        }

        public ActionResult Recipe(int id)
        {
 
            return View(db.Recipies.Find(id));
        }

        public ActionResult Favorites()
        {
            return View(db.Users.FirstOrDefault(i => i.Login == User.Identity.Name));
        }

        public ActionResult MyRecipies()
        {
            return View(db.Users.FirstOrDefault(i=>i.Login==User.Identity.Name));
        }

        public ActionResult GetRecipies()
        {
            List<RecipeAjax> listRecipe = new List<RecipeAjax>();
            foreach(var recipe in db.Recipies)
            {
                var newRecipe = new RecipeAjax()
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    PhotoUrl = recipe.PhotoUrl,
                    Preview = recipe.Preview,
                    Favourite = recipe.FavouriteRecipe.FirstOrDefault(i => i.User.Login == User.Identity.Name) != null ? true : false,
                    Likes = recipe.RecipeRating.Sum(i=>i.Rating)
                };

                listRecipe.Add(newRecipe);
            }
            return Json(listRecipe,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetRecipies(string[] tags,string searchText)
        {
            ICollection<RecipeAjax> resultRecipe = new List<RecipeAjax>();
            if (tags != null)
            {
                ICollection<string> recipeTags = new List<string>();

               
                foreach (var recipe in db.Recipies)
                {
                    recipeTags.Clear();
                    foreach (var tag in recipe.Tags)
                    {
                        recipeTags.Add(tag.Tag.Name);
                    }
                    int count = recipeTags.Intersect(tags).Count();
                    string search = searchText ?? "";
                    if (count==tags.Length&&recipe.Name.Contains(search))
                    {

                            resultRecipe.Add(new RecipeAjax()
                            {
                                Id = recipe.Id,
                                Name = recipe.Name,
                                PhotoUrl = recipe.PhotoUrl,
                                Preview = recipe.Preview,
                                Favourite = recipe.FavouriteRecipe.FirstOrDefault(i => i.User.Login == User.Identity.Name) != null ? true : false,
                                Likes = recipe.RecipeRating.Sum(i => i.Rating)
                            });     
                    }

                }
                return Json(resultRecipe, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (searchText != null)
                {
                    foreach(var recipe in db.Recipies)
                    {
                        if(recipe.Name.Contains(searchText))
                        {
                            resultRecipe.Add(new RecipeAjax()
                            {
                                Id = recipe.Id,
                                Name = recipe.Name,
                                PhotoUrl = recipe.PhotoUrl,
                                Preview = recipe.Preview
                            });
                        }
                    }
                    return Json(resultRecipe, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return RedirectToAction("GetRecipies");
                }
            }
           
            
        }

        public ActionResult NewRecipe()
        {
            return View(db);
        }

        [HttpPost]
        public ActionResult NewRecipe(string nameRecipe, string textRecipe, int hoursRecipe, int minutesRecipe, string[] nameIngrediendRecipe, int[] countIngredienRecipe, int[] measureIdRecipe, string tags, string[] textStepRecipe, IEnumerable<HttpPostedFileBase> fileInput)
        {
           
            Recipe recipe = new Recipe()
            {
                CookingTime = DateTime.Now,
                DateOfCreation = DateTime.Now,
                Name = nameRecipe,
                PhotoUrl = "",
                Preview = textRecipe,
                User = db.Users.FirstOrDefault(i => i.Login == User.Identity.Name),
                 Tags=new List<TagRecipe>(),
                  Steps=new List<Step>()

            };
            db.Recipies.Add(recipe);
            db.SaveChanges();
            recipe = db.Recipies.FirstOrDefault(i=>i.Name==nameRecipe&&i.Preview==textRecipe);
            string[] arrayOfTags = tags.Split(',');
             foreach(var tag in arrayOfTags)
            {
                TagRecipe tagRecpe = new TagRecipe()
                {
                    Tag = db.Tags.FirstOrDefault(i => i.Name == tag),
                    Recipe = recipe
                };
                recipe.Tags.Add(tagRecpe);
                db.TagRecipies.Add(tagRecpe);
                db.SaveChanges();
            }


            return Content("q");
        }





        [HttpPost]
        public ActionResult SendMessage(int idRecipe, string text)
        {
            var recipe = db.Recipies.Find(idRecipe);
            var user = db.Users.FirstOrDefault(i=>i.Login==User.Identity.Name);
            recipe.Comments.Add(new Comment(){
                Receipe=recipe,
                Text=text,
                Date=DateTime.Now,
                User=user
            });
            db.SaveChanges();
            return RedirectToAction("Recipe/"+idRecipe);
        }

        public ActionResult SwitchFavourite(int id)
        {
            bool result=true;
            var recipeFavourite = db.FavouriteRecipies.FirstOrDefault(i => i.User.Login == User.Identity.Name && i.Recipe.Id == id);
            if (recipeFavourite == null)
            {
                db.FavouriteRecipies.Add(new FavouriteRecipe()
                {  User=db.Users.FirstOrDefault(i=>i.Login==User.Identity.Name),
                 Recipe=db.Recipies.Find(id)}
                );
                result = true;
            }
            else
            {
                db.FavouriteRecipies.Remove(recipeFavourite);
                result = false;
            }
            db.SaveChanges();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult SwitchLikes(int id)
        {
            bool result = true;
            var recipeRating = db.RecipeRatings.FirstOrDefault(i => i.User.Login == User.Identity.Name && i.Recipe.Id == id);
            if (recipeRating == null)
            {
                db.RecipeRatings.Add(new RecipeRating()
                {
                    User = db.Users.FirstOrDefault(i => i.Login == User.Identity.Name),
                    Recipe = db.Recipies.Find(id),
                    Rating=1
                }
                );
                result = true;
            }
            else
            {
                db.RecipeRatings.Remove(recipeRating);
                result = false;
            }
            db.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Likes(int id)
        {
            int result = db.Recipies.Find(id).RecipeRating.Sum(i=>i.Rating);

            return Json(result, JsonRequestBehavior.AllowGet);
        }




    }
}