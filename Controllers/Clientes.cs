using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class ClientesController:Controller
    {
        TiendaCTX ctx;

        public ClientesController(TiendaCTX _ctx)
        {
            ctx = _ctx;
        }

        public IActionResult Index()
        {
            Clientes cliente = new Clientes();
            ViewBag.Clientes = ctx.Clientes.ToList();
            return View(cliente);
        }

        [BindProperty]
        public Clientes Cliente {get;set;}
        public IActionResult Guardar(string Clave)
        {
            if(!ModelState.IsValid)
            {
                return Redirect("/Clientes/");
            }
            var _Cliente = ctx.Clientes.Where(x=>x.IdCliente == Cliente.IdCliente).SingleOrDefault();
            if(_Cliente == null)
            {
                ctx.Clientes.Add(Cliente);
            }
            else
            {
                _Cliente.Correo = Cliente.Correo;
                _Cliente.Dui = Cliente.Dui;
                _Cliente.Nit = Cliente.Nit;
                _Cliente.Nombre = Cliente.Nombre;
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Modificar(int id)
        {
            var Cliente = ctx.Clientes.Find(id);
            //var Cliente = ctx.Clientes.Where(x=>x.IdCliente == id).SingleOrDefault()
            if(Cliente == null)
            {
                return Redirect("/Clientes/");
            }            
            return View(Cliente);
        }

        public IActionResult Eliminar(int id)
        {
            var Cliente = ctx.Clientes.Find(id);
            if(Cliente == null)
            {
                return StatusCode(404);
            }
            ctx.Clientes.Remove(Cliente);
            ctx.SaveChanges();
            return Redirect("/Clientes/");
        }
    }
}