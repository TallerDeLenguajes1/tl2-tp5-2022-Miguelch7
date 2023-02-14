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

public class PedidoController : Controller
{
    private readonly ILogger<CadeteController> _logger;
    private IMapper _mapper;
    private readonly IRepositorioPedidos _repositorioPedidos;

    public PedidoController(ILogger<CadeteController> logger, IMapper mapper, IRepositorioPedidos repositorioPedidos)
    {
        _logger = logger;
        _mapper = mapper;
        _repositorioPedidos = repositorioPedidos;
    }

    public IActionResult Index()
    {
        try 
        {
            if (HttpContext.Session.GetString("username") == null) return RedirectToAction("IniciarSesion", "Login");

            var pedidos = _repositorioPedidos.ObtenerPedidos();

            var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

            ListaPedidosViewModel listadoPedidosViewModel = new ListaPedidosViewModel(pedidosViewModel);
        
            return View(listadoPedidosViewModel);
        }
        catch (System.Exception)
        {
            return View(new ListaPedidosViewModel(new List<PedidoViewModel>()));
        }
    }

    [HttpGet]
    public IActionResult CrearPedido()
    {
        string? rol = HttpContext.Session.GetString("rol");
        if(rol != "Administrador") return RedirectToAction("Index");
        
        return View(new PedidoViewModel());
    }

    [HttpPost]
    public IActionResult CrearPedido(PedidoViewModel pedidoViewModel)
    {
        try
        {
            string? rol = HttpContext.Session.GetString("rol");
            if(rol != "Administrador") return RedirectToAction("Index");
            
            if (ModelState.IsValid)
            {
                var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                _repositorioPedidos.AgregarPedido(pedido);
                return RedirectToAction("Index");
            }
            return View("CrearPedido", pedidoViewModel);
        }
        catch (System.Exception)
        {
            return RedirectToAction("Index");
        }
    }

    public IActionResult EditarPedido(int numeroPedido)
    {
        try
        {
            string? rol = HttpContext.Session.GetString("rol");
            if(rol != "Administrador") return RedirectToAction("Index");

            Pedido? pedido = _repositorioPedidos.ObtenerPedidoPorNumeroPedido(numeroPedido);

            if (pedido != null)
            {
                var pedidoViewModel = _mapper.Map<PedidoViewModel>(pedido);

                return View(pedidoViewModel);
            } 
            else 
            {
                return RedirectToAction("Index");
            }
        }
        catch (System.Exception)
        {
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult EditarPedido(PedidoViewModel pedidoViewModel)
    {
        try
        {
            string? rol = HttpContext.Session.GetString("rol");
            if(rol != "Administrador") return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                var pedido = _mapper.Map<Pedido>(pedidoViewModel);

                _repositorioPedidos.EditarPedido(pedido);

                return RedirectToAction("Index");
            }

            return View("EditarPedido", pedidoViewModel);
        }
        catch (System.Exception)
        {
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult EliminarPedido(int numeroPedido)
    {
        try
        {
            string? rol = HttpContext.Session.GetString("rol");
            if(rol != "Administrador") return RedirectToAction("Index");

            _repositorioPedidos.EliminarPedido(numeroPedido);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return RedirectToAction("Index");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}