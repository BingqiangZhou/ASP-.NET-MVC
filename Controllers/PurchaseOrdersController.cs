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
    public class PurchaseOrdersController : Controller
    {
        private MVCEntities db = new MVCEntities();

        // GET: PurchaseOrders
        public ActionResult Index()
        {
            return View(db.PurchaseOrders.Select(p=>p.PurchaseOrderId).Distinct().ToList());
        }

        // GET: PurchaseOrders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.PurchaseOrders.AsEnumerable().Where(p => p.PurchaseOrderId.Equals(id));
            var list = order.Join(
                db.Commodities, p => p.CommodityId, q => q.CommodityId,
                (p, q) =>
                    new Commodity(q.CommodityId,
              q.CommodityName,
              q.CommodityPrice,
              p.CommodityAmount,
              q.CommodityImage,q.CommodityType)).ToList();
            ViewBag.ID = id;
            return View(list);
        }

        // GET: PurchaseOrders/Create

        public ActionResult Create(string id=null)
        {
            if(id != null)
            {
                ViewBag.PurchaseOrderId = id;
                ViewBag.Commodity = GetCommodityFromOrder(id);
            }
            ViewBag.ID = GetCommodityID();
            return View();
        }

        [NonAction]
        public List<SelectListItem> GetCommodityID()
        {
            var list = db.Commodities.Select(p => p.CommodityId);
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

        [NonAction]
        public List<Commodity> GetCommodityFromOrder(string id)
        {
            var commdity = from p in db.PurchaseOrders
                           join c in db.Commodities on p.CommodityId equals c.CommodityId
                           where p.PurchaseOrderId == id
                           select c;
            return commdity.ToList();
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseOrderId,CommodityId,CommodityAmount")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrders.Add(purchaseOrder);
                db.SaveChanges();
            }
            ViewBag.PurchaseOrderId = purchaseOrder.PurchaseOrderId;
            ViewBag.ID = GetCommodityID();
            ViewBag.Commodity = GetCommodityFromOrder(purchaseOrder.PurchaseOrderId);
            Commodity cc = db.Commodities.Find(purchaseOrder.CommodityId);
            cc.CommodityAmount += purchaseOrder.CommodityAmount;
            db.SaveChanges();
            //foreach (var item in commdity.ToList())
            //{
            //    System.Diagnostics.Debug.WriteLine(item.CommodityName);
            //}
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseOrderId,CommodityId,CommodityAmount")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrder).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Delete/5
        public ActionResult Delete(string pid,string cid)
        {
            if (pid == null || cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(pid,cid);
            ViewBag.PurchaseOrderId = purchaseOrder.PurchaseOrderId;
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string pid,string cid)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(pid,cid);
            int amount = purchaseOrder.CommodityAmount;
            db.PurchaseOrders.Remove(purchaseOrder);
            Commodity commodity = db.Commodities.Find(purchaseOrder.CommodityId);
            commodity.CommodityAmount -= amount;
            db.SaveChanges();
            ViewBag.ID = GetCommodityID();
            ViewBag.PurchaseOrderId = purchaseOrder.PurchaseOrderId;
            return RedirectPermanent("Create/"+purchaseOrder.PurchaseOrderId);
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
