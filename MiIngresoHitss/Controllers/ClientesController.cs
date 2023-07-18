using Microsoft.AspNetCore.Mvc;
using MiIngresoHitss.Models;
using MiIngresoHitss.Repositorio;
using System.Collections.Generic;

namespace MiIngresoHitss.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            List<Cliente> clientes = _clienteRepository.ObtenerTodos().ToList();
            return View(clientes);
        }

        public IActionResult Crear()
        {
            var cliente = new Cliente(); 
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.Crear(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        public IActionResult Editar(int id)
        {
            Cliente cliente = _clienteRepository.ObtenerPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.Actualizar(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        public IActionResult Borrar(int id)
        {
            Cliente cliente = _clienteRepository.ObtenerPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Borrar")]
        public IActionResult ConfirmarBorrar(int id)
        {
            _clienteRepository.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}
