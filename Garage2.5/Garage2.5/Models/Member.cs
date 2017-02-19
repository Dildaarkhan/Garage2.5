using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._5.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Telephone { get; set; }


        // Navigation Property
        public virtual Vehicle Vehicles { get; set; }

    }
}