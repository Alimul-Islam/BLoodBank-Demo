using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BB2.Models;

namespace BB2.Controllers
{
    [Authorize]
    public class ReqBloodsController : Controller
    {
        private FinalBB2Entities db = new FinalBB2Entities();

        // GET: ReqBloods
        [Authorize(Roles = "Admin")]

        public ActionResult Index()
        {
            var reqBloods = db.ReqBloods.Include(r => r.BloodGroup);
            return View(reqBloods.ToList());
        }

        // GET: ReqBloods/Details/5
        [Authorize(Roles = "Admin")]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReqBlood reqBlood = db.ReqBloods.Find(id);
            if (reqBlood == null)
            {
                return HttpNotFound();
            }
            return View(reqBlood);
        }

        // GET: ReqBloods/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.BloodGroups, "GroupID", "GroupName");
            return View();
        }

        // POST: ReqBloods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReqId,Title,Msg,ReqDate,Phone,Email,Address,GroupID")] ReqBlood reqBlood)
        {
            if (ModelState.IsValid)
            {
                db.ReqBloods.Add(reqBlood);
                db.SaveChanges();

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            ViewBag.GroupID = new SelectList(db.BloodGroups, "GroupID", "GroupName", reqBlood.GroupID);
            return View(reqBlood);
        }

        // GET: ReqBloods/Edit/5
        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReqBlood reqBlood = db.ReqBloods.Find(id);
            if (reqBlood == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.BloodGroups, "GroupID", "GroupName", reqBlood.GroupID);
            return View(reqBlood);
        }

        // POST: ReqBloods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReqId,Title,Msg,ReqDate,Phone,Email,Address,GroupID")] ReqBlood reqBlood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reqBlood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.BloodGroups, "GroupID", "GroupName", reqBlood.GroupID);
            return View(reqBlood);
        }

        // GET: ReqBloods/Delete/5
        [Authorize(Roles = "Admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReqBlood reqBlood = db.ReqBloods.Find(id);
            if (reqBlood == null)
            {
                return HttpNotFound();
            }
            return View(reqBlood);
        }

        // POST: ReqBloods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReqBlood reqBlood = db.ReqBloods.Find(id);
            db.ReqBloods.Remove(reqBlood);
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
