using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiIngresoHitss.Data;
using MiIngresoHitss.Models;
using MiIngresoHitss.Repositorio;
using MiIngresoHitss.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MiIngresoHitss.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ICompraRepository _compraRepository;

        public HomeController(IProductoRepository productoRepository,IClienteRepository clienteRepository,ICompraRepository compraRepository)
        {
            _productoRepository = productoRepository;
            _clienteRepository = clienteRepository;
            _compraRepository = compraRepository;
        }
        public IActionResult AgregarProducto()
        {
            var producto = new Producto(); 
            return View(producto);
        }
        public IActionResult EliminarProducto(int productoId)
        {
            Producto producto = _productoRepository.ObtenerPorId(productoId);

            if (producto != null)
            {
                _productoRepository.Eliminar(productoId);
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditarProducto(int productoId)
        {
            Producto producto = _productoRepository.ObtenerPorId(productoId);

            if (producto != null)
            {
                return View(producto);
            }

            return RedirectToAction("Index");
        }
        public IActionResult VerProductosComprados()
        {
            var productosComprados = _productoRepository.ObtenerProductosComprados();

            return View(productosComprados);
        }

        [HttpPost]
        public IActionResult EditarProducto(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _productoRepository.Actualizar(producto);
                return RedirectToAction("Index");
            }

            return View(producto);
        }
        [HttpPost]
        public IActionResult AgregarProducto(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _productoRepository.Crear(producto);
                return RedirectToAction("Index");
            }

            return View(producto);
        }
        public IActionResult Index()
        {
            var viewModel = new CatalogoViewModel
            {
                Productos = _productoRepository.ObtenerTodos().ToList(),
                Carrito = new List<Producto>()
            };

            return View(viewModel);
        }

        public IActionResult Carrito()
        {
            var viewModel = new CatalogoViewModel
            {
                Carrito = HttpContext.Session.GetObject<List<Producto>>("Carrito") ?? new List<Producto>(),
                Clientes = _clienteRepository.ObtenerTodos().ToList()
            };

            ViewData["Clientes"] = viewModel.Clientes;

            return View(viewModel);
        }

        public IActionResult EliminarDelCarrito(int productoId)
        {
            List<Producto> carrito = HttpContext.Session.GetObject<List<Producto>>("Carrito") ?? new List<Producto>();

            Producto producto = carrito.FirstOrDefault(p => p.Id == productoId);
            if (producto != null)
            {
                carrito.Remove(producto);
                HttpContext.Session.SetObject("Carrito", carrito);
            }

            return RedirectToAction("Carrito");
        }
        [HttpPost]
        public IActionResult Comprar(int clienteId)
        {
            List<Producto> carrito = HttpContext.Session.GetObject<List<Producto>>("Carrito") ?? new List<Producto>();

            // Obtener el cliente por su Id
            Cliente cliente = _clienteRepository.ObtenerPorId(clienteId);

            if (cliente == null)
            {
                return NotFound(); // Maneja el caso si no se encuentra el cliente
            }

            // Crear una nueva compra para cada producto en el carrito
            foreach (Producto producto in carrito)
            {
                Compra compra = new Compra
                {
                    ClienteId = clienteId,
                    ProductoId = producto.Id,
                    FechaCompra = DateTime.Now
                };

                // Agregar la compra a la base de datos
                _compraRepository.Agregar(compra);
            }

            // Limpiar el carrito
            HttpContext.Session.SetObject("Carrito", new List<Producto>());

            return RedirectToAction("Carrito");
        }
        public IActionResult AgregarAlCarrito(int productoId)
        {
            Producto producto = _productoRepository.ObtenerPorId(productoId);

            if (producto != null)
            {
                List<Producto> carrito = HttpContext.Session.GetObject<List<Producto>>("Carrito") ?? new List<Producto>();
                carrito.Add(producto);
                HttpContext.Session.SetObject("Carrito", carrito);
            }

            return RedirectToAction("Index");
        }
    }
}
