using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class ComprasController:Controller
    {
        TiendaCTX ctx;
        Response res = new Response();
        public ComprasController(TiendaCTX _ctx)
        {
            ctx = _ctx;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Compras = await ctx.Compras.Include("DetalleCompra").Include(x=>x.Proveedor).ToListAsync();
            return View();
        }

        public async Task<IActionResult> Nuevo(int? id)
        {
            var IdCompra = id ?? 0;
            var _Compra = await ctx.Compras.FindAsync(IdCompra);
            var Compra = new Compras();
            if(_Compra != null)
            {
                Compra = _Compra;
            }
            ViewBag.Proveedores = await ctx.Proveedores.ToListAsync();
            ViewBag.Productos = await ctx.Productos.Select(x=>new {x.IdProducto, NombreProducto = x.Nombre + " - " + x.Marca}).ToListAsync();
            Tuple<Compras, DetalleCompra> Model = new Tuple<Compras, DetalleCompra>(Compra, new DetalleCompra());
            ViewBag.DetalleCompra = await ctx.DetalleCompra.Include(x=>x.Producto).Where(x=>x.IdCompra == IdCompra).ToListAsync();
            return View(Model);
        }

        public async Task<IActionResult> SetEncabezado([Bind(Prefix="Item1")] Compras Compra)
        {
            if(!ModelState.IsValid)
            {
                res.estado = false;
                res.mensaje = "Rellene los campos solicitados.";
                return Json(res);
            }
            var _Compra = await ctx.Compras.FindAsync(Compra.IdCompra);
            if(_Compra == null)
            {
                ctx.Compras.Add(Compra);
                await ctx.SaveChangesAsync();
            }
            else
            {
                _Compra.IdProveedor = Compra.IdProveedor;
                _Compra.NumeroFactura = Compra.NumeroFactura;
                await ctx.SaveChangesAsync();
            }
            res.resultado = Compra.IdCompra;
            res.estado = true;
            return Json(res);
        }

        public async Task<IActionResult> SetDetalle([Bind(Prefix="Item2")] DetalleCompra Detalle)
        {
            if(!ModelState.IsValid)
            {
                res.estado = false;
                res.mensaje = "Rellene los campos solicitados.";
                return Json(res);
            }
            Detalle.Iva = Detalle.Cantidad * Detalle.Precio * 0.13M;
            ctx.DetalleCompra.Add(Detalle);
            await ctx.SaveChangesAsync();
            Detalle.Producto = await ctx.Productos.FindAsync(Detalle.IdProducto);
            res.resultado = Detalle;
            res.mensaje = "El producto ha sido agregado.";
            return Json(res);
        }
    }
}