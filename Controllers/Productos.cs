using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda.Models;
namespace Tienda.Controllers
{
    public class ProductosController:Controller
    {
        TiendaCTX ctx;
        public ProductosController(TiendaCTX _ctx)
        {
            ctx = _ctx;
        }

        public async Task<IActionResult> Index()
        {
            List<Productos> Productos = await ctx.Productos.Include(x=>x.Categoria).ToListAsync();
            Productos Producto = new Productos();
            ViewBag.Productos = Productos;
            ViewBag.Categorias = await ctx.Categorias.OrderBy(x=>x.Categoria).ToListAsync();
            return View(Producto);
        }

        [BindProperty]
        public Productos Producto {get; set;}
        public async Task<IActionResult> SetProducto()
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var _Producto = await ctx.Productos.Where(x=>x.IdProducto == Producto.IdProducto).AnyAsync();
            if(!_Producto)
            {
                ctx.Productos.Add(Producto);
            }
            else
            {
                ctx.Productos.Attach(Producto);
                ctx.Entry(Producto).State = EntityState.Modified;
            }            
            await ctx.SaveChangesAsync();
            return Redirect("/Productos");
        }

        public async Task<IActionResult> Modificar(int id)
        {
            var Producto = await ctx.Productos.FindAsync(id);
            if(Producto == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = await ctx.Categorias.OrderBy(x=>x.Categoria).ToListAsync();
            return View(Producto);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var _Producto = await ctx.Productos.FindAsync(id);
            if(_Producto == null)
            {
                return RedirectToAction("Index");
            }
            else
                ctx.Productos.Remove(_Producto);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Inventario()
        {
            var Productos = await ctx.DetalleCompra.Include(x=>x.Producto).Sum(x=>x.Cantidad).GroupBy(x=>x.)
            return View();
        }
    }
}