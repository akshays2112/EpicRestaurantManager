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
    public class MenuCategoriesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/MenuCategories
        public IQueryable<MenuCategory> GetMenuCategories(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetMenuCategories");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.MenuCategories;
            }
            else
            {
                var query = from menuCategory in db.MenuCategories
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on menuCategory.SiteID equals siteUserHasPermissionFor
                            select menuCategory;
                return query;
            }
        }

        // GET: api/MenuCategories/5
        [ResponseType(typeof(MenuCategory))]
        public IHttpActionResult GetMenuCategory(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetMenuCategory"))
            {
                return BadRequest();
            }
            var query = from menuCategory in db.MenuCategories
                        join user in db.Users on menuCategory.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && menuCategory.ID == id
                        select menuCategory;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/MenuCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMenuCategory(int id, MenuCategory menuCategory)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, menuCategory.UILoginUserID, menuCategory.UILoginPassword, menuCategory.SiteID, "PutMenuCategory"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menuCategory.ID)
            {
                return BadRequest();
            }

            MenuCategory mc = db.MenuCategories.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (mc == null)
            {
                return NotFound();
            }
            if (mc.SiteID != menuCategory.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(menuCategory.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && mc.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(menuCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuCategoryExists(id))
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

        // POST: api/MenuCategories
        [ResponseType(typeof(MenuCategory))]
        public IHttpActionResult PostMenuCategory(MenuCategory menuCategory)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, menuCategory.UILoginUserID, menuCategory.UILoginPassword, menuCategory.SiteID, "PostMenuCategory"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MenuCategories.Add(menuCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = menuCategory.ID }, menuCategory);
        }

        // DELETE: api/MenuCategories/5
        [ResponseType(typeof(MenuCategory))]
        public IHttpActionResult DeleteMenuCategory(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteMenuCategory"))
            {
                return BadRequest();
            }
            MenuCategory menuCategory = db.MenuCategories.Find(id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            if (SiteID != menuCategory.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && menuCategory.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.MenuCategories.Remove(menuCategory);
            db.SaveChanges();

            return Ok(menuCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuCategoryExists(int id)
        {
            return db.MenuCategories.Count(e => e.ID == id) > 0;
        }
    }
}