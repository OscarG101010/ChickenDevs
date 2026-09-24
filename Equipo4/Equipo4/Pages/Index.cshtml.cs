using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Equipo4.Serializado;
using Equipo4.Models;

namespace Equipo4.Pages
{
    public class IndexModel : PageModel
    {
        // Aqui es donde van los pollos:
        public List<Producto> Productos { get; } = new()
        {
            new() { Sku = "SKU-001", Nombre = "Teclado mecánico", Precio = 899.00m, ImagenUrl = "/img/teclado.jpg" },
            new() { Sku = "SKU-002", Nombre = "Mouse inalámbrico", Precio = 349.50m, ImagenUrl = "/img/mouse.jpg" },
            new() { Sku = "SKU-003", Nombre = "Monitor 24\"", Precio = 2599.00m, ImagenUrl = "/img/monitor.jpg" },
        };

        public Carrito Carrito { get; set; } = new(); // instancia carrito para que no sea null

        public void OnGet() // instancio carrito para cada "refresco" de página
        {
            Carrito = HttpContext.Session.GetObject<Carrito>("carrito") ?? new Carrito(); // Si hay objeto deserializado ==, si no new objeto
        }

        public IActionResult OnPostQuitar(string sku)
        {
            var carrito = HttpContext.Session.GetObject<Carrito>("carrito") ?? new Carrito(); // Si hay objeto deserializado ==, si no new objeto
            
            carrito.Quitar(sku);

            // funciona cómo diccionario clave : valor
            HttpContext.Session.SetObject("carrito", carrito); // actualiza el objeto carrito en sesión
            
            return RedirectToPage();
        }
    }
}
