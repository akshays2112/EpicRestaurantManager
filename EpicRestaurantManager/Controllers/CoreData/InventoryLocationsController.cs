using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using EpicRestaurantManager.Models;

namespace EpicRestaurantManager.Controllers
{
    public class InventoryLocationsController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/InventoryLocations
        public IQueryable<InventoryLocation> GetInventoryLocations(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetInventoryLocations");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.InventoryLocations;
            }
            else
            {
                var query = from inventoryLocation in db.InventoryLocations
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on inventoryLocation.SiteID equals siteUserHasPermissionFor
                            select inventoryLocation;
                return query;
            }
        }

        // GET: api/InventoryLocations/5
        [ResponseType(typeof(InventoryLocation))]
        public IHttpActionResult GetInventoryLocation(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetInventoryLocation"))
            {
                return BadRequest();
            }
            var query = from inventoryLocation in db.InventoryLocations
                        join user in db.Users on inventoryLocation.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && inventoryLocation.ID == id
                        select inventoryLocation;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/InventoryLocations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventoryLocation(int id, InventoryLocation inventoryLocation)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, inventoryLocation.UILoginUserID, inventoryLocation.UILoginPassword, inventoryLocation.SiteID, "PutInventoryLocation"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventoryLocation.ID)
            {
                return BadRequest();
            }

            InventoryLocation i = db.InventoryLocations.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (i == null)
            {
                return NotFound();
            }
            if (i.SiteID != inventoryLocation.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(inventoryLocation.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && i.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(inventoryLocation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryLocationExists(id))
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

        // POST: api/InventoryLocations
        [ResponseType(typeof(InventoryLocation))]
        public IHttpActionResult PostInventoryLocation(InventoryLocation inventoryLocation)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, inventoryLocation.UILoginUserID, inventoryLocation.UILoginPassword, inventoryLocation.SiteID, "PostInventoryLocation"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InventoryLocations.Add(inventoryLocation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = inventoryLocation.ID }, inventoryLocation);
        }

        // DELETE: api/InventoryLocations/5
        [ResponseType(typeof(InventoryLocation))]
        public IHttpActionResult DeleteInventoryLocation(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteInventoryLocation"))
            {
                return BadRequest();
            }
            InventoryLocation inventoryLocation = db.InventoryLocations.Find(id);
            if (inventoryLocation == null)
            {
                return NotFound();
            }

            if (inventoryLocation.SiteID != SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && inventoryLocation.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.InventoryLocations.Remove(inventoryLocation);
            db.SaveChanges();

            return Ok(inventoryLocation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InventoryLocationExists(int id)
        {
            return db.InventoryLocations.Count(e => e.ID == id) > 0;
        }
    }
}