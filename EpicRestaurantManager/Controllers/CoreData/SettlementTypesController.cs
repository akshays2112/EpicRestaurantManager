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
    public class SettlementTypesController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/SettlementTypes
        public IQueryable<SettlementType> GetSettlementTypes(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetSettlementTypes");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.SettlementTypes;
            }
            else
            {
                var query = from settlementType in db.SettlementTypes
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on settlementType.SiteID equals siteUserHasPermissionFor
                            select settlementType;
                return query;
            }
        }

        // GET: api/SettlementTypes/5
        [ResponseType(typeof(SettlementType))]
        public IHttpActionResult GetSettlementType(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetSettlementType"))
            {
                return BadRequest();
            }
            var query = from settlementType in db.SettlementTypes
                        join user in db.Users on settlementType.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && settlementType.ID == id
                        select settlementType;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/SettlementTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSettlementType(int id, SettlementType settlementType)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, settlementType.UILoginUserID, settlementType.UILoginPassword, settlementType.SiteID, "PutSettlementType"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != settlementType.ID)
            {
                return BadRequest();
            }

            SettlementType st = db.SettlementTypes.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (st == null)
            {
                return NotFound();
            }
            if (st.SiteID != settlementType.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(settlementType.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && st.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(settlementType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettlementTypeExists(id))
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

        // POST: api/SettlementTypes
        [ResponseType(typeof(SettlementType))]
        public IHttpActionResult PostSettlementType(SettlementType settlementType)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, settlementType.UILoginUserID, settlementType.UILoginPassword, settlementType.SiteID, "PostSettlementType"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SettlementTypes.Add(settlementType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = settlementType.ID }, settlementType);
        }

        // DELETE: api/SettlementTypes/5
        [ResponseType(typeof(SettlementType))]
        public IHttpActionResult DeleteSettlementType(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteSettlementType"))
            {
                return BadRequest();
            }
            SettlementType settlementType = db.SettlementTypes.Find(id);
            if (settlementType == null)
            {
                return NotFound();
            }

            if (SiteID != settlementType.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && settlementType.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.SettlementTypes.Remove(settlementType);
            db.SaveChanges();

            return Ok(settlementType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SettlementTypeExists(int id)
        {
            return db.SettlementTypes.Count(e => e.ID == id) > 0;
        }
    }
}