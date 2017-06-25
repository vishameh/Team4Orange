using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace EventManager.QueriesFolder
{
    public class UserQueries
    {
        public User CreateUser(User user, Location location)
        {
            User createdUser = null;
            using (var db = new EventRegEntities())
            {
                using (var tx = db.Database.BeginTransaction())
                {
                    var queriedLocation = db.Locations.Where(l =>
                        l.city.Equals(location.city, StringComparison.OrdinalIgnoreCase) &&
                        l.state.Equals(location.state, StringComparison.OrdinalIgnoreCase) &&
                        l.zip == location.zip && l.address.Equals(location.address, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();

                    if (queriedLocation != null)
                        user.location_ID = queriedLocation.location_ID;
                    else
                    {
                        db.Locations.Add(location);
                        db.SaveChanges();
                        user.location_ID = location.location_ID;
                    }

                    createdUser = db.Users.Add(user);
                    db.SaveChanges();
                    tx.Commit();
                }

            }

            return createdUser;
        }

        public void UpdateUser(User user, Location location)
        {
            using (var db = new EventRegEntities())
            {
                using (var tx = db.Database.BeginTransaction())
                {
                    db.Entry(location).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    tx.Commit();
                }
            }
        }
 
        public void DeleteUser(int userId)
        {
            using (var db = new EventRegEntities())
            {
                using (var tx = db.Database.BeginTransaction())
                {
                    var userToBeDeleted = db.Users.Find(userId);
                    var numberOfUsersWithSameLocation = db.Users
                                                          .Include(u => u.Location)
                                                          .Where(u => u.location_ID == userToBeDeleted.location_ID)
                                                          .Count();

                    if (numberOfUsersWithSameLocation == 0)
                    {
                        var location = db.Locations.Find(userToBeDeleted.location_ID);
                        db.Locations.Remove(location);
                    }

                    db.Users.Remove(userToBeDeleted);
                    db.SaveChanges();
                    tx.Commit();
                }
            }
        }

        public User GetUser(int userId)
        {
            User user = null;

            using (var db = new EventRegEntities())
            {
                using (var tx = db.Database.BeginTransaction())
                {
                    user = db.Users.Find(userId);
                    tx.Commit();
                }
            }

            return user;
        }

        public IEnumerable<User> GetAllUsersWithLocationInfo()
        {
            IEnumerable<User> users = null;

            using (var db = new EventRegEntities())
            {
                using (var tx = db.Database.BeginTransaction())
                {
                    users = db.Users.Include(u => u.Location).ToList();
                    tx.Commit();
                }
            }

            return users;
        }
    }
}