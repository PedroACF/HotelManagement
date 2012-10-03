using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    public class usuarioController : Controller
    {
        //
        // GET: /usuario/

        [Authorize(Roles="Administrador")]
        public ActionResult Index()
        {
           // string cad = Session["username"].ToString();
            DataClasses1DataContext db = new DataClasses1DataContext();
            //List<Role> lrol = db.UsersInRoles.Where(b => b.User.UserName==cad).Select(b=>b.Role).ToList();
            //ViewBag.lista1 = lrol;
            List<StructUser> lUsers = db.Users.Select(a => new StructUser() { id = a.UserId, nombre = a.UserName, roles = db.UsersInRoles.Where(s => s.UserId == a.UserId).Select(p => p.Role).ToList()}).ToList();
            //List<UsuarioView> lUsers = new List<UsuarioView>();
           
            /*UsuarioView p = new UsuarioView()
            {
                id = Guid.NewGuid(),
                nombre="DItmar",
                roles=null
            };
            lUsers.Add(p);
            */
            ViewBag.lista = lUsers;

            return View();
        }
       [Authorize(Roles="Usuario,Administrador")]
        public ActionResult reservar_habitaciones()
        {
            /*DataClasses1DataContext db = new DataClasses1DataContext();
            habitacion nueva = new Models.habitacion();
            nueva.tipo = "simple";
            
                    db.SubmitChanges();
            return RedirectToAction("Index", "Home");
           */
            return View();
        }
        

    }
}