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
            return View();
        }

        public ActionResult MyRecipies()
        {
            return View(db.Users.FirstOrDefault(i=>i.Login==User.Identity.Name));
        }

        public ActionResult GetRecipies()
        {
            List<Recipe> listRecipe = new List<Recipe>();
            foreach(var recipe in db.Recipies)
            {
                var newRecipe = new Recipe()
                {
                    Id = recipe.Id,
                    Name=recipe.Name,
                    Preview=recipe.Preview,
                    PhotoUrl=recipe.PhotoUrl,
                    CookingTime=recipe.CookingTime,
                    DateOfCreation=recipe.DateOfCreation                  
                };

                listRecipe.Add(newRecipe);
            }
            return Json(listRecipe,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetRecipies(string[] tags,string searchText)
        {
            ICollection<Recipe> resultRecipe = new List<Recipe>();
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

                            resultRecipe.Add(new Recipe()
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
                if (searchText != null)
                {
                    foreach(var recipe in db.Recipies)
                    {
                        if(recipe.Name.Contains(searchText))
                        {
                            resultRecipe.Add(new Recipe()
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
            return View();
        }

       




    }
}