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
    public class InventoriesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/Inventories
        public IQueryable<Inventory> GetInventories(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetInventories");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.Inventories;
            }
            else
            {
                var query = from inventory in db.Inventories
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on inventory.SiteID equals siteUserHasPermissionFor
                            select inventory;
                return query;
            }
        }

        // GET: api/Inventories/5
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult GetInventory(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetInventory"))
            {
                return BadRequest();
            }
            var query = from inventory in db.Inventories
                        join user in db.Users on inventory.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && inventory.ID == id
                        select inventory;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Inventories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(int id, Inventory inventory)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, inventory.UILoginUserID, inventory.UILoginPassword, inventory.SiteID, "PutInventory"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventory.ID)
            {
                return BadRequest();
            }

            Inventory i = db.Inventories.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (i == null)
            {
                return NotFound();
            }
            if (i.SiteID != inventory.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(inventory.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && i.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            if (i.ProductTypeID != inventory.ProductTypeID)
            {
                return BadRequest();
            }
            db.Entry(inventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventories
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult PostInventory(Inventory inventory)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, inventory.UILoginUserID, inventory.UILoginPassword, inventory.SiteID, "PostInventory"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inventory i = db.Inventories.AsNoTracking().SingleOrDefault(p => p.ProductTypeID == inventory.ProductTypeID);
            if (i != null)
            {
                return BadRequest();
            }
            db.Inventories.Add(inventory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = inventory.ID }, inventory);
        }

        // DELETE: api/Inventories/5
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult DeleteInventory(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteInventory"))
            {
                return BadRequest();
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }

            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && inventory.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Inventories.Remove(inventory);
            db.SaveChanges();

            return Ok(inventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InventoryExists(int id)
        {
            return db.Inventories.Count(e => e.ID == id) > 0;
        }
    }
}