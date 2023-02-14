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
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace SistemaCadeteriaMVC.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IRepositorioUsuarios _repositorioUsuarios;

    public LoginController(ILogger<LoginController> logger, IRepositorioUsuarios repositorioUsuarios)
    {
      _logger = logger;
      _repositorioUsuarios = repositorioUsuarios;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult IniciarSesion()
    {
      if (HttpContext.Session.GetString("username") == null)
      {
        return View(new UsuarioViewModel());
      }
      else
      {
        return RedirectToAction("Index", "Cadete");
      }
    }

    [HttpPost]
    public IActionResult IniciarSesion(UsuarioViewModel usuarioViewModel)
    {
      try
      {
        if (ModelState.IsValid)
        {
          UsuarioViewModel? user = _repositorioUsuarios.Login(usuarioViewModel.Username!, usuarioViewModel.Password!);

          if (user.Id != -1)
          {
            HttpContext.Session.SetString("username", user.Username!);
            HttpContext.Session.SetString("rol", user.Rol.ToString());
            
            _logger.LogInformation($"El usuario: { user.Username } ha iniciado sesión.");

            return RedirectToAction("Index", "Cadete");
          }
          else
          {
            return RedirectToAction("IniciarSesion", "Login");
          }
        }

        return RedirectToAction("Index", "Cadete");
      }
      catch (System.Exception)
      {
        return RedirectToAction("Index", "Cadete");
      }
    }

    public IActionResult CerrarSesion()
    {
      string? username = HttpContext.Session.GetString("username");
      
      _logger.LogInformation($"El usuario: { username } ha cerrado sesión.");
      HttpContext.Session.Clear();

      return RedirectToAction("Index", "Cadete");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}