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
    public class VendorsController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/Vendors
        public IQueryable<Vendor> GetVendors(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetVendors");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.Vendors;
            }
            else
            {
                var query = from vendor in db.Vendors
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on vendor.SiteID equals siteUserHasPermissionFor
                            select vendor;
                return query;
            }
        }

        // GET: api/Vendors/5
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult GetVendor(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetVendor"))
            {
                return BadRequest();
            }
            var query = from vendor in db.Vendors
                        join user in db.Users on vendor.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && vendor.ID == id
                        select vendor;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Vendors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendor(int id, Vendor vendor)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, vendor.UILoginUserID, vendor.UILoginPassword, vendor.SiteID, "PutVendor"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendor.ID)
            {
                return BadRequest();
            }

            Vendor v = db.Vendors.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (v == null)
            {
                return NotFound();
            }
            if (v.SiteID != vendor.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(vendor.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && v.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(vendor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult PostVendor(Vendor vendor)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, vendor.UILoginUserID, vendor.UILoginPassword, vendor.SiteID, "PostVendor"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vendors.Add(vendor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendor.ID }, vendor);
        }

        // DELETE: api/Vendors/5
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult DeleteVendor(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteVendor"))
            {
                return BadRequest();
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            if (vendor.SiteID != SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && vendor.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Vendors.Remove(vendor);
            db.SaveChanges();

            return Ok(vendor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorExists(int id)
        {
            return db.Vendors.Count(e => e.ID == id) > 0;
        }
    }
}