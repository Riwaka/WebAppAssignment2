//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDevAssignmnet2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Cloth
    {
        public int Cid { get; set; }
        
        [Required(ErrorMessage = "Gender Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Category Required")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Condition Required")]
        public string Condition { get; set; }
        [Required(ErrorMessage = "Color Required")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Size Required")]
        public string Size { get; set; }
        [Required(ErrorMessage = "Brand Required")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Location Required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Price Required")]
        public Nullable<decimal> Price { get; set; }
    }
}

