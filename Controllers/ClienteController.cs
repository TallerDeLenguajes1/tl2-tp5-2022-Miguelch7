using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using SistemaCadeteriaMVC.Models;
using SistemaCadeteriaMVC.ViewModels;

namespace SistemaCadeteriaMVC.Controllers;

public class ClienteController : Controller
{
    private readonly ILogger<ClienteController> _logger;
    private IMapper _mapper;
    private readonly IRepositorioClientes _repositorioClientes;

    public ClienteController(ILogger<ClienteController> logger, IMapper mapper, IRepositorioClientes repositorioClientes)
    {
        _logger = logger;
        _mapper = mapper;
        _repositorioClientes = repositorioClientes;
    }

    public IActionResult Index()
    {
        var clientes = _repositorioClientes.ObtenerClientes();

        var clientesViewModel = _mapper.Map<List<ClienteViewModel>>(clientes);

        ListaClientesViewModel listaClientesViewModel = new ListaClientesViewModel(clientesViewModel);
    
        return View(listaClientesViewModel);
    }

    [HttpGet]
    public IActionResult CrearCliente()
    {
        return View(new ClienteViewModel());
    }

    [HttpPost]
    public IActionResult CrearCliente(ClienteViewModel clienteViewModel)
    {
        if (ModelState.IsValid)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            _repositorioClientes.AgregarCliente(cliente);
            return RedirectToAction("Index");
        }
        return View("CrearCliente", clienteViewModel);
    }

    public IActionResult EditarCliente(int id)
    {
        var clientes = _repositorioClientes.ObtenerClientes();

        Cliente? cliente = clientes.Find(c => c.Id == id);

        if (cliente != null)
        {
            var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);

            return View(clienteViewModel);
        } 
        else 
        {
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult EditarCliente(ClienteViewModel clienteViewModel)
    {
        if (ModelState.IsValid)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);

            _repositorioClientes.EditarCliente(cliente);

            return RedirectToAction("Index");
        }

        return View("EditarCliente", clienteViewModel);
    }

    [HttpGet]
    public IActionResult EliminarCliente(int id)
    {
        _repositorioClientes.EliminarCliente(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}