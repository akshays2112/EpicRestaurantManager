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
    public class ProductMeasurementTypesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/ProductMeasurementTypes
        public IQueryable<ProductMeasurementType> GetProductMeasurementTypes(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetProductMeasurementTypes");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.ProductMeasurementTypes;
            }
            else
            {
                var query = from productMeasurementType in db.ProductMeasurementTypes
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on productMeasurementType.SiteID equals siteUserHasPermissionFor
                            select productMeasurementType;
                return query;
            }
        }

        // GET: api/ProductMeasurementTypes/5
        [ResponseType(typeof(ProductMeasurementType))]
        public IHttpActionResult GetProductMeasurementType(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetProductMeasurementType"))
            {
                return BadRequest();
            }
            var query = from productMeasurementType in db.ProductMeasurementTypes
                        join user in db.Users on productMeasurementType.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && productMeasurementType.ID == id
                        select productMeasurementType;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/ProductMeasurementTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductMeasurementType(int id, ProductMeasurementType productMeasurementType)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, productMeasurementType.UILoginUserID, productMeasurementType.UILoginPassword, productMeasurementType.SiteID, "PutProductMeasurementType"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productMeasurementType.ID)
            {
                return BadRequest();
            }

            ProductMeasurementType pmt = db.ProductMeasurementTypes.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (pmt == null)
            {
                return NotFound();
            }
            if (pmt.SiteID != productMeasurementType.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(productMeasurementType.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && pmt.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(productMeasurementType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductMeasurementTypeExists(id))
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

        // POST: api/ProductMeasurementTypes
        [ResponseType(typeof(ProductMeasurementType))]
        public IHttpActionResult PostProductMeasurementType(ProductMeasurementType productMeasurementType)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, productMeasurementType.UILoginUserID, productMeasurementType.UILoginPassword, productMeasurementType.SiteID, "PostProductMeasurementType"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductMeasurementTypes.Add(productMeasurementType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productMeasurementType.ID }, productMeasurementType);
        }

        // DELETE: api/ProductMeasurementTypes/5
        [ResponseType(typeof(ProductMeasurementType))]
        public IHttpActionResult DeleteProductMeasurementType(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteProductMeasurementType"))
            {
                return BadRequest();
            }
            ProductMeasurementType productMeasurementType = db.ProductMeasurementTypes.Find(id);
            if (productMeasurementType == null)
            {
                return NotFound();
            }

            if (productMeasurementType.SiteID != SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && productMeasurementType.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.ProductMeasurementTypes.Remove(productMeasurementType);
            db.SaveChanges();

            return Ok(productMeasurementType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductMeasurementTypeExists(int id)
        {
            return db.ProductMeasurementTypes.Count(e => e.ID == id) > 0;
        }
    }
}