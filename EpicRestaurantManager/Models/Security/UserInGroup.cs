using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class UserInGroup
    {
        public int ID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int UserGroupID { get; set; }
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
        public User UserPermission { get; set; }
        [ForeignKey("UserGroupID")]
        public UserGroup UserGroup { get; set; }

        public UserInGroup()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}