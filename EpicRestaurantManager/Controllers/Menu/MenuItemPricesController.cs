using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using EpicRestaurantManager.Models;

namespace EpicRestaurantManager.Controllers.Menu
{
    public class MenuItemPricesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/MenuItemPrices
        public IQueryable<MenuItemPrice> GetMenuItemPrices(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetMenuItemPrices");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.MenuItemPrices;
            }
            else
            {
                var query = from menuItemPrice in db.MenuItemPrices
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on menuItemPrice.SiteID equals siteUserHasPermissionFor
                            select menuItemPrice;
                return query;
            }
        }

        // GET: api/MenuItemPrices/5
        [ResponseType(typeof(MenuItemPrice))]
        public IHttpActionResult GetMenuItemPrice(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetMenuItemPrice"))
            {
                return BadRequest();
            }
            var query = from menuItemPrice in db.MenuItemPrices
                        join user in db.Users on menuItemPrice.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && menuItemPrice.ID == id
                        select menuItemPrice;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/MenuItemPrices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMenuItemPrice(int id, MenuItemPrice menuItemPrice)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, menuItemPrice.UILoginUserID, menuItemPrice.UILoginPassword, menuItemPrice.SiteID, "PutMenuItemPrice"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menuItemPrice.ID)
            {
                return BadRequest();
            }

            MenuItemPrice mip = db.MenuItemPrices.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (mip == null)
            {
                return NotFound();
            }
            if (mip.SiteID != menuItemPrice.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(menuItemPrice.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && mip.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(menuItemPrice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemPriceExists(id))
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

        // POST: api/MenuItemPrices
        [ResponseType(typeof(MenuItemPrice))]
        public IHttpActionResult PostMenuItemPrice(MenuItemPrice menuItemPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MenuItemPrices.Add(menuItemPrice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = menuItemPrice.ID }, menuItemPrice);
        }

        // DELETE: api/MenuItemPrices/5
        [ResponseType(typeof(MenuItemPrice))]
        public IHttpActionResult DeleteMenuItemPrice(int id)
        {
            MenuItemPrice menuItemPrice = db.MenuItemPrices.Find(id);
            if (menuItemPrice == null)
            {
                return NotFound();
            }

            db.MenuItemPrices.Remove(menuItemPrice);
            db.SaveChanges();

            return Ok(menuItemPrice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuItemPriceExists(int id)
        {
            return db.MenuItemPrices.Count(e => e.ID == id) > 0;
        }
    }
}