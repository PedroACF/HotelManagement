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
                    select new SelectListItem { Selected = false, Text = tip.tipo, Value = tip.tipo};
            SelectListItem[] listaOpciones = new SelectListItem[10];
            for (int i = 0; i < 10;i++ )
                listaOpciones[i] = new SelectListItem() { Text = (i + 1).ToString(), Value = (i + 1).ToString(), Selected = false };
                //SelectListItem []listaOpciones=new SelectListItem            ViewBag.t = tipos;
            ViewBag.t = q.ToArray();
            ViewBag.t2 = listaOpciones;
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
                    ModelState.AddModelError("", "El número de la habitacion ya existe <href http://localhost:1265/>");
                    var q = from tip in db.tip_habs
                            select new SelectListItem { Selected = false, Text = tip.tipo, Value = tip.tipo };
                    SelectListItem[] listaOpciones = new SelectListItem[10];
                    for (int j = 0; j < 10; j++)
                        listaOpciones[j] = new SelectListItem() { Text = (j + 1).ToString(), Value = (j + 1).ToString(), Selected = false };
                    //SelectListItem []listaOpciones=new SelectListItem            ViewBag.t = tipos;
                    ViewBag.t = q.ToArray();
                    ViewBag.t2 = listaOpciones;
                    return View(habitacion);
                }
            }
            //return Json(new Object());
            return RedirectToAction("Index", "Home");
        }
    }
}
