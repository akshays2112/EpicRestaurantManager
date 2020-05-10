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
    public class VendorProductTypePricesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/VendorProductTypePrices
        public IQueryable<VendorProductTypePrice> GetVendorProductTypePrices(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetVendorProductTypePrices");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.VendorProductTypePrices;
            }
            else
            {
                var query = from vendorProductTypePrice in db.VendorProductTypePrices
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on vendorProductTypePrice.SiteID equals siteUserHasPermissionFor
                            select vendorProductTypePrice;
                return query;
            }
        }

        // GET: api/VendorProductTypePrices/5
        [ResponseType(typeof(VendorProductTypePrice))]
        public IHttpActionResult GetVendorProductTypePrice(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetVendorProductTypePrice"))
            {
                return BadRequest();
            }
            var query = from vendorProductTypePrice in db.VendorProductTypePrices
                        join user in db.Users on vendorProductTypePrice.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && vendorProductTypePrice.ID == id
                        select vendorProductTypePrice;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/VendorProductTypePrices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendorProductTypePrice(int id, VendorProductTypePrice vendorProductTypePrice)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, vendorProductTypePrice.UILoginUserID, vendorProductTypePrice.UILoginPassword, vendorProductTypePrice.SiteID, "PutVendorProductTypePrice"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorProductTypePrice.ID)
            {
                return BadRequest();
            }

            VendorProductTypePrice vptp = db.VendorProductTypePrices.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (vptp == null)
            {
                return NotFound();
            }
            if (vptp.SiteID != vendorProductTypePrice.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(vendorProductTypePrice.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && vptp.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(vendorProductTypePrice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorProductTypePriceExists(id))
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

        // POST: api/VendorProductTypePrices
        [ResponseType(typeof(VendorProductTypePrice))]
        public IHttpActionResult PostVendorProductTypePrice(VendorProductTypePrice vendorProductTypePrice)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, vendorProductTypePrice.UILoginUserID, vendorProductTypePrice.UILoginPassword, vendorProductTypePrice.SiteID, "PostVendorProductTypePrice"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendorProductTypePrices.Add(vendorProductTypePrice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendorProductTypePrice.ID }, vendorProductTypePrice);
        }

        // DELETE: api/VendorProductTypePrices/5
        [ResponseType(typeof(VendorProductTypePrice))]
        public IHttpActionResult DeleteVendorProductTypePrice(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteVendorProductTypePrice"))
            {
                return BadRequest();
            }
            VendorProductTypePrice vendorProductTypePrice = db.VendorProductTypePrices.Find(id);
            if (vendorProductTypePrice == null)
            {
                return NotFound();
            }

            if (vendorProductTypePrice.SiteID != SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && vendorProductTypePrice.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.VendorProductTypePrices.Remove(vendorProductTypePrice);
            db.SaveChanges();

            return Ok(vendorProductTypePrice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorProductTypePriceExists(int id)
        {
            return db.VendorProductTypePrices.Count(e => e.ID == id) > 0;
        }
    }
}