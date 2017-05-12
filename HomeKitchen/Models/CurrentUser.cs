using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class CurrentUser
    {
        private static CurrentUser instance;

        User user;
        string name;

        private CurrentUser(string name)
        {
            KitchenContext db = new KitchenContext();
            this.name = name;
            user = db.Users.FirstOrDefault(i=>i.Login==name);
        }

        public User GetUser()
        {
            return user;
        }

        public static CurrentUser getInstance(string name)
        {
            if (instance == null)
                instance = new CurrentUser(name);
            return instance;
        }
    }
}