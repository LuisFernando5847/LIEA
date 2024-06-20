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
    public class ProveedorsController : Controller
    {
        private readonly AppDBContext _context;

        public ProveedorsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Proveedors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proveedores.ToListAsync());
        }

        // GET: Proveedors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                return NotFound();  
            }
            else
            {
                _context.Proveedores.Add(proveedor);
                await _context.SaveChangesAsync();
                TempData["Agregar"] = "¡Exitoso!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Proveedors/Edit/5
        public IActionResult Edit(int id)
        {
            var buscarProveedor = _context.Proveedores.Find(id);
            if (buscarProveedor == null)
            {
                return NotFound();
            }
            else
            {
                return View(buscarProveedor);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                return NotFound();
            }
            else
            {
                _context.Proveedores.Update(proveedor);
                await _context.SaveChangesAsync();
                TempData["Actualizado"] = "Actualizacion con exito";
                return RedirectToAction(nameof(Index));

            }
        }

        // GET: Proveedors/Delete/5
        public IActionResult Delete(int id)
        {
            var buscarProveedor = _context.Clientes.Find(id);

            if (buscarProveedor == null)
            {
                return NotFound();
            }
            else
            {
                return View(buscarProveedor);
            }
        }

        // POST: Proveedors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                return NotFound();
            }
            else
            {
                //se actualiza en la bd SQL Server
                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();
                TempData["Eliminar"] = "Cliente Eliminado";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.Id_Proveedor == id);
        }
    }
}
