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
    public class MenuItemIngredientsController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/MenuItemIngredients
        public IQueryable<MenuItemIngredient> GetMenuItemIngredients(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetMenuItemIngredients");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.MenuItemIngredients;
            }
            else
            {
                var query = from menuItemIngredient in db.MenuItemIngredients
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on menuItemIngredient.SiteID equals siteUserHasPermissionFor
                            select menuItemIngredient;
                return query;
            }
        }

        // GET: api/MenuItemIngredients/5
        [ResponseType(typeof(MenuItemIngredient))]
        public IHttpActionResult GetMenuItemIngredient(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetMenuItemIngredient"))
            {
                return BadRequest();
            }
            var query = from menuItemIngredient in db.MenuItemIngredients
                        join user in db.Users on menuItemIngredient.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && menuItemIngredient.ID == id
                        select menuItemIngredient;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/MenuItemIngredients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMenuItemIngredient(int id, MenuItemIngredient menuItemIngredient)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, menuItemIngredient.UILoginUserID, menuItemIngredient.UILoginPassword, menuItemIngredient.SiteID, "PutMenuItemIngredient"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menuItemIngredient.ID)
            {
                return BadRequest();
            }

            MenuItemIngredient mii = db.MenuItemIngredients.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (mii == null)
            {
                return NotFound();
            }
            if (mii.SiteID != menuItemIngredient.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(menuItemIngredient.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && mii.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(menuItemIngredient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemIngredientExists(id))
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

        // POST: api/MenuItemIngredients
        [ResponseType(typeof(MenuItemIngredient))]
        public IHttpActionResult PostMenuItemIngredient(MenuItemIngredient menuItemIngredient)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, menuItemIngredient.UILoginUserID, menuItemIngredient.UILoginPassword, menuItemIngredient.SiteID, "PostMenuItemIngredient"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MenuItemIngredients.Add(menuItemIngredient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = menuItemIngredient.ID }, menuItemIngredient);
        }

        // DELETE: api/MenuItemIngredients/5
        [ResponseType(typeof(MenuItemIngredient))]
        public IHttpActionResult DeleteMenuItemIngredient(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteMenuItemIngredient"))
            {
                return BadRequest();
            }
            MenuItemIngredient menuItemIngredient = db.MenuItemIngredients.Find(id);
            if (menuItemIngredient == null)
            {
                return NotFound();
            }

            if (SiteID != menuItemIngredient.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && menuItemIngredient.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.MenuItemIngredients.Remove(menuItemIngredient);
            db.SaveChanges();

            return Ok(menuItemIngredient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuItemIngredientExists(int id)
        {
            return db.MenuItemIngredients.Count(e => e.ID == id) > 0;
        }
    }
}