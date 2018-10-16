using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using PagedList;

namespace Model.Controllers
{
    public class CommodityController : Controller
    {
        private MVCEntities db = new MVCEntities();

        // GET: Commodity
        public ActionResult Index(string Sort=" ",string SortType="ASC")
        {
        //    var commodity = from commodities in db.Commodities
        //                    join sell in db.SellOrders
        //                    on commodities.CommodityId equals sell.CommodityID
        //                    group sell by new
        //                    {
        //                        sell.CommodityID,
        //                        commodities.CommodityId,
        //                        commodities.CommodityAmount,
        //                        commodities.CommodityImage,
        //                        commodities.CommodityPrice
        //                    }
        //                    into cGetd
        //                    select new
        //                    {
        //                        Id = cGetd.Key.CommodityId,
        //                        Sell = cGetd.Sum(q => q.CommodityAmount)
        //                    };
        //foreach (var item in commodity)
        //    {
        //        System.Diagnostics.Debug.WriteLine("{0},{1}", item.Key.CommodityId, item.Sell);
        //    }
            if (Sort.Equals("Price"))
            {
                if(SortType.Equals("DESC"))
                    return View(db.Commodities.OrderByDescending(q=>q.CommodityPrice));
                else
                    return View(db.Commodities.OrderBy(q => q.CommodityPrice));
            }
            else if (Sort.Equals("Inventory"))
            {
                if (SortType.Equals("DESC"))
                    return View(db.Commodities.OrderByDescending(q => q.CommodityAmount));
                else
                    return View(db.Commodities.OrderBy(q => q.CommodityAmount));
            }
            else
            {
                return View(db.Commodities.ToList());
            }
        }

        // GET: Commodity/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.Commodities.Find(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // GET: Commodity/Create
        public ActionResult Create()
        {
            ViewBag.Types = GetCommodityType();
            return View();
        }

        [NonAction]
        public List<SelectListItem> GetCommodityType()
        {
            var list = db.CommodityTypes.Select(p => p.TypeId);
            var selectItemList = new List<SelectListItem>();
            int i = 0;
            foreach (var item in list)
            {
                if (i == 0)
                    selectItemList.Add(new SelectListItem() { Value = item.ToString(), Text = item.ToString(), Selected = true });
                else
                    selectItemList.Add(new SelectListItem() { Value = item.ToString(), Text = item.ToString() });
                i++;
            }
            return selectItemList;
        }

        // POST: Commodity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommodityId,CommodityName,CommodityPrice,CommodityAmount,CommodityImage,CommodityType")] Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                db.Commodities.Add(commodity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commodity);
        }

        // GET: Commodity/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.Commodities.Find(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // POST: Commodity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommodityId,CommodityName,CommodityPrice,CommodityAmount,CommodityImage")] Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commodity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commodity);
        }

        // GET: Commodity/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.Commodities.Find(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // POST: Commodity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Commodity commodity = db.Commodities.Find(id);
            db.Commodities.Remove(commodity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
