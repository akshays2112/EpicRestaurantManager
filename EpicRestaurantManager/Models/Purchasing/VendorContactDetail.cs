using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    /* A vendor can have zero or thousands of contacts.  When contact person leaves vendors 
    company the data is not lost about the old procurement person for data anaylysis or
    business intelligence needs. */
    public class VendorContactDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
        [Required]
        public int VendorID { get; set; }
        [Required]
        public bool Active { get; set; }
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
        [ForeignKey("VendorID")]
        public Vendor Vendor { get; set; }

        public VendorContactDetail()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}