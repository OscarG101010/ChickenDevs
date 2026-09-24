namespace Equipo4.Models
{
    public class Carrito
    {
        public List<ItemCarrito> Items { get; set; } = new List<ItemCarrito>();

        public const decimal TasaIva = 0.16m;
        public decimal GranSubtotal => Items.Sum(i => i.Subtotal);
        public decimal Iva => Math.Round(GranSubtotal * TasaIva, 2);
        public decimal Total => GranSubtotal + Iva;

        public void Agregar(Producto p)
        {
            var item = Items.FirstOrDefault(i => i.Producto.Sku == p.Sku);

            if (item is null)
                Items.Add(new ItemCarrito { Producto = p, Cantidad = 1 });
            else
                item.Cantidad++;
        }

        public void Quitar(string sku) => Items.RemoveAll(i => i.Producto.Sku == sku); // elimina todos los elementos que cumplan la condición
    }

    public class ItemCarrito
    {
        public Producto Producto { get; set; } = new();
        public int Cantidad { get; set; }
        public decimal Subtotal => Producto.Precio * Cantidad;
    }
}
