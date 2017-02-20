using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._5.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required!")]
        [RegularExpression(pattern: "^[a-zA-Z]{3}[0-9]{3}", ErrorMessage = "Registration Formate is AAA123")]
        [DisplayName("Registration Number")]
        public string RegistrationNumber { get; set; }


        [Required(ErrorMessage = "Required!")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\-\s*]+$", ErrorMessage = "Only Alphabets!")]
        [StringLength(30, ErrorMessage = "30 Characters Maximum")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Required!")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\-\s*]+$", ErrorMessage = "Only Alphabets!")]
        [StringLength(30, ErrorMessage = "30 Characters Maximum!")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Required!")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ0-9\-\s*]+$", ErrorMessage = "Special Charaters are not allowed!")]
        [StringLength(30, ErrorMessage = "30 Characters Maximum!")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Range(0, 30)]
        [DisplayName("Number of Wheels")]
        public int Wheels { get; set; }

        public DateTime CheckIn { get; set; }    
        
           
       


        // Foreign Keys
        public int MemberId { get; set; }

        public int VehicleTypeId { get; set; }



        /// navigation Property

        public virtual Member Members { get; set; }
        public virtual VehicleType VehicleTypes { get; set; }

        
    }
}