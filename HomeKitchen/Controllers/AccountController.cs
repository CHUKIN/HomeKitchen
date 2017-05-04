using HomeKitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;






namespace HomeKitchen.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (KitchenContext db = new KitchenContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (KitchenContext db = new KitchenContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (KitchenContext db = new KitchenContext())
                    {
                        user = new User { Login = model.Login, Password = model.Password, DateOfRegistration = DateTime.Now, Role = db.Roles.Where(i => i.Name == "User").FirstOrDefault(), Email=model.Email};
                        db.Users.Add(user);
                        var userProfile = new UserProfile()
                        {
                            DateOfBirth = DateTime.Now,
                            Gender = null,
                            Name = "",
                            Surname = "",
                            PhotoUrl = "",
                            User = user
                        };
                        db.UserProfilies.Add(userProfile);
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}