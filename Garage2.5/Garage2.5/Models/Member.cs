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

        [Required(ErrorMessage = "Required!")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\-\s*]+$", ErrorMessage = "Only Alphabets!")]
        [StringLength(50, ErrorMessage = "50 Characters Maximum")]
        public string Name { get; set; }

        public string Address { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered mobile format is not valid.")]
        public string Telephone { get; set; }
        


        // Navigation Property
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}