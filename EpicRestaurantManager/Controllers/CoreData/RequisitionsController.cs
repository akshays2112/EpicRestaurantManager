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
    public class RequisitionsController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/Requisitions
        public IQueryable<Requisition> GetRequisitions(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetRequisitions");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.Requisitions;
            }
            else
            {
                var query = from requisition in db.Requisitions
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on requisition.SiteID equals siteUserHasPermissionFor
                            select requisition;
                return query;
            }
        }

        // GET: api/Requisitions/5
        [ResponseType(typeof(Requisition))]
        public IHttpActionResult GetRequisition(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "GetRequisition"))
            {
                return BadRequest();
            }
            var query = from requisition in db.Requisitions
                        join user in db.Users on requisition.EntryByUserID equals user.ID
                        join userSite in db.UserSites on user.ID equals userSite.UserID
                        where ((userSite.UserID == UILoginUserID && userSite.ID == SiteID) || user.IsRootUser) && requisition.ID == id
                        select requisition;
            if (query.Count() > 0)
            {
                return Ok(query.SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Requisitions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequisition(int id, Requisition requisition)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, requisition.UILoginUserID, requisition.UILoginPassword, requisition.SiteID, "PutRequisition"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requisition.ID)
            {
                return BadRequest();
            }

            Requisition req = db.Requisitions.AsNoTracking().SingleOrDefault(p => p.ID == id);
            if (req == null)
            {
                return NotFound();
            }
            if (req.SiteID != requisition.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(requisition.UILoginUserID);
            if (!user.IsRootUser && !user.IsSiteAdmin && req.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Entry(requisition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequisitionExists(id))
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

        // POST: api/Requisitions
        [ResponseType(typeof(Requisition))]
        public IHttpActionResult PostRequisition(Requisition requisition)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, requisition.UILoginUserID, requisition.UILoginPassword, requisition.SiteID, "PostRequisition"))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Requisitions.Add(requisition);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = requisition.ID }, requisition);
        }

        // DELETE: api/Requisitions/5
        [ResponseType(typeof(Requisition))]
        public IHttpActionResult DeleteRequisition(int id, int UILoginUserID, string UILoginPassword, int SiteID)
        {
            if (!Global.CheckUserIDAndPasswordWithSiteID(db, UILoginUserID, UILoginPassword, SiteID, "DeleteRequisition"))
            {
                return BadRequest();
            }
            Requisition requisition = db.Requisitions.Find(id);
            if (requisition == null)
            {
                return NotFound();
            }

            if (SiteID != requisition.SiteID)
            {
                return BadRequest();
            }
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return BadRequest();
            }
            if (!user.IsRootUser && !user.IsSiteAdmin && requisition.EntryByUserID != user.ID)
            {
                return BadRequest();
            }
            db.Requisitions.Remove(requisition);
            db.SaveChanges();

            return Ok(requisition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequisitionExists(int id)
        {
            return db.Requisitions.Count(e => e.ID == id) > 0;
        }
    }
}