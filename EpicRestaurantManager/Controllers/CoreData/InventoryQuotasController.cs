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
    public class InventoryQuotasController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/InventoryQuotas
        public IQueryable<InventoryQuota> GetInventoryQuotas(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetInventoryQuotas");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.InventoryQuotas;
            }
            else
            {
                var query = from inventoryQuota in db.InventoryQuotas
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on inventoryQuota.SiteID equals siteUserHasPermissionFor
                            select inventoryQuota;
                return query;
            }
        }

        // GET: api/InventoryQuotas/5
        [ResponseType(typeof(InventoryQuota))]
        public IHttpActionResult GetInventoryQuota(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetInventoryQuota"))
            {
                return BadRequest();
            }
            var query = from inventoryQuota in db.InventoryQuotas
                        join user in db.Users on inventoryQuota.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && inventoryQuota.ID == id
                        select inventoryQuota;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/InventoryQuotas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventoryQuota(int id, InventoryQuota inventoryQuota)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, inventoryQuota.UILoginUserID, inventoryQuota.UILoginPassword, inventoryQuota.SiteID, "PutInventoryQuota"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventoryQuota.ID)
            {
                return BadRequest();
            }

            InventoryQuota i = db.InventoryQuotas.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (i == null)
            {
                return NotFound();
            }
            if (i.SiteID != inventoryQuota.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(inventoryQuota.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && i.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(inventoryQuota).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryQuotaExists(id))
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

        // POST: api/InventoryQuotas
        [ResponseType(typeof(InventoryQuota))]
        public IHttpActionResult PostInventoryQuota(InventoryQuota inventoryQuota)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, inventoryQuota.UILoginUserID, inventoryQuota.UILoginPassword, inventoryQuota.SiteID, "PostInventoryQuota"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InventoryQuotas.Add(inventoryQuota);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = inventoryQuota.ID }, inventoryQuota);
        }

        // DELETE: api/InventoryQuotas/5
        [ResponseType(typeof(InventoryQuota))]
        public IHttpActionResult DeleteInventoryQuota(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteInventoryQuota"))
            {
                return BadRequest();
            }
            InventoryQuota inventoryQuota = db.InventoryQuotas.Find(id);
            if (inventoryQuota == null)
            {
                return NotFound();
            }

            if (inventoryQuota.SiteID != SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && inventoryQuota.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.InventoryQuotas.Remove(inventoryQuota);
            db.SaveChanges();

            return Ok(inventoryQuota);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InventoryQuotaExists(int id)
        {
            return db.InventoryQuotas.Count(e => e.ID == id) > 0;
        }
    }
}