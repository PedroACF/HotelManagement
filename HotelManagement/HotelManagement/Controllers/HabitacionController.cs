using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    public class HabitacionController : Controller
    {
        //
        // GET: /Habitacion/

        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //tip_hab[] tipos = db.tip_habs.ToArray();
            var q = from tip in db.tip_habs
                    select new SelectListItem { Selected = false, Text = tip.tipo, Value = tip.id.ToString() };

            var ha = from habi in db.habitacions
                     select new SelectListItem { Selected = false, Text = habi.numero.ToString(), Value = habi.numero.ToString() };
            SelectListItem[] listaOpciones = new SelectListItem[10];
            for (int i = 0; i < 10; i++)
                listaOpciones[i] = new SelectListItem() { Text = (i + 1).ToString(), Value = (i + 1).ToString(), Selected = false };
            //SelectListItem []listaOpciones=new SelectListItem            ViewBag.t = tipos;
            ViewBag.t = q.ToArray();
            ViewBag.t2 = listaOpciones;
            ViewBag.x = (from k in db.habitacions select k).Take(10).ToList();
            ViewBag.hab = ha.ToArray();
            return View();
        }
        [HttpPost]
        public ActionResult Index(_HabitacionArray habitacion)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            //numero = db.numerohabitaciones;
            int tam = int.Parse(habitacion.tam);
            for (int i = 0; i < tam; i++)
            {
                habitacion hab = new habitacion();

                hab.numero = habitacion.h[i].numero;
                hab.tipo = habitacion.h[i].tipo;
                hab.disponibilidad = true;
                hab.precio = habitacion.h[i].precio;
                try
                {
                    db.habitacions.InsertOnSubmit(hab);
                    db.SubmitChanges();
                }
                catch (Exception)
                {
                    //this.ValidateRequest = false;
                    ModelState.AddModelError("", "El número de la habitacion ya existe");
                    var q = from tip in db.tip_habs
                            select new SelectListItem { Selected = false, Text = tip.tipo, Value = tip.id.ToString() };
                    var ha = from habi in db.habitacions
                             select new SelectListItem { Selected = false, Text = habi.numero.ToString(), Value = habi.numero.ToString() };
                    SelectListItem[] listaOpciones = new SelectListItem[10];
                    for (int j = 0; j < 10; j++)
                        listaOpciones[j] = new SelectListItem() { Text = (j + 1).ToString(), Value = (j + 1).ToString(), Selected = false };

                    //SelectListItem []listaOpciones=new SelectListItem            ViewBag.t = tipos;
                    ViewBag.x = (from k in db.habitacions select k).Take(10).ToList();
                    ViewBag.t = q.ToArray();
                    ViewBag.t2 = listaOpciones;
                    ViewBag.hab = ha.ToArray();
                    return View(habitacion);
                }
                ViewBag.x = (from k in db.habitacions select k).Take(10).ToList();
            }

            //return n(new Object());
            return RedirectToAction("Index", "Habitacion");
        }
        public JsonResult DeleteHab(int id)
        {
            //string _num= new string numero;
            //Guid _idRol = new Guid(idRol);
            //Guid _idUser = new Guid(idUser);
            DataClasses1DataContext db = new DataClasses1DataContext();
            habitacion num = db.habitacions.Where(a => a.numero == id).First();
            db.habitacions.DeleteOnSubmit(num);
            db.SubmitChanges();
            return Json(new { success = true });
        }

        public JsonResult AgregarTipo(string tipo)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            tip_hab nuevo = new Models.tip_hab();
            nuevo.tipo = tipo;
            db.tip_habs.InsertOnSubmit(nuevo);
            db.SubmitChanges();
            return Json(new { success = true });

        }
        public JsonResult PonerMantenimiento(string habi, string fechai, string fechaf)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            mantencione man = new Models.mantencione();
            man.idhab = Convert.ToInt32(habi);
            string ini = fechai.Substring(3, 3) + fechai.Substring(0, 3) + fechai.Substring(6, 4);
            string fin = fechaf.Substring(3, 3) + fechaf.Substring(0, 3) + fechaf.Substring(6, 4);
            man.fecha_ini = Convert.ToDateTime(ini);
            man.fecha_fin = Convert.ToDateTime(fin);
            db.mantenciones.InsertOnSubmit(man);
            db.SubmitChanges();
            return Json(new { success = true });

        }
    }
}
