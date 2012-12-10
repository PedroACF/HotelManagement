using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    public class modificarUsuarioController : Controller
    {
        //
        // GET: /modificarUsuario/

        public ActionResult Index(string r)
        {
            Guid aux = ViewBag.gg = Guid.Parse(r);
            
            DataClasses1DataContext db = new DataClasses1DataContext();
            var q = db.Users.Where(u => u.UserId == aux).First();
            
            ViewBag.userData = q;
            /*SelectListItem[] listaOpciones = new SelectListItem[10];
            for (int j = 0; j < 10; j++)
                listaOpciones[j] = new SelectListItem() { Text = (j + 1).ToString(), Value = (j + 1).ToString(), Selected = false };
            
            ViewBag.t = q.ToArray();*/
            return View();
        }
        [HttpPost]
        public ActionResult Index(StructUser u)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var id = db.Users.Where(b => b.UserId == u.id).First();
            id.UserName = u.nombre;
            id.Membership.Email = u.email;
//            id.cliente.nit = u.nit;
            db.SubmitChanges();

            ViewBag.gg = Guid.Parse("00000000-0000-0000-0000-000000000000");
            ViewBag.t = new SelectListItem[] { new SelectListItem { Selected = true, Text = "0", Value = "0" } };

            return View();
        }
        
    }
}
