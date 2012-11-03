using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    public class _Habitacion
    {
        public int numero { get; set; }
        public string tipo { get; set; }
        public bool disponibilidad { get; set; }
        public float precio { get; set; }
    }

    public class _HabitacionArray
    {
        public IList<_Habitacion> h { get; set; }
        public string tam { get; set; }
        
    }
}