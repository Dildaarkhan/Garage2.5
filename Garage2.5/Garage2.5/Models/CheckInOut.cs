using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Garage2._5.Models
{
    public class CheckInOut
    {
        [Key]
        [Column(Order =1)]
        public DateTime CheckIn { get; set; }
        [Key]
        [Column(Order = 2)]
        public int VehicleId { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? Price { get; set; }

        // navigation property

        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}