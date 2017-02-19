using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._5.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Color Field is required")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\-\s*]+$", ErrorMessage = "Only Alphabets!")]
        [StringLength(50, ErrorMessage = "50 Characters Maximum")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Telephone { get; set; }


        // Navigation Property
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}