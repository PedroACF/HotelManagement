using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Dashboard
    {
        
        public int numero { set; get; }
        public string descripcion { set; get; }
        public List<res> reservas { set; get; }
        [DataType(DataType.Date)]
        public DateTime fechaInicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime fechaFin { get; set; }
    }
    public class res
    {
        public int idHa { set; get; }
        public DateTime fechainicial { set; get; }
        public DateTime fechafinal { set; get; }
        public int totaldias { set; get; }
        public string estado { set; get; }
    }
}