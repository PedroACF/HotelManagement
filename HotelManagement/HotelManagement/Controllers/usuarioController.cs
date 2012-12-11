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

        public JsonResult loadData()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            return Json(new { lista = db.Users.ToList(), nombres = db.Users.Select(a => a.UserName).ToList() });
        }

        [Authorize(Roles="Administrador")]
        public ActionResult Index()
        {
           // string cad = Session["username"].ToString();
            DataClasses1DataContext db = new DataClasses1DataContext();
            //List<Role> lrol = db.UsersInRoles.Where(b => b.User.UserName==cad).Select(b=>b.Role).ToList();
            //ViewBag.lista1 = lrol;
            //BORRADO List<StructUser> lUsers = db.Users.Select(a => new StructUser() { id = a.UserId, nombre = a.UserName, roles = db.UsersInRoles.Where(s => s.UserId == a.UserId).Select(p => p.Role).ToList()}).ToList();
            
            //List<UsuarioView> lUsers = new List<UsuarioView>();
           /*UsuarioView p = new UsuarioView()
            {
                id = Guid.NewGuid(),
                nombre="DItmar",
                roles=null
            };
            lUsers.Add(p);
            */
            List<UserListRol> Lista = db.Users.Select(a => new UserListRol()
            {
                id = a.UserId,
                nombre = a.UserName,
                ListaRoles = db.UsersInRoles.Where(b => b.UserId == a.UserId).Select(b => new RolView()
                {
                    id = b.Role.RoleId,
                    nombre = b.Role.RoleName
                }).ToList()
            }).ToList();
            ViewBag.lista = Lista;
            List<Role> listaRoles = db.Roles.ToList();
            ViewBag.listRol = listaRoles;
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
       public JsonResult addrol(string idUs2, string idRol)
       {
           //string data = idUs + idRol;
           Guid _idUs = new Guid(idUs2);
           Guid _idRol = new Guid(idRol);
           DataClasses1DataContext db = new DataClasses1DataContext();
           UsersInRole rol = new UsersInRole()
           {
               RoleId = _idRol,
               UserId = _idUs
           };
           int aa = db.UsersInRoles.Where(a => a.RoleId == _idRol && a.UserId == _idUs).Count();
           if (aa == 1)
           {
               return Json(new { success = "Error Este Rol esta Asignado" });
           }
           db.UsersInRoles.InsertOnSubmit(rol);
           db.SubmitChanges();
           string nombrerol = db.Roles.Where(a => a.RoleId == _idRol).First().RoleName;
           return Json(new { success = "Rol Agregado", rol = nombrerol, idRol = _idRol, idUs = _idUs });
       }

       public JsonResult DeleteRol(string idRol, string idUser)
       {
           Guid _idRol = new Guid(idRol);
           Guid _idUser = new Guid(idUser);
           DataClasses1DataContext db = new DataClasses1DataContext();
           UsersInRole rol = db.UsersInRoles.Where(a => a.RoleId == _idRol && a.UserId == _idUser).First();
           db.UsersInRoles.DeleteOnSubmit(rol);
           db.SubmitChanges();
           return Json(new { success = true });
       }

       public JsonResult DeleteUsuario(string id)
       {
           Guid usu = new Guid(id);
           DataClasses1DataContext db=new DataClasses1DataContext();
           User us=db.Users.Where(a=>a.UserId==usu).First();
           db.Users.DeleteOnSubmit(us);
           db.SubmitChanges();
           return Json(new{success=true});
       }
        
     }
}