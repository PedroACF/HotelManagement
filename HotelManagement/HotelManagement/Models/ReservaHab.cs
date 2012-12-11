using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class ReservaHab
    {
        public int tipoH { set; get; }
        public int tipocli { set; get; }
        public int tipocli2 { set; get; }
        public string buscarpersona { set; get; }
        public string buscarempresa { set; get; }
        public string buscaragencia { set; get; }
        public int idhab { set; get; }
        public bool HabEleg { set; get; }
        public int Cant_cli { set; get; }
        public int CantD { set; get; }
        public reservass res { get; set; }
        public clientess cli { get; set; }
        public personass per { get; set; }
        public empresass emp { get; set; }
        public agenciass agen { get; set; }
        public pasajeross pasa { get; set; }

        public string Fecha_I { set; get; }
        public string Fecha_F { set; get; }
    }
    public class reservass
    {
        public string id_usu { set; get; }
        public string id_cli { set; get; }
        public int num_hab { set; get; }
        public string estado { set; get; }
        public double tip_pago { set; get; }
    }
    public class clientess
    {
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }
        [Display(Name = "Estado")]
        public string Estadoo { get; set; }
        [Display(Name = "País")]
        public string pais { get; set; }
        [Display(Name = "NIT")]
        public string nit { get; set; }
        [Display(Name = "Telefono")]
        public string telefono { get; set; }
        [Display(Name = "Direccion")]
        public string direccion { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Comentarios")]
        public string comentarios { get; set; }
    }
    public class personass
    {
        public int idper { get; set; }
        [Display(Name = "C.I.")]
        public int ci { get; set; }
        [Display(Name = "Apellido")]
        public string apellido { get; set; }
        [Display(Name = "Cumpleaños")]
        public DateTime cumple { get; set; }
        [Display(Name = "Pasaporte")]
        public string pasaporte { get; set; }
    }
    public class empresass
    {
        public int idempresa { get; set; }
        [Display(Name = "Contacto")]
        public string contacto { get; set; }
        [Display(Name = "Metodo de Pago")]
        public string metodoPago { get; set; }
    }
    public class agenciass
    {
        public int id_agencia { get; set; }
        [Display(Name = "Contacto")]
        public string contacto { get; set; }
    }
    public class pasajeross
    {
        public int idreserva { get; set; }
        public int idpersona { get; set; }
    }
}