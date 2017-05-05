using HomeKitchen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            return View(db);
        }

        public ActionResult ChangeRecipe(int id)
        {
            return View(db.Recipies.Find(id));
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
        public ActionResult GetRecipies(string[] tags,string searchText, string[] ingredients)
        {

            ICollection<RecipeAjax> resultRecipe = new List<RecipeAjax>();
            if (tags != null)
            {
                ICollection<string> recipeTags = new List<string>();
                ICollection<string> recipeIngredients = new List<string>();

                foreach (var recipe in db.Recipies)
                {
                    recipeTags.Clear();
                    foreach (var tag in recipe.Tags)
                    {
                        recipeTags.Add(tag.Tag.Name);
                    }
                    int count2 = 0;
                    int result = 0;
                    if(ingredients!=null)
                    {
                        foreach (var ingredient in recipe.RecipeIngredient)
                        {
                            recipeIngredients.Add(ingredient.Ingredient.Name);
                        }
                        count2 = recipeIngredients.Intersect(ingredients).Count();
                        result = ingredients.Length;
                    }
                    int count = recipeTags.Intersect(tags).Count();
                    
                    string search = searchText ?? "";
                    string name = recipe.Name.ToLower();
                    search = search.ToLower(); 
                    if (count==tags.Length&&name.Contains(search)&&count2==result)
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
        public ActionResult NewRecipe( string[] nameIngrediendRecipe, int[] countIngredienRecipe, int[] measureIdRecipe,  string[] textStepRecipe, List<HttpPostedFileBase> fileInput, HttpPostedFileBase previewPhoto, string nameRecipe = "", string textRecipe = "", int hoursRecipe = 0, int minutesRecipe = 0, string tags = "")
        {
            DirectoryInfo Dir = new DirectoryInfo(Request.MapPath("~/Files/"));
            if (Dir.EnumerateDirectories().FirstOrDefault(i => i.Name == User.Identity.Name) == null)
            {
                Dir.CreateSubdirectory(User.Identity.Name);
            }

            Dir = new DirectoryInfo(Request.MapPath("~/Files/"+ User.Identity.Name));
            if (Dir.EnumerateDirectories().FirstOrDefault(i => i.Name == nameRecipe) == null)
            {
                Dir.CreateSubdirectory(nameRecipe);
            }


            previewPhoto.SaveAs(Server.MapPath("~/Files/"+User.Identity.Name+"/"+nameRecipe+"/" + previewPhoto.FileName));
            Recipe recipe = new Recipe()
            {
                Hours=hoursRecipe,
                Minutes=minutesRecipe,
                DateOfCreation = DateTime.Now,
                Name = nameRecipe,
                PhotoUrl = "../Files/" + User.Identity.Name + "/" + nameRecipe + "/" + previewPhoto.FileName,
                Preview = textRecipe,
                User = db.Users.FirstOrDefault(i => i.Login == User.Identity.Name),

            };
            db.Recipies.Add(recipe);

            string[] arrayOfTags = tags.Split(',');
            for(int j=0;j<arrayOfTags.Length;j++)
            {
                arrayOfTags[j] = arrayOfTags[j].Trim();
            }
            arrayOfTags = arrayOfTags.Distinct().ToArray();
            string result = "";
            for(int j=0;j<arrayOfTags.Length-1;j++)
            {
                var tag = arrayOfTags[j];
                result += tag;

                var tagDb = db.Tags.FirstOrDefault(i => i.Name == tag);
                if(tagDb==null)
                {
                    db.Tags.Add(new Tag { Name = tag, Category = db.Categorys.FirstOrDefault(i => i.Name == "Прочее") });
                }

                TagRecipe tagRecpe = new TagRecipe()
                {
                    
                    Tag = tagDb,
                    Recipe = recipe
                };
                db.TagRecipies.Add(tagRecpe);
            }


            for(int j=0;j<textStepRecipe.Length;j++)
            {
                fileInput[j].SaveAs(Server.MapPath("~/Files/" + User.Identity.Name + "/" + nameRecipe + "/" + fileInput[j].FileName));
                var newStep = new Step()
                {
                    PhotoUrl = "../Files/" + User.Identity.Name + "/" + nameRecipe + "/" + fileInput[j].FileName,
                    Receipe = recipe,
                    Text = textStepRecipe[j]
                };
                db.Steps.Add(newStep);
            }


            for(int j=0;j< countIngredienRecipe.Length;j++)
            {
                string name = nameIngrediendRecipe[j];
                var ingredient = db.Ingredients.FirstOrDefault(i => i.Name == name);
                if (ingredient == null)
                {
                    ingredient = new Ingredient()
                    {

                        Name = nameIngrediendRecipe[j],
                        Permission = 0
                    };
                    db.Ingredients.Add(ingredient);
                };



                var newIngredient = new RecipeIngredient();

                newIngredient.Amount = countIngredienRecipe[j];
                newIngredient.Ingredient = ingredient;
                newIngredient.Recipe = recipe;
                newIngredient.Measure = db.Measuries.Find(measureIdRecipe[j]);
                db.RecipeIngredients.Add(newIngredient);
            }







            db.SaveChanges();
       
            return RedirectToAction("NewRecipe");
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

        public ActionResult EditRecipe()
        {
            return View();
        }

        public ActionResult ChangePassword(string previousPassword, string newPassword)
        {
            var user = db.Users.FirstOrDefault(i=>i.Login==User.Identity.Name);
            if (user.Password == previousPassword)
            {
                user.Password = newPassword;

                db.SaveChanges();
            }
           
           
            return RedirectToAction("MyAccount");
        }

        public ActionResult ChangeInfo(string email, string gender, string name, DateTime? date)
        {
            var user = db.Users.FirstOrDefault(i => i.Login == User.Identity.Name);
            user.Email = email;
            user.UserProfile.Name = name;
            user.UserProfile.Gender = gender;
            if(date!=null) user.UserProfile.DateOfBirth = date.Value;
            db.SaveChanges();
            return RedirectToAction("MyAccount");
        }

        public ActionResult ChangeAvatar(HttpPostedFileBase fileInput)
        {
            var user = db.Users.FirstOrDefault(i => i.Login == User.Identity.Name);
            DirectoryInfo Dir = new DirectoryInfo(Request.MapPath("~/Files/"));
            if (Dir.EnumerateDirectories().FirstOrDefault(i => i.Name == user.Login) == null)
            {
                Dir.CreateSubdirectory(user.Login);
            }




            fileInput.SaveAs(Server.MapPath("~/Files/" + user.Login + "/" +  fileInput.FileName));
            user.UserProfile.PhotoUrl = "../Files/" + User.Identity.Name + "/" + fileInput.FileName;




                db.SaveChanges();
            return RedirectToAction("MyAccount");
        }

        public ActionResult DeleteRecipe(int id)
        {
            var recipe = db.Recipies.Find(id);
            db.Comments.RemoveRange(recipe.Comments);
            db.RecipeIngredients.RemoveRange(recipe.RecipeIngredient);
            db.Steps.RemoveRange(recipe.Steps);
            db.TagRecipies.RemoveRange(recipe.Tags);
            db.FavouriteRecipies.RemoveRange(recipe.FavouriteRecipe);
            db.RecipeRatings.RemoveRange(recipe.RecipeRating);
            db.Recipies.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Search");
        }
    }
}