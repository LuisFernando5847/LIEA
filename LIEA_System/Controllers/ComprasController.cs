using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIEA_System.Data;
using LIEA_System.Models;

namespace LIEA_System.Controllers
{
    public class ComprasController : Controller
    {
        private readonly AppDBContext _context;

        public ComprasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Compras.Include(c => c.Proveedor);
            return View(await appDBContext.ToListAsync());
        }





















        // GET: Compras/Create
        public IActionResult Create()
        {
            var compra = new Compra
            {
                Fecha_Compra = DateTime.Now.Date,
                Hora_Compra = DateTime.Now.TimeOfDay
            };
            ViewData["Id_Proveedor"] = new SelectList(_context.Proveedores, "Id_Proveedor", "Nombre");
            ViewBag.Productos = _context.Productos.ToList();
            return View(compra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Compra compra)
        {
            if (compra == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Id_Proveedor"] = new SelectList(_context.Proveedores, "Id_Proveedor", "Nombre", compra.Id_Proveedor);
                ViewBag.Productos = _context.Productos.ToList();
                // Calcular el total de la compra
                compra.Total = compra.DetallesCompra.Sum(dc => dc.Cantidad * dc.P_Unitario);
                _context.Compras.Add(compra);

                // Actualizar el stock 
                foreach (var detalle in compra.DetallesCompra)
                {
                    var producto = _context.Productos.Find(detalle.Id_Producto);
                    if (producto != null)
                    {
                        producto.Existencias += detalle.Cantidad;
                        _context.Productos.Update(producto);
                    }
                }

                _context.SaveChanges();
                TempData["Agregar"] = "La compra tuvo exito";
                return RedirectToAction(nameof(Index));

            }
        }

































        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["Id_Proveedor"] = new SelectList(_context.Proveedores, "Id_Proveedor", "Id_Proveedor", compra.Id_Proveedor);
            return View(compra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Compra,Fecha_Compra,Hora_Compra,Id_Proveedor,Total")] Compra compra)
        {
            if (id != compra.Id_Compra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.Id_Compra))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Proveedor"] = new SelectList(_context.Proveedores, "Id_Proveedor", "Id_Proveedor", compra.Id_Proveedor);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.Id_Compra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.Id_Compra == id);
        }
    }
}
