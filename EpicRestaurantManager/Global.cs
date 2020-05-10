using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EpicRestaurantManager.Models;

namespace EpicRestaurantManager
{
    public class Global
    {
        public static List<int> CheckUserIDAndPassword(EpicRestaurantManagerContext db, int UILoginUserID, string UILoginPassword, string ControllerActionName)
        {
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return new List<int>();
            }
            if (user.Password != UILoginPassword)
            {
                return new List<int>();
            }
            if (user.IsRootUser)
            {
                List<int> rootSites = new List<int>();
                rootSites.Add(-1);
                return rootSites;
            }
            if (user.IsSiteAdmin)
            {
                var querySiteAdmin = from siteAdmin in db.Users
                                     join userSite in db.UserSites on siteAdmin.ID equals userSite.UserID
                                     where siteAdmin.ID == UILoginUserID && siteAdmin.Password == UILoginPassword
                                     select userSite.SiteID;
                return querySiteAdmin.ToList();
            }
            List<int> SiteIDs = new List<int>();
            var queryUserForGroupPermission = from userInGroup in db.UserInGroups
                                              join userx in db.Users on userInGroup.UserID equals userx.ID
                                              join userSite in db.UserSites on userx.ID equals userSite.UserID
                                              join userGroup in db.UserGroups on userInGroup.UserGroupID equals userGroup.ID
                                              join userGroupControllerActionPermission in db.UserGroupControllerActionPermissions on userGroup.ID equals userGroupControllerActionPermission.UserGroupID
                                              join controllerAction in db.ControllerActions on userGroupControllerActionPermission.ControllerActionID equals controllerAction.ID
                                              where userInGroup.UserID == UILoginUserID && (controllerAction.ControllerActionName == ControllerActionName || user.IsSiteAdmin)
                                              select new { Allow = userGroupControllerActionPermission.Allow, SiteID = userSite.SiteID };
            if (queryUserForGroupPermission.Count() > 0)
            {
                foreach (var permission in queryUserForGroupPermission)
                {
                    if (permission.Allow)
                    {
                        if (!SiteIDAlreadyExists(SiteIDs, permission.SiteID))
                        {
                            SiteIDs.Add(permission.SiteID);
                        }
                    }
                }
            }
            var queryUserPermission = from userControllerActionPermissions in db.UserControllerActionPermissions
                                      join usery in db.Users on userControllerActionPermissions.UserID equals usery.ID
                                      join userSitex in db.UserSites on userControllerActionPermissions.SiteID equals userSitex.SiteID
                                      join controllerActions in db.ControllerActions on userControllerActionPermissions.ControllerActionID equals controllerActions.ID
                                      where userControllerActionPermissions.UserID == UILoginUserID && (controllerActions.ControllerActionName == ControllerActionName || usery.IsSiteAdmin)
                                      select new { Allow = userControllerActionPermissions.Allow, userSitex.SiteID };
            if (queryUserPermission.Count() > 0)
            {
                foreach (var userpermission in queryUserPermission)
                {
                    if (userpermission.Allow)
                    {
                        if (!SiteIDAlreadyExists(SiteIDs, userpermission.SiteID))
                        {
                            SiteIDs.Add(userpermission.SiteID);
                        }
                    }
                }
            }
            return SiteIDs;
        }

        private static bool SiteIDAlreadyExists(List<int> SiteIDs, int SiteID)
        {
            foreach (int x in SiteIDs)
            {
                if (x == SiteID)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckUserIDAndPasswordWithSiteID(EpicRestaurantManagerContext db, int UILoginUserID, string UILoginPassword, int SiteID, string ControllerActionName)
        {
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return false;
            }
            if (user.Password != UILoginPassword)
            {
                return false;
            }
            if (user.IsRootUser)
            {
                return true;
            }
            if (user.IsSiteAdmin && db.UserSites.Where(u => u.SiteID == SiteID && u.UserID == UILoginUserID).Count() > 0)
            {
                return true;
            }
            var queryUserForGroupPermission = from userInGroup in db.UserInGroups
                                              join userx in db.Users on userInGroup.UserID equals userx.ID
                                              join userSite in db.UserSites on userx.ID equals userSite.UserID
                                              join userGroup in db.UserGroups on userInGroup.UserGroupID equals userGroup.ID
                                              join userGroupControllerActionPermission in db.UserGroupControllerActionPermissions on userGroup.ID equals userGroupControllerActionPermission.UserGroupID
                                              join controllerAction in db.ControllerActions on userGroupControllerActionPermission.ControllerActionID equals controllerAction.ID
                                              where userInGroup.UserID == UILoginUserID && (controllerAction.ControllerActionName == ControllerActionName || user.IsSiteAdmin) && userSite.SiteID == SiteID
                                              select userGroupControllerActionPermission;
            if (queryUserForGroupPermission.Count() > 0)
            {
                foreach (var permission in queryUserForGroupPermission)
                {
                    if (!permission.Allow)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                var queryUserPermission = from userControllerActionPermissions in db.UserControllerActionPermissions
                                          join usery in db.Users on userControllerActionPermissions.UserID equals usery.ID
                                          join userSitex in db.UserSites on userControllerActionPermissions.SiteID equals userSitex.SiteID
                                          join controllerActions in db.ControllerActions on userControllerActionPermissions.ControllerActionID equals controllerActions.ID
                                          where userControllerActionPermissions.UserID == UILoginUserID && (controllerActions.ControllerActionName == ControllerActionName || usery.IsSiteAdmin) && userSitex.SiteID == SiteID
                                          select userControllerActionPermissions;
                if (queryUserPermission.Count() > 0)
                {
                    foreach (var userpermission in queryUserPermission)
                    {
                        if (!userpermission.Allow)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static bool CheckUserIDAndPasswordForSiteAdd(EpicRestaurantManagerContext db, int UILoginUserID, string UILoginPassword)
        {
            User user = db.Users.Find(UILoginUserID);
            if (user == null)
            {
                return false;
            }
            if (user.Password != UILoginPassword)
            {
                return false;
            }
            if (user.IsRootUser)
            {
                return true;
            }
            if (user.IsSiteAdmin)
            {
                return true;
            }
            return false;
        }
    }
}