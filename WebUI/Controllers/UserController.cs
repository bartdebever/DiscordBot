using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.Static_Data;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var database = new Mock();
            var users = database.Users.ToList();
            var user = users[0];
            return View(user);
        }
    }
}