﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._5.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Type { get; set; }
       
        // Navigation Property

        public virtual Vehicle Vehicles { get; set; }
    }
}