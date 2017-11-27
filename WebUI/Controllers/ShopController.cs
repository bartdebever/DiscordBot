using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Shop;

namespace WebUI.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            var itemlist = ShopItems.GetShopItems;
            return View(itemlist);
        }

        public ActionResult Edit(int id)
        {
            var item = ShopItems.GetShopItems.FirstOrDefault(x => x.Id == id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(ShopItem item)
        { 
            ShopItems.ReplaceItem(item);
            return RedirectToAction("Index");
        }
    }
}