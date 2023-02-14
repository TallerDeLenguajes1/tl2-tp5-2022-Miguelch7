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

public class CadeteController : Controller
{
    private readonly ILogger<CadeteController> _logger;
    private IMapper _mapper;
    private readonly IRepositorioCadetes _repositorioCadetes;

    public CadeteController(ILogger<CadeteController> logger, IMapper mapper, IRepositorioCadetes repositorioCadetes)
    {
        _logger = logger;
        _mapper = mapper;
        _repositorioCadetes = repositorioCadetes;
    }

    public IActionResult Index()
    {
        try 
        {
            if (HttpContext.Session.GetString("username") == null) return RedirectToAction("IniciarSesion", "Login");
            
            var cadetes = _repositorioCadetes.ObtenerCadetes();

            var cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);

            ListaCadetesViewModel listaCadetesViewModel = new ListaCadetesViewModel(cadetesViewModel);
        
            return View(listaCadetesViewModel);
        }
        catch (System.Exception)
        {
            return View(new ListaCadetesViewModel(new List<CadeteViewModel>()));
        }
    }

    [HttpGet]
    public IActionResult CrearCadete()
    {
        string? rol = HttpContext.Session.GetString("rol");
        if (rol != "Administrador") return RedirectToAction("Index");

        return View(new CadeteViewModel());
    }

    [HttpPost]
    public IActionResult CrearCadete(CadeteViewModel cadeteViewModel)
    {
        try
        {
            string? rol = HttpContext.Session.GetString("rol");
            if (rol != "Administrador") return RedirectToAction("Index", "Cadete");

            if (ModelState.IsValid)
            {
                var cadete = _mapper.Map<Cadete>(cadeteViewModel);
                _repositorioCadetes.AgregarCadete(cadete);
                return RedirectToAction("Index");
            }
            return View("CrearCadete", cadeteViewModel);
        }
        catch (System.Exception)
        {
            return RedirectToAction("Index");
        }
    }

    public IActionResult EditarCadete(int id)
    {
        try
        {
            string? rol = HttpContext.Session.GetString("rol");
            if (rol != "Administrador") return RedirectToAction("Index");
            var cadetes = _repositorioCadetes.ObtenerCadetes();

            Cadete? cadete = cadetes.Find(c => c.Id == id);

            if (cadete != null)
            {
                var cadeteViewModel = _mapper.Map<CadeteViewModel>(cadete);

                return View(cadeteViewModel);
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
    public IActionResult EditarCadete(CadeteViewModel cadeteViewModel)
    {
        try
        {
            string? rol = HttpContext.Session.GetString("rol");
            if (rol != "Administrador") return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                var cadete = _mapper.Map<Cadete>(cadeteViewModel);

                _repositorioCadetes.EditarCadete(cadete);

                return RedirectToAction("Index");
            }

            return View("EditarCadete", cadeteViewModel);
        }
        catch (System.Exception)
        {
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult EliminarCadete(int id)
    {
        try
        {
            string? rol = HttpContext.Session.GetString("rol");
            if (rol != "Administrador") return RedirectToAction("Index");

            _repositorioCadetes.EliminarCadete(id);
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