using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    public class _Habitacion
    {
        public int numero { get; set; }
        public int tipo { get; set; }
        public bool disponibilidad { get; set; }
        public float precio { get; set; }
    }

    public class _HabitacionArray
    {
        public IList<_Habitacion> h { get; set; }
        public string tam { get; set; }

    }
    public class Tip_Hab_mod
    {
        public int idTH { set; get; }
        public string tipo { get; set; }
    }
    public class Mantenimiento
    {
        public int idhab { set; get; }
        public DateTime fecha_ini { set; get; }
        public DateTime fecha_fin { set; get; }
    }

}