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
    public class MenuItemsController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/MenuItems
        public IQueryable<MenuItem> GetMenuItems(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetMenuItems");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.MenuItems;
            }
            else
            {
                var query = from menuItem in db.MenuItems
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on menuItem.SiteID equals siteUserHasPermissionFor
                            select menuItem;
                return query;
            }
        }

        // GET: api/MenuItems/5
        [ResponseType(typeof(MenuItem))]
        public IHttpActionResult GetMenuItem(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetMenuItem"))
            {
                return BadRequest();
            }
            var query = from menuItem in db.MenuItems
                        join user in db.Users on menuItem.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && menuItem.ID == id
                        select menuItem;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/MenuItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMenuItem(int id, MenuItem menuItem)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, menuItem.UILoginUserID, menuItem.UILoginPassword, menuItem.SiteID, "PutMenuItem"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menuItem.ID)
            {
                return BadRequest();
            }

            MenuItem mi = db.MenuItems.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (mi == null)
            {
                return NotFound();
            }
            if (mi.SiteID != menuItem.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(menuItem.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && mi.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(menuItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
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

        // POST: api/MenuItems
        [ResponseType(typeof(MenuItem))]
        public IHttpActionResult PostMenuItem(MenuItem menuItem)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, menuItem.UILoginUserID, menuItem.UILoginPassword, menuItem.SiteID, "PostMenuItem"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MenuItems.Add(menuItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = menuItem.ID }, menuItem);
        }

        // DELETE: api/MenuItems/5
        [ResponseType(typeof(MenuItem))]
        public IHttpActionResult DeleteMenuItem(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteMenuItem"))
            {
                return BadRequest();
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            if (SiteID != menuItem.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && menuItem.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.MenuItems.Remove(menuItem);
            db.SaveChanges();

            return Ok(menuItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuItemExists(int id)
        {
            return db.MenuItems.Count(e => e.ID == id) > 0;
        }
    }
}