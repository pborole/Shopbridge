using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class Inventories
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Item Name required.")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Item Description required.")]
        [Display(Name = "Item Description")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "Item Price required.")]
        [Display(Name = "Item Price")]
        public string ItemPrice { get; set; }

        [Required(ErrorMessage = "Item Expiry Date required.")]
        [Display(Name = "Item Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ItemExpiryDate { get; set; }

        [Required(ErrorMessage = "Item Code required.")]
        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

    }
}