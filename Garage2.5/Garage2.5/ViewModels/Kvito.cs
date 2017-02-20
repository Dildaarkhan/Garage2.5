using Garage2._5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;





namespace Garage2._5.ViewModels
{
    public class Kvito
    {
        
        [Display(Name ="Registration Number")]
        public string RegNumber { get; set; }
        [Display(Name ="Member Name")]
        public string MemberName { get; set; }
        [Display(Name = "Vehicle Type")]
        public string VehicleTypeName { get; set; }
        [Display(Name = "Check In Date")]
        public DateTime CheckInDate { get; set; }
        [Display(Name = "Check Out Date")]
        public DateTime CheckOutDate { get; set; }
        [Display(Name = "Total Price")]
        public int TotalPrice { get; set; }

    }
}