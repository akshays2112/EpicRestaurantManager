using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class UserControllerActionPermission
    {
        public int ID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ControllerActionID { get; set; }
        [Required]
        public bool Allow { get; set; }
        [Required]
        public int EntryByUserID { get; set; }
        [Required]
        public int SiteID { get; set; }
        [Required]
        public DateTime TransactionDateTime { get; set; }
        [NotMapped]
        public int UILoginUserID { get; set; }
        [NotMapped]
        public string UILoginPassword { get; set; }
        [ForeignKey("EntryByUserID")]
        public User User { get; set; }
        [ForeignKey("SiteID")]
        public Site Site { get; set; }
        [ForeignKey("UserID")]
        public User UserForPermission { get; set; }
        [ForeignKey("ControllerActionID")]
        public ControllerAction ControllerAction { get; set; }

        public UserControllerActionPermission()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}