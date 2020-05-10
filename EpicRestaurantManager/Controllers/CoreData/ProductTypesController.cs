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
    public class ProductTypesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/ProductTypes
        public IQueryable<ProductType> GetProductTypes(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetProductTypes");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.ProductTypes;
            }
            else
            {
                var query = from productType in db.ProductTypes
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on productType.SiteID equals siteUserHasPermissionFor
                            select productType;
                return query;
            }
        }

        // GET: api/ProductTypes/5
        [ResponseType(typeof(ProductType))]
        public IHttpActionResult GetProductType(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetProductType"))
            {
                return BadRequest();
            }
            var query = from productType in db.ProductTypes
                        join user in db.Users on productType.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && productType.ID == id
                        select productType;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/ProductTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductType(int id, ProductType productType)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, productType.UILoginUserID, productType.UILoginPassword, productType.SiteID, "PutProductType"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productType.ID)
            {
                return BadRequest();
            }

            ProductType pt = db.ProductTypes.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (pt == null)
            {
                return NotFound();
            }
            if (pt.SiteID != productType.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(productType.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && pt.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(productType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
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

        // POST: api/ProductTypes
        [ResponseType(typeof(ProductType))]
        public IHttpActionResult PostProductType(ProductType productType)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, productType.UILoginUserID, productType.UILoginPassword, productType.SiteID, "PostProductType"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductTypes.Add(productType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productType.ID }, productType);
        }

        // DELETE: api/ProductTypes/5
        [ResponseType(typeof(ProductType))]
        public IHttpActionResult DeleteProductType(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteProductType"))
            {
                return BadRequest();
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }

            if (SiteID != productType.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && productType.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.ProductTypes.Remove(productType);
            db.SaveChanges();

            return Ok(productType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductTypeExists(int id)
        {
            return db.ProductTypes.Count(e => e.ID == id) > 0;
        }
    }
}