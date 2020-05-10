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
    public class ProductTypeCategoriesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/ProductTypeCategories
        public IQueryable<ProductTypeCategory> GetProductTypeCategories(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetProductTypeCategories");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.ProductTypeCategories;
            }
            else
            {
                var query = from productTypeCategory in db.ProductTypeCategories
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on productTypeCategory.SiteID equals siteUserHasPermissionFor
                            select productTypeCategory;
                return query;
            }
        }

        // GET: api/ProductTypeCategories/5
        [ResponseType(typeof(ProductTypeCategory))]
        public IHttpActionResult GetProductTypeCategory(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetProductTypeCategory"))
            {
                return BadRequest();
            }
            var query = from productTypeCategory in db.ProductTypeCategories
                        join user in db.Users on productTypeCategory.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && productTypeCategory.ID == id
                        select productTypeCategory;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/ProductTypeCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductTypeCategory(int id, ProductTypeCategory productTypeCategory)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, productTypeCategory.UILoginUserID, productTypeCategory.UILoginPassword, productTypeCategory.SiteID, "PutProductTypeCategory"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productTypeCategory.ID)
            {
                return BadRequest();
            }

            ProductTypeCategory ptc = db.ProductTypeCategories.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (ptc == null)
            {
                return NotFound();
            }
            User user = db.Users.Find(productTypeCategory.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && ptc.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            if (ptc.SiteID != productTypeCategory.SiteID)
            {
                return BadRequest();
            }
            db.Entry(productTypeCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeCategoryExists(id))
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

        // POST: api/ProductTypeCategories
        [ResponseType(typeof(ProductTypeCategory))]
        public IHttpActionResult PostProductTypeCategory(ProductTypeCategory productTypeCategory)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, productTypeCategory.UILoginUserID, productTypeCategory.UILoginPassword, productTypeCategory.SiteID, "PostProductTypeCategory"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductTypeCategories.Add(productTypeCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productTypeCategory.ID }, productTypeCategory);
        }

        // DELETE: api/ProductTypeCategories/5
        [ResponseType(typeof(ProductTypeCategory))]
        public IHttpActionResult DeleteProductTypeCategory(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteProductTypeCategory"))
            {
                return BadRequest();
            }
            ProductTypeCategory productTypeCategory = db.ProductTypeCategories.Find(id);
            if (productTypeCategory == null)
            {
                return NotFound();
            }

            if (SiteID != productTypeCategory.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && productTypeCategory.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.ProductTypeCategories.Remove(productTypeCategory);
            db.SaveChanges();

            return Ok(productTypeCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductTypeCategoryExists(int id)
        {
            return db.ProductTypeCategories.Count(e => e.ID == id) > 0;
        }
    }
}