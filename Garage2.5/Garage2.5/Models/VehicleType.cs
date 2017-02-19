using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._5.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Required!")]
        [Display(Name ="Vehicle Type")]
        public string Type { get; set; }
       
        // Navigation Property

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}