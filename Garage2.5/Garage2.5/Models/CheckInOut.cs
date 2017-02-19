using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._5.Models
{
    public class CheckInOut
    {
        public DateTime CheckIn { get; set; }
        public int VehicleId { get; set; }
        public DateTime CheckOut { get; set; }
        public int Price { get; set; }

    }
}