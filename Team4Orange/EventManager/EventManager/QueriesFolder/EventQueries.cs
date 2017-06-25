using System;
using System.Linq;
using System.Data;

namespace EventManager.QueriesFolder
{
    public class EventQueries
    {
        public Event CreateEvent(Event newEvent, Location location)
        {
            Event createdEvent;

            using (var db = new EventRegEntities())
            {
                using (var tx = db.Database.BeginTransaction(IsolationLevel.Unspecified))
                {
                    var queriedLocation = db.Locations.Where(l =>
                        l.city.Equals(location.city, StringComparison.OrdinalIgnoreCase) &&
                        l.state.Equals(location.state, StringComparison.OrdinalIgnoreCase) &&
                        l.zip == location.zip && l.address.Equals(location.address, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();

                    if (queriedLocation != null)
                        newEvent.location_ID = queriedLocation.location_ID;
                    else
                    {
                        db.Locations.Add(location);
                        db.SaveChanges();
                        newEvent.location_ID = location.location_ID;
                    }

                    createdEvent = db.Events.Add(newEvent);
                    db.SaveChanges();
                    tx.Commit();
                }
            }

            return createdEvent;
        }

        public void UpdateEvent(Event theEvent, Location location)
        {
            using (var db = new EventRegEntities())
            {
                using (var tx = db.Database.BeginTransaction())
                {
                    db.Entry(location).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(theEvent).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    tx.Commit();
                }
            }
        }

        public IQueryable<Event> GetAllEvents()
        {
            IQueryable<Event> events;
            using (var db = new EventRegEntities())
            {
                using (var tx = db.Database.BeginTransaction(IsolationLevel.Unspecified))
                {
                    events = db.Events;
                    tx.Commit();
                }
            }
            return events;
        }
    }
}