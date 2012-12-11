using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagement.Models;
namespace HotelManagement.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //consulta para seleccionar y filtrar los datos iniciales
            List<Dashboard> lista = db.habitacions.Select(a => new Dashboard()
            {
                numero = a.numero,
                reservas = a.reservas.Select(b => new res()
                {
                    idHa = b.idHab,
                    fechainicial = b.fecha_ini,
                    fechafinal = b.fecha_fin,
                    totaldias = b.fecha_fin.Subtract(b.fecha_ini).Days
                }).ToList()
            }).ToList();

            ViewBag.info = lista;
            DateTime hoy = DateTime.Now;
            int anio = hoy.Year;
            int mes = hoy.Month;
            int dia = hoy.Day;
            string value = "";
            while (value != "Monday")
            {
                DateTime find = new DateTime(anio, mes, dia);
                dia--;
                if (dia < 1)
                {
                    mes--;
                    if (mes < 1)
                    {
                        anio--;
                        mes = 12;
                    }
                    dia = DateTime.DaysInMonth(anio, mes);
                }
                value = find.DayOfWeek.ToString();
            }
            dia++;
            DateTime hh = new DateTime(anio, mes, dia);
            List<DateTime> semanas = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                semanas.Add(hh.AddDays(i));
            }
            ViewBag.fechas = semanas;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Dashboard datos)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //consulta para seleccionar y filtrar los datos iniciales
            List<Dashboard> lista = db.habitacions.Select(a => new Dashboard()
            {
                numero = a.numero,
                reservas = a.reservas.Select(b => new res()
                {
                    idHa = b.idHab,
                    fechainicial = b.fecha_ini,
                    fechafinal = b.fecha_fin,
                    totaldias = b.fecha_fin.Subtract(b.fecha_ini).Days
                }).ToList()
            }).ToList();

            ViewBag.info = lista;
            DateTime hoy = DateTime.Parse(datos.fechaInicio.Day + "-" + datos.fechaInicio.Month + "-" + datos.fechaInicio.Year);
            int anio = hoy.Year;
            int mes = hoy.Month;
            int dia = hoy.Day;
            string value = "";
            while (value != "Monday")
            {
                DateTime find = new DateTime(anio, mes, dia);
                dia--;
                if (dia < 1)
                {
                    mes--;
                    if (mes < 1)
                    {
                        anio--;
                        mes = 12;
                    }
                    dia = DateTime.DaysInMonth(anio, mes);
                }
                value = find.DayOfWeek.ToString();
            }
            dia++;
            DateTime hh = hoy;
            List<DateTime> semanas = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                semanas.Add(hh.AddDays(i));
            }
            ViewBag.fechas = semanas;
            return View();
        }
    }
}