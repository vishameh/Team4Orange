﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventManager;
using EventManager.QueriesFolder;
using EventManager.Models;

namespace EventManager.Controllers
{
    public class EventsController : Controller
    {
        private EventRegEntities db = new EventRegEntities();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events.Include(l => l.Location);
            return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title,startDateTime,endDateTime,status,category,type,logoPath,description,address,city,state,zip")] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    title = eventViewModel.title,
                    startDateTime = eventViewModel.startDateTime,
                    endDateTime = eventViewModel.endDateTime,
                    status = eventViewModel.status,
                    category = eventViewModel.category,
                    type = eventViewModel.type,
                    logoPath = eventViewModel.logoPath,
                    description = eventViewModel.description
                };
                Location location = new Location
                {
                    address = eventViewModel.address,
                    city = eventViewModel.city,
                    state = eventViewModel.state,
                    zip = eventViewModel.zip,
                    addressType = "Event"
                };

                (new EventQueries()).CreateEvent(newEvent, location);

                return RedirectToAction("Index");
            }

            return View(eventViewModel);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.location_ID = new SelectList(db.Locations, "location_ID", "city", @event.location_ID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "event_ID,title,startDate,endDate,status,category,type,logoPath,description,location_ID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.location_ID = new SelectList(db.Locations, "location_ID", "city", @event.location_ID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
