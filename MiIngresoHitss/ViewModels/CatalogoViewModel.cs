using MiIngresoHitss.Models;

namespace MiIngresoHitss.ViewModels
{
    public class CatalogoViewModel
    {
        public List<Producto> Productos { get; set; }
        public List<Producto> Carrito { get; set; }
        public List<Cliente> Clientes { get; set; }
    }
}
