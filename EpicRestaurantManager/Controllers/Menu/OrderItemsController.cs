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
    public class OrderItemsController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/OrderItems
        public IQueryable<OrderItem> GetOrderItems(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetOrderItems");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.OrderItems;
            }
            else
            {
                var query = from orderItem in db.OrderItems
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on orderItem.SiteID equals siteUserHasPermissionFor
                            select orderItem;
                return query;
            }
        }

        // GET: api/OrderItems/5
        [ResponseType(typeof(OrderItem))]
        public IHttpActionResult GetOrderItem(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetOrderItem"))
            {
                return BadRequest();
            }
            var query = from orderItem in db.OrderItems
                        join user in db.Users on orderItem.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && orderItem.ID == id
                        select orderItem;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/OrderItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderItem(int id, OrderItem orderItem)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, orderItem.UILoginUserID, orderItem.UILoginPassword, orderItem.SiteID, "PutOrderItem"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderItem.ID)
            {
                return BadRequest();
            }

            OrderItem oi = db.OrderItems.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (oi == null)
            {
                return NotFound();
            }
            if (oi.SiteID != orderItem.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(orderItem.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && oi.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(orderItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
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

        // POST: api/OrderItems
        [ResponseType(typeof(OrderItem))]
        public IHttpActionResult PostOrderItem(OrderItem orderItem)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, orderItem.UILoginUserID, orderItem.UILoginPassword, orderItem.SiteID, "PostOrderItem"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderItems.Add(orderItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderItem.ID }, orderItem);
        }

        // DELETE: api/OrderItems/5
        [ResponseType(typeof(OrderItem))]
        public IHttpActionResult DeleteOrderItem(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteOrderItem"))
            {
                return BadRequest();
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            if (SiteID != orderItem.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && orderItem.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.OrderItems.Remove(orderItem);
            db.SaveChanges();

            return Ok(orderItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderItemExists(int id)
        {
            return db.OrderItems.Count(e => e.ID == id) > 0;
        }
    }
}