using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIEA_System.Data;
using LIEA_System.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LIEA_System.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly AppDBContext _context;

        public ProductoesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Productos.Include(p => p.Proveedor);
            return View(await appDBContext.ToListAsync());
        }







        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["Id_Proveedor"] = new SelectList(_context.Proveedores, "Id_Proveedor", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto, IFormFile Imagen)
        {
            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                if(Imagen != null && Imagen.Length>0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await Imagen.CopyToAsync(ms);
                        producto.Imagen = ms.ToArray(); // Convertir la imagen a bytes y asignarla a la propiedad Imagen de la persona
                    }
                }
                
                ViewData["Id_Proveedor"] = new SelectList(_context.Proveedores, "Id_Proveedor", "Nombre", producto.Id_Proveedor);
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                TempData["Agregar"] = "¡Registro Exitoso!";
                return RedirectToAction(nameof(Index));
            }
        }












        // GET: Productoes/Edit/5
        public IActionResult Edit(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Id_Proveedor"] = new SelectList(_context.Proveedores, "Id_Proveedor", "Nombre"/*, producto.Id_Proveedor*/);
                return View(producto);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Producto producto, IFormFile Imagen)
        {
            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Id_Proveedor"] = new SelectList(_context.Proveedores, "Id_Proveedor", "Nombre", producto.Id_Proveedor);
                
                // Buscar la persona en la base de datos
                var personaEnBD = await _context.Productos.FindAsync(producto.Id_Producto);
                if (personaEnBD == null)
                {
                    return NotFound();
                }

                // Actualizar propiedades de la persona con los nuevos valores
                personaEnBD.Nombre = producto.Nombre;
                personaEnBD.Tipo = producto.Tipo;
                personaEnBD.Genero = producto.Genero;
                personaEnBD.Talla = producto.Talla;
                personaEnBD.Color = producto.Color;
                personaEnBD.Modelo = producto.Modelo;
                personaEnBD.Precio = producto.Precio;
                personaEnBD.Existencias = producto.Existencias;

                // Si se cargó una nueva imagen, actualizarla
                if (Imagen != null && Imagen.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await Imagen.CopyToAsync(ms);
                        personaEnBD.Imagen = ms.ToArray();
                    }
                }
                personaEnBD.Id_Proveedor = producto.Id_Proveedor;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                TempData["Actualizado"] = "Actualizacion";

                // Redirigir a la vista Index
                return RedirectToAction(nameof(Index));
            }           
            
        }











        // GET: Productoes/Delete/5
        public IActionResult Delete(int id)
        {
            //buscar la persona x id
            var buscarProduct = _context.Productos.Find(id);
            if (buscarProduct == null)
            {
                return NotFound();
            }
            else
            {
                return View(buscarProduct);
            }
        }

        // POST: Productoes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuitarProduct(Producto producto)
        {
            //validar que lleve el objeto persona completo
            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                //se actualiza en la bd SQL Server
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                TempData["Eliminar"] = "Eliminado";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id_Producto == id);
        }
    }
}
