using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Model.Controllers
{
    public class MainController : Controller
    {
        private MVCEntities db = new MVCEntities();
        // GET: Main
        public ActionResult Index()
        {
            if(IsLogin() == false)
            {
                return RedirectToAction("../Index");
            }
            //get Commodity's Sale Amount
            var commodity = db.Commodities.AsEnumerable().Join(db.SellOrders, p => p.CommodityId, q => q.CommodityID,
                (p, q) =>
                new CommoditySaleDeatil(p.CommodityId, p.CommodityName, p.CommodityPrice,
                p.CommodityAmount, p.CommodityImage, q.CommodityAmount));
            return View(commodity.ToList());
        }

        public ActionResult SignOut()
        {
            Response.Cookies["Name"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Email"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("../Index");
        }

        [NonAction]
        public bool IsLogin()
        {
            if(Request.Cookies["Name"] == null || Request.Cookies["Email"] == null)
            {
                return false;
            }
            else
            {
                ViewBag.Name = Request.Cookies["Name"].Value;
                return true;
            }
        }
    }
}