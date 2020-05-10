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
    public class PurchaseOrdersController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/PurchaseOrders
        public IQueryable<PurchaseOrder> GetPurchaseOrders(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetPurchaseOrders");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.PurchaseOrders;
            }
            else
            {
                var query = from purchaseOrder in db.PurchaseOrders
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on purchaseOrder.SiteID equals siteUserHasPermissionFor
                            select purchaseOrder;
                return query;
            }
        }

        // GET: api/PurchaseOrders/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult GetPurchaseOrder(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetPurchaseOrder"))
            {
                return BadRequest();
            }
            var query = from purchaseOrder in db.PurchaseOrders
                        join user in db.Users on purchaseOrder.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && purchaseOrder.ID == id
                        select purchaseOrder;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/PurchaseOrders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, purchaseOrder.UILoginUserID, purchaseOrder.UILoginPassword, purchaseOrder.SiteID, "PutPurchaseOrder"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseOrder.ID)
            {
                return BadRequest();
            }

            PurchaseOrder po = db.PurchaseOrders.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (po == null)
            {
                return NotFound();
            }
            if (po.SiteID != purchaseOrder.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(purchaseOrder.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && po.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(purchaseOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderExists(id))
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

        // POST: api/PurchaseOrders
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, purchaseOrder.UILoginUserID, purchaseOrder.UILoginPassword, purchaseOrder.SiteID, "PostPurchaseOrder"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseOrders.Add(purchaseOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseOrder.ID }, purchaseOrder);
        }

        // DELETE: api/PurchaseOrders/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult DeletePurchaseOrder(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeletePurchaseOrder"))
            {
                return BadRequest();
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            if (purchaseOrder.SiteID != SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && purchaseOrder.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.PurchaseOrders.Remove(purchaseOrder);
            db.SaveChanges();

            return Ok(purchaseOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseOrderExists(int id)
        {
            return db.PurchaseOrders.Count(e => e.ID == id) > 0;
        }
    }
}