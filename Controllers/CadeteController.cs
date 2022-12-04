using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaCadeteriaMVC.Models;

namespace SistemaCadeteriaMVC.Controllers;

public class CadeteController : Controller
{
    private readonly ILogger<CadeteController> _logger;
    private static List<Cadete> ListadoCadetes = new List<Cadete>()!;

    public CadeteController(ILogger<CadeteController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(ListadoCadetes);
    }

    [HttpGet]
    public IActionResult RestaurarDatos()
    {
        string[] array;
        List<Cadete> ListadoCadetesBackUp = new List<Cadete>();
        
        foreach (string s in System.IO.File.ReadAllLines("CSV/cadetes.csv"))
        {
            if (s != "")
            {
                array = s.Split(";");
                ListadoCadetesBackUp.Add(
                    new Cadete(Int32.Parse(array[0]), array[1], array[2], array[3])
                );
            }
        }

        ListadoCadetes = ListadoCadetesBackUp;

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult CrearCadete()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearCadete(Cadete cadete)
    {
        ListadoCadetes!.Add(cadete);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EliminarCadete(int id)
    {
        ListadoCadetes!.Remove(
            ListadoCadetes!.Find(cadete => cadete!.Id == id)!
        );
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}