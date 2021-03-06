﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rentilla.Models;
using Microsoft.AspNet.Identity;
using A11_RBS.CustomFilters;

namespace Rentilla.Controllers
{
    public class DemandsController : Controller
    {
        private InterchangeDBContext db = new InterchangeDBContext();
        private ApplicationDbContext adbc = new ApplicationDbContext();
        // GET: Demands
        public ActionResult Index()
        {
            foreach (var item in db.Demands)
            {
                item.UID = (from u in adbc.Users where u.Id.Contains(item.UID) select u.UserName).FirstOrDefault();
            }
            return View(db.Demands.ToList());
        }

        // GET: Demands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demand demand = db.Demands.Find(id);
            if (demand == null)
            {
                return HttpNotFound();
            }
            return View(demand);
        }

        [Authorize]
        // GET: Demands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Demands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Accepted,DateStart,DateEnd,UID,Titel,Description,Allowance,AllowanceDescription")] Demand demand)
        {
            demand.UID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Demands.Add(demand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(demand);
        }

        [AuthorizeAdminOrOwnerOfPost]
        [Authorize]
        // GET: Demands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demand demand = db.Demands.Find(id);
            if (demand == null)
            {
                return HttpNotFound();
            }
            return View(demand);
        }

        [AuthorizeAdminOrOwnerOfPost]
        [Authorize]
        // POST: Demands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Accepted,DateStart,DateEnd,Titel,Description,Allowance,AllowanceDescription")] Demand demand)
        {
            demand.UID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(demand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(demand);
        }

        [AuthorizeAdminOrOwnerOfPost]
        [Authorize]
        // GET: Demands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demand demand = db.Demands.Find(id);
            if (demand == null)
            {
                return HttpNotFound();
            }
            return View(demand);
        }

        [AuthorizeAdminOrOwnerOfPost]
        [Authorize]
        // POST: Demands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Demand demand = db.Demands.Find(id);
            db.Demands.Remove(demand);
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
