using System.IO.Compression;
using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Buffers.Text;
using Microsoft.AspNetCore.Mvc;
using _4to_ProyectoFinal.Models;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Hosting;
using System.Security.AccessControl;
using System.Reflection.PortableExecutable;

namespace _4to_ProyectoFinal.Controllers;

public class JuegosController : Controller
{
    private readonly ILogger<JuegosController> _logger;
    private IWebHostEnvironment Environment;

    public JuegosController(IWebHostEnvironment environment)
    {
        Environment = environment;
    }

    public IActionResult Tetris()
    {
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        return View();
    }

    public IActionResult Snake()
    {
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        return View();
    }

    public IActionResult Buscaminas()
    {
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        return View();
    }
    public IActionResult _2048()
    {
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        return View();
    }
    public IActionResult Wordle()
    {
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
