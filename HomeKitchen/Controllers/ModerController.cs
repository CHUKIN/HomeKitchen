using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeKitchen.Controllers
{
    [Authorize(Roles ="Moder")]
    public class ModerController : Controller
    {
        // GET: Moder
        public ActionResult Index()
        {
            return View();
        }
    }
}