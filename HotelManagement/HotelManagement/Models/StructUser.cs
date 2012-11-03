using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    /*public class StructUser
    {
        public System.Guid id { set; get; }
        public String nombre { set; get; }
        public List<Role> roles { set; get; }
    }*/
    public class RolView
    {
        public Guid id { set; get; }
        public string nombre { set; get; }
    }
    public class UserListRol
    {
        public Guid id { set; get; }
        public string nombre { set; get; }
        public List<RolView> ListaRoles { set; get; }

    }
}