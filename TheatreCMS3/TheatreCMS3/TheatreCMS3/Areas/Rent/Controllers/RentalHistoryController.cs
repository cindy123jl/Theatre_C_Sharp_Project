﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Areas.Rent.Models;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Rent.Controllers
{
    public class RentalHistoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent/RentalHistory
        public ActionResult Index(string sortOrder)
        {
            
            
            return View(db.RentalHistories.ToList());
        }
        [HttpPost]
        public PartialViewResult PartialTable(string sortOrder)
        {
            var rentalHistories = from s in db.RentalHistories
                                  select s;
            if (sortOrder == "default")
            {

            }
            else if (sortOrder == "damage_rentals")
            {
                rentalHistories = rentalHistories.OrderByDescending(s => s.RentalDamaged);
            }
            else if (sortOrder == "damage_rentals_desc")
            {
                rentalHistories = rentalHistories.OrderBy(s => s.RentalDamaged);
            }
            else if (sortOrder == "alpha_rentals")
            {
                rentalHistories = rentalHistories.OrderBy(s => s.Rental);
            }
            else if (sortOrder == "alpha_rentals_desc")
            {
                rentalHistories = rentalHistories.OrderByDescending(s => s.Rental);
            }
            return PartialView("_IndexTable", rentalHistories);
        }

        // GET: Rent/RentalHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            if (rentalHistory == null)
            {
                return HttpNotFound();
            }
            return View(rentalHistory);
        }

        // GET: Rent/RentalHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rent/RentalHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RentalDamaged,DamagesIncurred,Rental")] RentalHistory rentalHistory)
        {
            if (ModelState.IsValid)
            {
                db.RentalHistories.Add(rentalHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentalHistory);
        }

        // GET: Rent/RentalHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            if (rentalHistory == null)
            {
                return HttpNotFound();
            }
            return View(rentalHistory);
        }

        // POST: Rent/RentalHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RentalDamaged,DamagesIncurred,Rental")] RentalHistory rentalHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentalHistory);
        }

        // GET: Rent/RentalHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            if (rentalHistory == null)
            {
                return HttpNotFound();
            }
            return View(rentalHistory);
        }

        // POST: Rent/RentalHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            db.RentalHistories.Remove(rentalHistory);
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
