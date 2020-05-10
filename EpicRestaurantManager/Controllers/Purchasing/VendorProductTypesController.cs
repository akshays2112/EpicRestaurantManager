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
    public class VendorProductTypesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/VendorProductTypes
        public IQueryable<VendorProductType> GetVendorProductTypes(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetVendorProductTypes");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.VendorProductTypes;
            }
            else
            {
                var query = from vendorProductType in db.VendorProductTypes
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on vendorProductType.SiteID equals siteUserHasPermissionFor
                            select vendorProductType;
                return query;
            }
        }

        // GET: api/VendorProductTypes/5
        [ResponseType(typeof(VendorProductType))]
        public IHttpActionResult GetVendorProductType(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetVendorProductType"))
            {
                return BadRequest();
            }
            var query = from vendorProductType in db.VendorProductTypes
                        join user in db.Users on vendorProductType.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && vendorProductType.ID == id
                        select vendorProductType;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/VendorProductTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendorProductType(int id, VendorProductType vendorProductType)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, vendorProductType.UILoginUserID, vendorProductType.UILoginPassword, vendorProductType.SiteID, "PutVendorProductType"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorProductType.ID)
            {
                return BadRequest();
            }

            VendorProductType vpt = db.VendorProductTypes.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (vpt == null)
            {
                return NotFound();
            }
            if (vpt.SiteID != vendorProductType.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(vendorProductType.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && vpt.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(vendorProductType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorProductTypeExists(id))
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

        // POST: api/VendorProductTypes
        [ResponseType(typeof(VendorProductType))]
        public IHttpActionResult PostVendorProductType(VendorProductType vendorProductType)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, vendorProductType.UILoginUserID, vendorProductType.UILoginPassword, vendorProductType.SiteID, "PostVendorProductType"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendorProductTypes.Add(vendorProductType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendorProductType.ID }, vendorProductType);
        }

        // DELETE: api/VendorProductTypes/5
        [ResponseType(typeof(VendorProductType))]
        public IHttpActionResult DeleteVendorProductType(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteVendorProductType"))
            {
                return BadRequest();
            }
            VendorProductType vendorProductType = db.VendorProductTypes.Find(id);
            if (vendorProductType == null)
            {
                return NotFound();
            }

            if (vendorProductType.SiteID != SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && vendorProductType.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.VendorProductTypes.Remove(vendorProductType);
            db.SaveChanges();

            return Ok(vendorProductType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorProductTypeExists(int id)
        {
            return db.VendorProductTypes.Count(e => e.ID == id) > 0;
        }
    }
}