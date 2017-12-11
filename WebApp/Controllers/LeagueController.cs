using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class LeagueController : Controller
    {
        // GET: League
        public ActionResult Index()
        {
            return RedirectToAction("Champions");
        }
        [Authorize]
        public ActionResult Champions()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = new ApplicationDbContext().Users.FirstOrDefault(x => x.UserName == userName);
                if (user.IsAdmin)
                {
                    var riotData = new RiotData();
                    var champions = riotData.Champions.Include("spells").Include("skins").Include("passive").Include("stats").ToList();
                    return View(champions);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditChampion(int id)
        {
            var champion = new RiotData().Champions.Include("spells").Include("spells.vars").Include("spells.vars.Coeff").Include("skins").Include("passive").Include("stats").ToList().FirstOrDefault(x=> x.Id == id);
            return View(champion);
        }
    }
}