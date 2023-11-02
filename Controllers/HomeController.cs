using System;
using System.Net;
using System.Diagnostics;
using System.Buffers.Text;
using Microsoft.AspNetCore.Mvc;
using _4to_ProyectoFinal.Models;
using System.ComponentModel.Design;
using System.Reflection.PortableExecutable;

namespace _4to_ProyectoFinal.Controllers;

/*public class CargarUsuarioAttribute : ActionFilterAttribute{
    public override void OnActionExecuting(ActionExecutingContext filterContext){
        var cookie = filterContext.HttpContext.Request.Cookies["log"];
        if (cookie != null){
            filterContext.Controller.ViewBag.Log = cookie.Value;
        }
        base.OnActionExecuting(filterContext);
    }
}

[CargarUsuario]*/
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.Juegos = HakunaMatata.ObtenerJuegos();
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        return View();
    }

    public IActionResult Login(){
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        return View();
    }

    public IActionResult LoginAction(string Nombre, string Contrasena){
        ViewBag.Log = HakunaMatata.IniciarSesion(HttpContext, Nombre, Contrasena);
        if (ViewBag.Log == "1"){
            ViewBag.AlertSesion = 1;
            return View("Index", "Home");
        } else {
            ViewBag.AlertSesion = 0;
            return View("Login", "Home");
        }
    }

    public IActionResult Register(){
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        if (ViewBag.Log == "1"){
            return View("Index", "Home");
        } else {
            return View("Register", "Home");
        }
    }

    public IActionResult RegisterAction(string Nombre, string Contrasena, string Mail){
        string hashContrasena = BCrypt.Net.BCrypt.HashPassword(Contrasena);
        ViewBag.Log = HakunaMatata.Registrarse(HttpContext, Nombre, hashContrasena, Mail);
        ViewBag.PopUP = 1;
        return View("Index", "Home");
    }

    public IActionResult Logout(){
        HakunaMatata.Logout(HttpContext);
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
