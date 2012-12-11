using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagement.Models;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace HotelManagement.Controllers
{
    public class cvsController : Controller
    {
        //
        // GET: /cvs/
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult upload(ArchivoFile datos)
        {
            string rutafisica = Server.MapPath("~/csv");
            string rt = rutafisica + @"\" + datos.archivo.FileName;
            datos.archivo.SaveAs(rutafisica + @"\" + datos.archivo.FileName);
            DataClasses1DataContext db = new DataClasses1DataContext();
            archivo ar = new archivo()
            {
                ruta_fisica = rutafisica + @"\" + datos.archivo.FileName,
                fecha = DateTime.Now
            };
            db.archivos.InsertOnSubmit(ar);
            db.SubmitChanges();
            CsvReader csv = new CsvReader(new StreamReader(rt), true);
            //int total = csv.FieldCount;
            //string[] headers = csv.GetFieldHeaders;
            List<reservasC> listares = new List<reservasC>();
            while (csv.ReadNextRecord())
            {
                reservasC res = new reservasC()
                {
                    nombreCliente = csv[0],
                    numHab = csv[1],
                    fechaI = csv[2],
                    fechaF = csv[3]
                    
         
                };
                listares.Add(res);
            }
            ViewBag.lista = listares;
            return View();
        }

        public ActionResult uploadCli()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadCli(ArchivoFile datos)
        {
            string rutafisica = Server.MapPath("~/csv");
            string rt = rutafisica + @"\" + datos.archivo.FileName;
            datos.archivo.SaveAs(rutafisica + @"\" + datos.archivo.FileName);
            DataClasses1DataContext db = new DataClasses1DataContext();
            archivo ar = new archivo()
            {
                ruta_fisica = rutafisica + @"\" + datos.archivo.FileName,
                fecha = DateTime.Now
            };
            db.archivos.InsertOnSubmit(ar);
            db.SubmitChanges();
            CsvReader csv = new CsvReader(new StreamReader(rt), true);
            //int total = csv.FieldCount;
            //string[] headers = csv.GetFieldHeaders;
            List<clienteC> listacli = new List<clienteC>();
            while (csv.ReadNextRecord())
            {
                clienteC cli = new clienteC()
                {
                    nombreC= csv[0],
                    ciudadC = csv[1],
                    nitC = csv[2],
                    telefonoC = csv[3],
                    
                };
                listacli.Add(cli);
            }
            ViewBag.lista2 = listacli;
            return View();
        }
        public ActionResult exportarCsv()
        {
            return View();
        }
        public ActionResult exportarRes()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<reserva> datos = db.reservas.ToList();
            string nombre = "DETALLE DE RESERVAS" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(".", "_").Replace(" ", "_") + ".csv";
            string ruta = Server.MapPath("~/download");
            StreamWriter stream = System.IO.File.CreateText(ruta + @"\" + nombre);

            foreach (var item in datos)
            {
                //+=item.id+"+"
                stream.WriteLine("{0},{1},{2},{3}", "id de habitacion "+item.idcli,"id de habitacion"+ item.idHab, "fecha de llegada"+item.fecha_ini, "fecha de llegada"+item.fecha_fin);
            }
            stream.Close();
            return Redirect("/download/" + nombre);
        }

        public ActionResult exportarcvs()
        {
            return View();
        }
        public ActionResult exportarCli()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<cliente> datos = db.clientes.ToList();
            string nombre = "DETALLE DE CLIENTES" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(".", "_").Replace(" ", "_") + ".csv";
            string ruta = Server.MapPath("~/download");
            StreamWriter stream = System.IO.File.CreateText(ruta + @"\" + nombre);

            foreach (var item in datos)
            {
                //+=item.id+"+"
                stream.WriteLine("{0},{1},{2},{3},{4}", "id de Cliente: "+item.id, "Nombre del Cliente:"+item.nombre,"Pais:" +item.pais, "NIT Cliente"+item.nit, "Telefono Cliente"+item.telefono);
            }
            stream.Close();
            return Redirect("/download/" + nombre);
        }
            
    }
}
