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
    public class VentasController : Controller
    {
        private readonly AppDBContext _context;

        public VentasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Ventas.Include(v => v.Cliente);
            return View(await appDBContext.ToListAsync());
        }



































        // GET: Ventas/Create
        public IActionResult Create()
        {
            var venta = new Venta
            {
                Fecha_Venta = DateTime.Now.Date,
                Hora_Venta = DateTime.Now.TimeOfDay
            };

            ViewData["Id_Cliente"] = new SelectList(_context.Clientes, "Id_Cliente", "Nombre");
            ViewBag.Productos = _context.Productos.ToList();
            return View(venta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venta venta)
        {
            if (venta == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Id_Cliente"] = new SelectList(_context.Clientes, "Id_Cliente", "Nombre", venta.Id_Cliente);
               /* if (venta.DetallesVenta == null || !venta.DetallesVenta.Any())
                {
                    ModelState.AddModelError("", "Debe agregar al menos un detalle de venta.");
                    ViewBag.Productos = _context.Productos.ToList();
                    return View(venta);
                }
                */
                ViewBag.Productos = _context.Productos.ToList();

                // Calcular el total de la venta
                venta.Total = venta.DetallesVenta.Sum(dv => dv.Cantidad * dv.P_Unitario);
                _context.Ventas.Add(venta);

                // Actualizar el stock
                foreach (var detalle in venta.DetallesVenta)
                {
                    var producto = _context.Productos.Find(detalle.Id_Producto);
                    if (producto != null)
                    {
                        if (producto.Existencias < detalle.Cantidad)
                        {
                            ModelState.AddModelError("", $"No hay suficientes existencias de {producto.Nombre}. Existencias actuales: {producto.Existencias}");
                            return View(venta);
                        }

                        producto.Existencias -= detalle.Cantidad;
                        _context.Productos.Update(producto);
                    }
                }

            }           

            await _context.SaveChangesAsync();
            TempData["Agregar"] = "Venta realizada con exito";
            return RedirectToAction(nameof(Index));
        }





































        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["Id_Cliente"] = new SelectList(_context.Clientes, "Id_Cliente", "Id_Cliente", venta.Id_Cliente);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Venta,Fecha_Venta,Hora_Venta,Id_Cliente,Total")] Venta venta)
        {
            if (id != venta.Id_Venta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.Id_Venta))
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
            ViewData["Id_Cliente"] = new SelectList(_context.Clientes, "Id_Cliente", "Id_Cliente", venta.Id_Cliente);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.Id_Venta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.Id_Venta == id);
        }
    }
}
