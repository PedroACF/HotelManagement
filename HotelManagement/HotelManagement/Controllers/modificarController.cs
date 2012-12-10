using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    public class modificarController : Controller
    {
        //
        // GET: /modificar/

        public ActionResult Index(int r)
        {
            ViewBag.gg = r;
            DataClasses1DataContext db = new DataClasses1DataContext();
            var q = from tip in db.tip_habs
                    select new SelectListItem { Selected = false, Text = tip.tipo, Value = tip.id.ToString() };
            SelectListItem[] listaOpciones = new SelectListItem[10];
            for (int j = 0; j < 10; j++)
                listaOpciones[j] = new SelectListItem() { Text = (j + 1).ToString(), Value = (j + 1).ToString(), Selected = false };
            
            ViewBag.t = q.ToArray();
            return View();
        }
        [HttpPost]
        public ActionResult Index(_Habitacion a)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var id = db.habitacions.Where(b => b.numero == a.numero).First();
            id.disponibilidad = a.disponibilidad;
            id.precio = a.precio;
            id.tipo = a.tipo;
            db.SubmitChanges();
            
            ViewBag.gg = -1;
            ViewBag.t = new SelectListItem[] { new SelectListItem { Selected = true, Text = "0", Value = "0" } };

            return View();
        }
    }
}
