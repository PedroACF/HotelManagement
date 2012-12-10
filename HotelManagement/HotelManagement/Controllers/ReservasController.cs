using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagement.Models;
using Newtonsoft.Json.Linq;

namespace HotelManagement.Controllers
{
    public class ReservasController : Controller
    {
        //
        // GET: /Reservas/
        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            ViewBag.noww = DateTime.Now.ToString("MM-dd-yyyy");

            List<ReservaHab> Lista = db.habitacions.Select(a => new ReservaHab()
            {
                res = new reservass() { num_hab = a.numero, tip_pago = a.precio },
                tipoH = a.tipo
            }).ToList();
            //List<ReservaHab> Lista=db..Where(h=>db.ocupado(h.numero,model.res.Fecha_I,model.res.Fecha_F).Value==true);
            /*var lista = from h in db.habitacions
                        where db.ocupado(h.numero, model.res.Fecha_I, model.res.Fecha_F) == true
                        select new ReservaHab() {  };*/

            ViewBag.lista = Lista;
            //listar clientes
            var pe = from per in db.personas
                     select new SelectListItem { Selected = false, Text = per.cliente.nombre, Value = per.id.ToString() };

            ViewBag.p = pe.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(ReservaHab model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            reserva reserr = new reserva();
            System.Guid id = System.Guid.Parse((System.Web.Security.Membership.GetUser(User.Identity.Name).ProviderUserKey).ToString());
            reserr.idusuario = id;
            var cliiii = 0;
            if (model.tipocli2 == 1)
            {
                db.personas.Where(a => (a.cliente.nombre + " " + a.apellido).ToString() == model.buscarpersona.ToString()).Select(a => a.id).First();
            }
            if (model.tipocli2 == 2)
            {
                db.empresas.Where(a => a.cliente.nombre.ToString() == model.buscarempresa.ToString()).Select(a => a.id).First();
            }
            if (model.tipocli2 == 3)
            {
                db.agencias.Where(a => a.cliente.nombre.ToString() == model.buscaragencia.ToString()).Select(a => a.id).First();
            }
            reserr.idcli = cliiii;

            reserr.idHab = model.idhab;
            string ini = model.Fecha_I.Substring(3, 3) + model.Fecha_I.Substring(0, 3) + model.Fecha_I.Substring(6, 4);
            string fin = model.Fecha_F.Substring(3, 3) + model.Fecha_F.Substring(0, 3) + model.Fecha_F.Substring(6, 4);
            reserr.fecha_ini = DateTime.Parse(ini);
            reserr.fecha_fin = DateTime.Parse(fin);
            reserr.estado = "pendiente";
            var ppago = db.habitacions.Where(a => a.numero == model.idhab).Select(a => a.precio).First();
            reserr.pago = ppago * model.CantD;
            db.reservas.InsertOnSubmit(reserr);
            db.SubmitChanges();
            return View();
        }
        public JsonResult loadDatanombres()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var qq = db.personas.Select(a => a.cliente.nombre + " " + a.apellido).ToArray();
            var qqq = Json(qq);
            return qqq;
        }
        public JsonResult loadDataEmpresa()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var qq = db.empresas.Select(a => a.cliente.nombre).ToArray();
            var qqq = Json(qq);
            return qqq;
        }
        public JsonResult loadDataAgencia()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var qq = db.agencias.Select(a => a.cliente.nombre).ToArray();
            var qqq = Json(qq);
            return qqq;
        }

        [HttpPost]
        public ActionResult regcliente(ReservaHab reser)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            cliente cli = new cliente();
            cli.nombre = reser.cli.Nombre;
            cli.ciudad = reser.cli.Ciudad;
            cli.estado = reser.cli.Estadoo;
            cli.pais = reser.cli.pais;
            cli.nit = reser.cli.nit;
            cli.telefono = reser.cli.telefono;
            cli.direccion = reser.cli.direccion;
            cli.email = reser.cli.email;
            cli.comentarios = reser.cli.comentarios;

            db.clientes.InsertOnSubmit(cli);
            db.SubmitChanges();

            if (reser.tipocli == 1)
            {
                persona per = new persona();
                per.id = cli.id;
                per.ci = reser.per.ci;
                per.apellido = reser.per.apellido;/*
                string d = (reser.per.cumple).ToString();
                d = d.Substring(3, 3) + d.Substring(0, 3) + d.Substring(6, 4);
                per.cumpleaños = Convert.ToDateTime(d);*/
                per.cumpleaños = reser.per.cumple;
                per.pasaporte = reser.per.pasaporte;
                db.personas.InsertOnSubmit(per);
                db.SubmitChanges();
            }
            if (reser.tipocli == 2)
            {
                empresa empp = new empresa();
                empp.id = cli.id;
                empp.contacto = reser.emp.contacto;
                empp.metodopago = reser.emp.metodoPago;
                db.empresas.InsertOnSubmit(empp);
                db.SubmitChanges();
            }
            if (reser.tipocli == 3)
            {
                agencia agenn = new agencia();
                agenn.id = cli.id;
                agenn.contacto = reser.agen.contacto;
                db.agencias.InsertOnSubmit(agenn);
                db.SubmitChanges();
            }
            return View();
        }
        public JsonResult habitacionesdisponibles(string fechai, string fechaf)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            string fii = fechai.Substring(3, 3) + fechai.Substring(0, 3) + fechai.Substring(6, 4);
            string fnn = fechaf.Substring(3, 3) + fechaf.Substring(0, 3) + fechaf.Substring(6, 4);
            DateTime fi = DateTime.Parse(fii), fn = DateTime.Parse(fnn);

            var numero = (db.habitacions.Where(h => db.ocupado(h.numero, fi, fn).Value).Select(h => h.numero.ToString())).ToArray();
            var tipo = (db.habitacions.Where(h => db.ocupado(h.numero, fi, fn).Value).Select(h => h.tip_hab.tipo)).ToArray();
            var precio = (db.habitacions.Where(h => db.ocupado(h.numero, fi, fn).Value).Select(h => (Convert.ToInt32(h.precio)).ToString())).ToArray();
            var q = db.habitacions.Where(h => db.ocupado(h.numero, fi, fn).Value).Select(h => h.numero);
            var ress = q.ToArray();
            var rr = Json(ress);
            int t = numero.Count();
            string[,] cadena = new string[t, 3];
            for (int i = 0; i < t; i++)
            {
                cadena[i, 0] = numero[i];
                cadena[i, 1] = tipo[i];
                cadena[i, 2] = precio[i];
            }

            var cad = Json(cadena);
            return cad;
        }
    }
}