using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EpicRestaurantManager.Models;

namespace EpicRestaurantManager.Controllers
{
    public class SitesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/Sites
        public IQueryable<Site> GetSites(int UILoginUserID, string UILoginPassword, int SiteID)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetSites");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.Sites;
            }
            else
            {
                var query = from site in db.Sites
                            join userSite in db.UserSites on site.ID equals userSite.SiteID
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on userSite.SiteID equals siteUserHasPermissionFor
                            where userSite.UserID == UILoginUserID
                            select site;
                return query;
            }
        }

        // GET: api/Sites/5
        [ResponseType(typeof(Site))]
        public IHttpActionResult GetSite(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetSite"))
            {
                return BadRequest();
            }
            var query = from site in db.Sites
                        join userSite in db.UserSites on site.ID equals userSite.SiteID
                        join user in db.Users on userSite.UserID equals user.ID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && site.ID == id
                        select site;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Sites/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSite(int id, Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != site.ID)
            {
                return BadRequest();
            }

            if (!Global.CheckUserIDAndPasswordWithSiteID(db, site.UILoginUserID, site.UILoginPassword, site.ID, "PutSite"))
            {
                return BadRequest();
            }

            UserSite userSite = db.UserSites.Where(u => u.SiteID == site.ID && u.UserID == site.UILoginUserID).SingleOrDefault();
            if (userSite == null)
            {
                return BadRequest();
            }
            User user = db.Users.Find(site.UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsSiteAdmin && !user.IsRootUser)
            {
                return BadRequest();
            }
            db.Entry(site).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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

        // POST: api/Sites
        [ResponseType(typeof(Site))]
        public IHttpActionResult PostSite(Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!Global.CheckUserIDAndPasswordForSiteAdd(db, site.UILoginUserID, site.UILoginPassword))
            {
                return BadRequest();
            }
            if (db.Sites.Where(u => u.NameOfBusiness == site.NameOfBusiness).Count() > 0)
            {
                return BadRequest();
            }
            db.Sites.Add(site);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = site.ID }, site);
        }

        // DELETE: api/Sites/5
        [ResponseType(typeof(Site))]
        public IHttpActionResult DeleteSite(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteSite"))
            {
                return BadRequest();
            }
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return NotFound();
            }
            UserSite userSite = db.UserSites.Find(site.ID);
            if (userSite.UserID != UILoginUserID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsSiteAdmin)
            {
                return BadRequest();
            }
            db.Sites.Remove(site);
            db.SaveChanges();

            return Ok(site);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SiteExists(int id)
        {
            return db.Sites.Count(e => e.ID == id) > 0;
        }
    }
}