using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using EpicRestaurantManager.Models;

namespace EpicRestaurantManager.Controllers
{
    public class ControllerActionsController : ApiController
    {
        private EpicRestaurantManagerContext db = new EpicRestaurantManagerContext();

        // GET: api/ControllerActions
        public IQueryable<ControllerAction> GetControllerActions(int UILoginUserID, string UILoginPassword)
        {
            List<int> sitesUserHasPermissionFor = Global.CheckUserIDAndPassword(db, UILoginUserID, UILoginPassword, "GetControllerActions");
            if (sitesUserHasPermissionFor.Count() < 1)
            {
                return null;
            }
            if (sitesUserHasPermissionFor.Count() == 1 && sitesUserHasPermissionFor[0] == -1)
            {
                return db.ControllerActions;
            }
            else
            {
                var query = from controllerAction in db.ControllerActions
                            select controllerAction;
                return query;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}