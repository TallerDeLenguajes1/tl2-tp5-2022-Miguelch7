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
    private static ListaPedidos ListadoPedidos = new ListaPedidos();

    public PedidoController(ILogger<CadeteController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var pedidos = ListadoPedidos.ObtenerPedidos();

        var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

        ListaPedidosViewModel listadoPedidosViewModel = new ListaPedidosViewModel(pedidosViewModel);
    
        return View(listadoPedidosViewModel);
    }

    [HttpGet]
    public IActionResult CrearPedido()
    {
        return View(new PedidoViewModel());
    }

    [HttpPost]
    public IActionResult CrearPedido(PedidoViewModel pedidoViewModel)
    {
        if (ModelState.IsValid)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            ListadoPedidos.AgregarPedido(pedido);
            return RedirectToAction("Index");
        }
        return View("CrearPedido", pedidoViewModel);
    }

    public IActionResult EditarPedido(int numeroPedido)
    {
        var pedidos = ListadoPedidos.ObtenerPedidos();

        Pedido? pedido = pedidos.Find(p => p.NumeroPedido == numeroPedido);

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

    [HttpPost]
    public IActionResult EditarPedido(PedidoViewModel pedidoViewModel)
    {
        if (ModelState.IsValid)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);

            ListadoPedidos.EditarPedido(pedido);

            return RedirectToAction("Index");
        }

        return View("EditarPedido", pedidoViewModel);
    }

    [HttpGet]
    public IActionResult EliminarPedido(int numeroPedido)
    {
        ListadoPedidos.EliminarPedido(numeroPedido);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}