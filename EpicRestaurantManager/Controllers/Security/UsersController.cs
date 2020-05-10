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
    public class UsersController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/Users
        public IQueryable<User> GetUsers(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetUsers");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.Users;
            }
            else
            {
                var query = from user in db.Users
                            join userSite in db.UserSites on user.ID equals userSite.UserID
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on userSite.SiteID equals siteUserHasPermissionFor
                            where userSite.UserID == UILoginUserID && user.IsSiteAdmin
                            select user;
                return query;
            }
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id, int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetUser");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return Ok(db.Users.Find(id));
            }
            else
            {
                var query = from user in db.Users
                            join userSite in db.UserSites on user.ID equals userSite.UserID
                            where userSite.UserID == UILoginUserID && user.ID == id
                            select user;
                if (query.Count() > 0)
                {
                    return Ok(query.SingleOrDefault());
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string UserName, string Password)
        {
            try
            {
                User user = db.Users.Where(u => (u.UserName == UserName || u.EmailAddress == UserName) && u.Password == Password).SingleOrDefault();
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }

        public class GetUserSiteResponse
        {
            public int SiteID;
            public string SiteName;
            public bool IsSiteAdmin;
        }

        public IQueryable<GetUserSiteResponse> GetUser(string UserName, string Password, string Dummy)
        {
            try
            {
                if (db.Users.Where(u => (u.UserName == UserName || u.EmailAddress == UserName) && u.Password == Password).SingleOrDefault().IsRootUser)
                {
                    var query = from onesite in db.Sites
                                select new GetUserSiteResponse { SiteID = onesite.ID, IsSiteAdmin = true, SiteName = onesite.NameOfBusiness };
                    return query;
                }
                else
                {
                    var query = from user in db.Users
                                join userSite in db.UserSites on user.ID equals userSite.UserID
                                join site in db.Sites on userSite.SiteID equals site.ID
                                where (user.UserName == UserName || user.EmailAddress == UserName) && user.Password == Password
                                select new GetUserSiteResponse { SiteID = userSite.SiteID, IsSiteAdmin = user.IsSiteAdmin, SiteName = site.NameOfBusiness };
                    return query;
                }
            }
            catch
            {
                return null;
            }
        }

        public class GetControllerActionPermission
        {
            public bool Allow;
            public string ControllerActionName;
        }

        public List<GetControllerActionPermission> GetUser(string UserName, string Password, string Dummy, string XDummy)
        {
            try
            {
                var query1 = from user in db.Users
                             join userControllerActionPermission in db.UserControllerActionPermissions on user.ID equals userControllerActionPermission.UserID
                             join controllerAction in db.ControllerActions on userControllerActionPermission.ControllerActionID equals controllerAction.ID
                             where (user.UserName == UserName || user.EmailAddress == UserName) && user.Password == Password
                             select new GetControllerActionPermission { Allow = userControllerActionPermission.Allow, ControllerActionName = controllerAction.ControllerActionName };

                var query2 = from user in db.Users
                             join userInGroup in db.UserInGroups on user.ID equals userInGroup.UserID
                             join userGroupControllerActionPermission in db.UserGroupControllerActionPermissions on userInGroup.UserGroupID equals userGroupControllerActionPermission.UserGroupID
                             join controllerActionUserGroup in db.ControllerActions on userGroupControllerActionPermission.ControllerActionID equals controllerActionUserGroup.ID
                             where (user.UserName == UserName || user.EmailAddress == UserName) && user.Password == Password
                             select new GetControllerActionPermission
                             {
                                 Allow = userGroupControllerActionPermission.Allow,
                                 ControllerActionName = controllerActionUserGroup.ControllerActionName
                             };


                List<GetControllerActionPermission> list = new List<GetControllerActionPermission>();
                List<GetControllerActionPermission> query1list = query1.ToList();
                List<GetControllerActionPermission> query2list = query2.ToList();
                foreach (GetControllerActionPermission x1 in query1list)
                {
                    bool found = false;
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (x1.ControllerActionName == list[i].ControllerActionName)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        list.Add(x1);
                    }
                }
                foreach (GetControllerActionPermission x2 in query2list)
                {
                    bool found = false;
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (x2.ControllerActionName == list[i].ControllerActionName)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        list.Add(x2);
                    }
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, user.UILoginUserID, user.UILoginPassword, "PutUser");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return BadRequest();
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                db.Entry(user).State = EntityState.Modified;
            }
            else
            {
                var query = from userx in db.Users
                            join userSite in db.UserSites on userx.ID equals userSite.UserID
                            join siteUserHasPermissionFor in sitesUserHasPermissionFor on userSite.SiteID equals siteUserHasPermissionFor
                            join usery in db.Users on userx.CreatedByUserID equals usery.ID
                            where userx.ID == user.ID && usery.IsSiteAdmin && usery.ID == user.UILoginUserID && userx.CreatedByUserID == user.UILoginUserID
                            select userx;
                if (query.Count() == 1)
                {
                    db.Entry(user).State = EntityState.Modified;
                }
                else
                {
                    return BadRequest();
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (db.Users.Where(u => u.UserName == user.UserName).Count() > 0)
            {
                return BadRequest();
            }
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, user.UILoginUserID, user.UILoginPassword, "PostUser");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return BadRequest();
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                db.Users.Add(user);
            }
            else
            {
                User userx = db.Users.Find(user.UILoginUserID);
                if (userx == null)
                {
                    return BadRequest();
                }
                if (!userx.IsSiteAdmin)
                {
                    return BadRequest();
                }
                if (userx.ID != user.CreatedByUserID)
                {
                    return BadRequest();
                }
                db.Users.Add(user);
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id, int UILoginUserID, string UILoginPassword)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "DeleteUser");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return BadRequest();
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                db.Users.Remove(user);
            }
            else
            {
                User userx = db.Users.Find(UILoginUserID);
                if (userx == null)
                {
                    return BadRequest();
                }
                if (user.IsSiteAdmin || user.IsRootUser)
                {
                    return BadRequest();
                }
                if (user.CreatedByUserID != userx.ID && !userx.IsRootUser)
                {
                    return BadRequest();
                }
                if (!userx.IsSiteAdmin && !userx.IsRootUser)
                {
                    return BadRequest();
                }
                db.Users.Remove(user);
            }
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}