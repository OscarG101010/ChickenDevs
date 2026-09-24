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
            new() { Sku = "SKU-001", Nombre = "Combo Individual", Precio = 130.00m, ImagenUrl = "/images/combo_individual.jpg" },
            new() { Sku = "SKU-002", Nombre = "Combo Mediano", Precio = 260.00m, ImagenUrl = "/images/combo_mediano.jpg" },
            new() { Sku = "SKU-003", Nombre = "Combo Familiar", Precio = 400.00m, ImagenUrl = "/images/combo_familiar.jpg" },
            new() { Sku = "SKU-004", Nombre = "Quesadilla", Precio = 20.00m, ImagenUrl = "/images/quesadilla.jpeg" },
            new() { Sku = "SKU-005", Nombre = "Salchicha Polaca", Precio = 45.00m, ImagenUrl = "/images/salchicha_polaca.jpeg" },
            new() { Sku = "SKU-006", Nombre = "Salchicha Roja", Precio = 35.00m, ImagenUrl = "/images/salchicha_roja.jpeg" }
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
