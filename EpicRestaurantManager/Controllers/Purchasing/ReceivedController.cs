using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using EpicRestaurantManager.Models;
using System;

namespace EpicRestaurantManager.Controllers
{
    public class ReceivedController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/Receivings
        public IQueryable<Receiving> GetReceived(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetReceived");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.Received;
            }
            else
            {
                var query = from receiving in db.Received
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on receiving.SiteID equals siteUserHasPermissionFor
                            select receiving;
                return query;
            }
        }

        // GET: api/Receivings/5
        [ResponseType(typeof(Receiving))]
        public IHttpActionResult GetReceived(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetReceiving"))
            {
                return BadRequest();
            }
            var query = from receiving in db.Received
                        join user in db.Users on receiving.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && receiving.ID == id
                        select receiving;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Receivings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReceived(int id, Receiving receiving)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, receiving.UILoginUserID, receiving.UILoginPassword, receiving.SiteID, "PutReceiving"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != receiving.ID)
            {
                return BadRequest();
            }

            Receiving r = db.Received.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (r == null)
            {
                return NotFound();
            }
            if (r.SiteID != receiving.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(receiving.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && r.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            VendorProductType vpt = db.VendorProductTypes.Find(receiving.VendorProductTypeID);
            if (vpt == null)
            {
                return BadRequest();
            }
            Inventory i = db.Inventories.AsNoTracking().SingleOrDefault(u => u.ProductTypeID == vpt.ProductTypeID);
            if (i != null)
            {
                if (r.VendorProductTypeID == receiving.VendorProductTypeID && r.Quantity != receiving.Quantity)
                {
                    i.Quantity += receiving.Quantity - r.Quantity;
                    db.Entry(i).State = EntityState.Modified;
                }
                else if (r.VendorProductTypeID != receiving.VendorProductTypeID)
                {
                    VendorProductType vpt2 = db.VendorProductTypes.Find(r.VendorProductTypeID);
                    Inventory i2 = db.Inventories.AsNoTracking().SingleOrDefault(u => u.ProductTypeID == vpt2.ProductTypeID);
                    i2.Quantity -= r.Quantity;
                    db.Entry(i2).State = EntityState.Modified;
                    i.Quantity += receiving.Quantity;
                    db.Entry(i).State = EntityState.Modified;
                }
            }
            else
            {
                Inventory inew = new Inventory();
                inew.EntryByUserID = receiving.UILoginUserID;
                inew.TransactionDateTime = DateTime.Now;
                inew.ProductTypeID = vpt.ProductTypeID;
                inew.Quantity = receiving.Quantity;
                inew.SiteID = receiving.SiteID;
                db.Inventories.Add(inew);
            }
            db.Entry(receiving).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceivedExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Receivings
        [ResponseType(typeof(Receiving))]
        public IHttpActionResult PostReceived(Receiving receiving)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, receiving.UILoginUserID, receiving.UILoginPassword, receiving.SiteID, "PostReceiving"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VendorProductType vpt = db.VendorProductTypes.Find(receiving.VendorProductTypeID);
            if (vpt == null)
            {
                return BadRequest();
            }
            Inventory i = db.Inventories.AsNoTracking().SingleOrDefault(u => u.ProductTypeID == vpt.ProductTypeID);
            if (i != null)
            {
                i.Quantity += receiving.Quantity;
                db.Entry(i).State = EntityState.Modified;
            }
            else
            {
                Inventory inew = new Inventory();
                inew.EntryByUserID = receiving.UILoginUserID;
                inew.TransactionDateTime = DateTime.Now;
                inew.ProductTypeID = vpt.ProductTypeID;
                inew.Quantity = receiving.Quantity;
                inew.SiteID = receiving.SiteID;
                db.Inventories.Add(inew);
            }
            db.Received.Add(receiving);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = receiving.ID }, receiving);
        }

        // DELETE: api/Receivings/5
        [ResponseType(typeof(Receiving))]
        public IHttpActionResult DeleteReceived(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteReceiving"))
            {
                return BadRequest();
            }
            Receiving receiving = db.Received.Find(id);
            if (receiving == null)
            {
                return NotFound();
            }

            if (receiving.SiteID != SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && receiving.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            VendorProductType vpt = db.VendorProductTypes.Find(receiving.VendorProductTypeID);
            if (vpt == null)
            {
                return BadRequest();
            }
            Inventory i = db.Inventories.AsNoTracking().SingleOrDefault(u => u.ProductTypeID == vpt.ProductTypeID);
            if (i != null)
            {
                i.Quantity -= receiving.Quantity;
                db.Entry(i).State = EntityState.Modified;
            }
            db.Received.Remove(receiving);
            db.SaveChanges();

            return Ok(receiving);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReceivedExists(int id)
        {
            return db.Received.Count(e => e.ID == id) > 0;
        }
    }
}