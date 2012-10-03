using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    public class StructUser
    {
        public System.Guid id { set; get; }
        public String nombre { set; get; }
        public List<Role> roles { set; get; }
    }
}