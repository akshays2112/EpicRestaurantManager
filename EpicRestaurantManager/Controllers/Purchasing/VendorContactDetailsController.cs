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
    public class VendorContactDetailsController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/VendorContactDetails
        public IQueryable<VendorContactDetail> GetVendorContactDetails(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetVendorContactDetails");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.VendorContactDetails;
            }
            else
            {
                var query = from vendorContactDetail in db.VendorContactDetails
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on vendorContactDetail.SiteID equals siteUserHasPermissionFor
                            select vendorContactDetail;
                return query;
            }
        }

        // GET: api/VendorContactDetails/5
        [ResponseType(typeof(VendorContactDetail))]
        public IHttpActionResult GetVendorContactDetail(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetVendorContactDetail"))
            {
                return BadRequest();
            }
            var query = from vendorContactDetail in db.VendorContactDetails
                        join user in db.Users on vendorContactDetail.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && vendorContactDetail.ID == id
                        select vendorContactDetail;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/VendorContactDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendorContactDetail(int id, VendorContactDetail vendorContactDetail)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, vendorContactDetail.UILoginUserID, vendorContactDetail.UILoginPassword, vendorContactDetail.SiteID, "PutVendorContactDetail"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorContactDetail.ID)
            {
                return BadRequest();
            }

            VendorContactDetail vcd = db.VendorContactDetails.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (vcd == null)
            {
                return NotFound();
            }
            if (vcd.SiteID != vendorContactDetail.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(vendorContactDetail.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && vcd.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(vendorContactDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorContactDetailExists(id))
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

        // POST: api/VendorContactDetails
        [ResponseType(typeof(VendorContactDetail))]
        public IHttpActionResult PostVendorContactDetail(VendorContactDetail vendorContactDetail)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, vendorContactDetail.UILoginUserID, vendorContactDetail.UILoginPassword, vendorContactDetail.SiteID, "PostVendorContactDetail"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendorContactDetails.Add(vendorContactDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendorContactDetail.ID }, vendorContactDetail);
        }

        // DELETE: api/VendorContactDetails/5
        [ResponseType(typeof(VendorContactDetail))]
        public IHttpActionResult DeleteVendorContactDetail(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteVendorContactDetail"))
            {
                return BadRequest();
            }
            VendorContactDetail vendorContactDetail = db.VendorContactDetails.Find(id);
            if (vendorContactDetail == null)
            {
                return NotFound();
            }

            if (vendorContactDetail.SiteID != SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && vendorContactDetail.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.VendorContactDetails.Remove(vendorContactDetail);
            db.SaveChanges();

            return Ok(vendorContactDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorContactDetailExists(int id)
        {
            return db.VendorContactDetails.Count(e => e.ID == id) > 0;
        }
    }
}