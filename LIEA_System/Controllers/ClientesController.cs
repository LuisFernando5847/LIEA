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
    public class ClientesController : Controller
    {
        private readonly AppDBContext _context;

        public ClientesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // Apartado para agregar
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> agregarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                TempData["Agregar"] = "¡Exitoso!";
                return RedirectToAction(nameof(Index));
            }
        }


        // Apartado para Editar
        public IActionResult Edit(int id)
        {
            var buscarCliente = _context.Clientes.Find(id);

            if (buscarCliente == null)
            {
                return NotFound();
            }
            else
            {
                return View(buscarCliente);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> actualizarDatos(Cliente cliente)
        {
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
                TempData["Actualizado"] = "Actualizacion con exito";
                return RedirectToAction(nameof(Index));

            }
        }

        // GET: Clientes/Delete/5
        public IActionResult Delete(int id)
        {
            var buscarCliente = _context.Clientes.Find(id);

            if (buscarCliente == null)
            {
                return NotFound();
            }
            else
            {
                return View(buscarCliente);
            }
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> elimnarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                //se actualiza en la bd SQL Server
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                TempData["Eliminar"] = "Cliente Eliminado";
                return RedirectToAction(nameof(Index));
            }

            
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id_Cliente == id);
        }
    }
}
