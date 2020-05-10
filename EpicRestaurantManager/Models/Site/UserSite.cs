using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class UserSite
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int SiteID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("SiteID")]
        public Site Site { get; set; }
    }
}