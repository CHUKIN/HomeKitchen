using HomeKitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HomeKitchen.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string Login)
        {
            string[] role = new string[] { };
            using (KitchenContext db = new KitchenContext())
            {
                // Получаем пользователя
                User user = db.Users.FirstOrDefault(u => u.Login == Login);
                if (user != null)
                {
                    // Получаем роль
                    Role userRole = db.Roles.Find(user.RoleId);
                    if (userRole != null)
                    {
                        switch(userRole.Name) // В зависимости от роли, добавляем список ролей
                        {
                            case "Admin":
                                role = new string[] { "Admin","Moder","User" };
                                break;
                            case "Moder":
                                role = new string[] {  "Moder", "User" };
                                break;
                            case "User":
                                role = new string[] {"User" };
                                break;
                        }
                    }
                }
            }
            return role;
        }
        public override void CreateRole(string roleName)
        {
            Role newRole = new Role() { Name = roleName };
            KitchenContext db = new KitchenContext();
            db.Roles.Add(newRole);
            db.SaveChanges();
        }
        public override bool IsUserInRole(string Login, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя
            using (KitchenContext db = new KitchenContext())
            {
                // Получаем пользователя
                User user = db.Users.FirstOrDefault(u => u.Login == Login);
                if (user != null)
                {
                    // получаем роль
                    Role userRole = db.Roles.Find(user.RoleId);
                    //сравниваем
                    if (userRole != null && userRole.Name == roleName)
                    {
                        outputResult = true;
                    }
                }
            }
            return outputResult;
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}