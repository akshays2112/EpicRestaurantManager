using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class ControllerAction
    {
        public int ID { get; set; }
        [Required]
        public string ControllerActionName { get; set; }
        [Required]
        public string Description { get; set; }
        [NotMapped]
        public int UILoginUserID { get; set; }
        [NotMapped]
        public string UILoginPassword { get; set; }
        public List<UserControllerActionPermission> UserControllerActionPermissions { get; set; }
        public List<UserGroupControllerActionPermission> UserGroupControllerActionPermissions { get; set; }
    }
}