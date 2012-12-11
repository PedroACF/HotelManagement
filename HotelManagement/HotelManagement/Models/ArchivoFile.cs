using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    public class ArchivoFile
    {
        public HttpPostedFileBase archivo { set; get; }
        public string rutafisica { set; get; }
        public DateTime fecha { set; get; }
    }

    public class clienteC
    {
        public string nombreC { set; get; }
        public string ciudadC { set; get; }
        public string nitC { set; get; }
        public string telefonoC { set; get; }
    }
    public class reservasC
    {
        public string nombreCliente { set; get; }
        public string numHab { set; get; }
        public string fechaI { set; get; }
        public string fechaF { set; get; }
    }
    
}