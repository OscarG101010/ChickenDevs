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
            new Producto { Sku = "SKU-001", Nombre = "Combo Familiar", Precio = 600.00m, Descripcion = "Pollo entero (8 piezas), tortillas, salchicha y totopos", ImagenUrl = "/images/combo_1.jpg" },
            new Producto { Sku = "SKU-002", Nombre = "Combo Pareja", Precio = 334.50m, Descripcion = "Medio pollo (4 piezas), tortillas, salchicha y totopos", ImagenUrl = "/images/combo_1.jpg" },
            new Producto { Sku = "SKU-003", Nombre = "Combo Individual", Precio = 135.00m, Descripcion = "2 piezas de pollo, tortillas y totopos", ImagenUrl = "/images/combo_1.jpg" },
            new Producto { Sku ="SKU-005", Nombre = "Pollo entero", Precio = 449.99m, Descripcion = "8 piezas de pollo", ImagenUrl = "/images/pollo_entero.jpg" },
            new Producto { Sku ="SKU-004", Nombre = "Medio pollo", Precio = 249.99m, Descripcion = "4 piezas de pollo", ImagenUrl = "/images/combo_1.jpg" },  
            new Producto { Sku ="SKU-006", Nombre = "Pechuga de pollo", Precio = 79.99m, Descripcion = "2 piezas de pechuga de pollo", ImagenUrl = "/images/combo_1.jpg" },
            new Producto { Sku ="SKU-007", Nombre = "Tortillas", Precio = 49.99m, Descripcion = "medio kilo de tortillas", ImagenUrl = "/images/combo_1.jpg" },
            new Producto { Sku ="SKU-008", Nombre = "Salchicha", Precio = 39.99m, Descripcion = "2 piezas de salchicha", ImagenUrl = "/images/combo_1.jpg" },
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
