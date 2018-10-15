using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Models;

namespace Model.Controllers
{
    public class SellOrdersController : Controller
    {
        private MVCEntities db = new MVCEntities();

        // GET: SellOrders
        public ActionResult Index()
        {
            var list = db.SellOrders.Select(p => p.OrderId).Distinct().ToList();
            return View(list);
        }

        // GET: SellOrders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.SellOrders.AsEnumerable().Where(p => p.OrderId.Equals(id));
            var list = order.Join(
                db.Commodities, p => p.CommodityID, q => q.CommodityId,
                (p, q) =>
                    new Commodity(q.CommodityId,
              q.CommodityName,
              q.CommodityPrice,
              p.CommodityAmount,
              q.CommodityImage)).ToList();
            ViewBag.ID = id;
            return View(list);
        }

        // GET: SellOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CommodityID,CommodityAmount")] SellOrder sellOrder)
        {
            if (ModelState.IsValid)
            {
                db.SellOrders.Add(sellOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sellOrder);
        }

        // GET: SellOrders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellOrder sellOrder = db.SellOrders.Find(id);
            if (sellOrder == null)
            {
                return HttpNotFound();
            }
            return View(sellOrder);
        }

        // POST: SellOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CommodityID,CommodityAmount")] SellOrder sellOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellOrder).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sellOrder);
        }

        // GET: SellOrders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellOrder sellOrder = db.SellOrders.Find(id);
            if (sellOrder == null)
            {
                return HttpNotFound();
            }
            return View(sellOrder);
        }

        // POST: SellOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SellOrder sellOrder = db.SellOrders.Find(id);
            db.SellOrders.Remove(sellOrder);
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
