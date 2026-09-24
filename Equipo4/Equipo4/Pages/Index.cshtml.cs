using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Equipo4.Serializado;
using Equipo4.Models;

namespace Equipo4.Pages
{
    public class IndexModel : PageModel
    {
        // Aqui es donde van los pollos:
        public List<Producto> Productos { get; } = new List<Producto>
        {
            new Producto { Sku = "SKU-001", Nombre = "Combo Individual", Precio = 130.00m, Descripcion = "2 piezas de pollo, tortillas y totopos", ImagenUrl = "/images/combo_individual.jpg" },
            new Producto { Sku = "SKU-002", Nombre = "Combo Mediano", Precio = 260.00m, Descripcion = "Medio pollo (4 piezas), tortillas, salchicha y totopos", ImagenUrl = "/images/combo_mediano.jpg" },
            new Producto { Sku = "SKU-003", Nombre = "Combo Familiar", Precio = 400.00m, Descripcion = "Pollo entero (8 piezas), tortillas, salchicha y totopos", ImagenUrl = "/images/combo_familiar.jpg" },
            new Producto { Sku = "SKU-004", Nombre = "Quesadilla", Precio = 45.00m, Descripcion = "Quesadilla en tortilla grande con queso asadero", ImagenUrl = "/images/quesadilla.jpeg" },
            new Producto { Sku = "SKU-005", Nombre = "Salchicha Polaca", Precio = 45.00m, Descripcion = "Orden individual asada al carbón", ImagenUrl = "/images/salchicha_polaca.jpeg" },
            new Producto { Sku = "SKU-006", Nombre = "Salchicha Roja", Precio = 35.00m, Descripcion = "Orden individual asada al carbón", ImagenUrl = "/images/salchicha_roja.jpeg" }
        };

        public Carrito Carrito { get; set; } = new Carrito(); // instancia carrito para que no sea null

        public void OnGet() // instancio carrito para cada "refresco" de página
        {
            Carrito = HttpContext.Session.GetObject<Carrito>("carrito") ?? new Carrito(); // Si hay objeto deserializado ==, si no new objeto
        }
        
        public IActionResult OnPostAgregar(string sku)
        {
            var producto = Productos.FirstOrDefault(p => p.Sku == sku);

            if (producto is null) return NotFound();

            var carrito = HttpContext.Session.GetObject<Carrito>("carrito") ?? new Carrito();
            carrito.Agregar(producto);
            HttpContext.Session.SetObject("carrito", carrito);

            return RedirectToPage();
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
