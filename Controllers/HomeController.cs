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

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IWebHostEnvironment Environment;

    public HomeController(IWebHostEnvironment environment)
    {
        Environment = environment;
    }

    public IActionResult Index()
    {
        ViewBag.Juegos = HakunaMatata.ObtenerJuegos();
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        return View();
    }

    [HttpGet]
    public IActionResult Juego(int juego){
        ViewBag.Juego = juego;
        return View();
    }

    public IActionResult Perfil(){
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        ViewBag.Usuario = BD.ObtenerUsuario("", HakunaMatata.ObtenerIdUsuario(HttpContext));
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

    [HttpPost]
    public IActionResult RegisterAction(string Nombre, string Contrasena, DateTime FechaNacimiento, string Mail, IFormFile MyFile = null){
        string urlArchivo = "";
        if (MyFile != null){
            SubirFotoPerfil(Nombre, MyFile);
            urlArchivo = @$"\assets\fotosPerfil\foto-{Nombre}{System.IO.Path.GetExtension(MyFile.FileName)}";
        } else {
            urlArchivo = "/assets/fotosPerfil/foto-anonimo.jpg";
        }
        string hashContrasena = BCrypt.Net.BCrypt.HashPassword(Contrasena);
        ViewBag.Log = HakunaMatata.Registrarse(HttpContext, Nombre, hashContrasena, Mail, FechaNacimiento, urlArchivo);
        ViewBag.PopUP = 1;
        return View("Index", "Home");
    }

    public void SubirFotoPerfil(string Nombre, IFormFile MyFile){
        string wwwRootLocal = this.Environment.ContentRootPath + @$"\wwwroot\assets\fotosPerfil\foto-{Nombre}{System.IO.Path.GetExtension(MyFile.FileName)}";
        using (var stream = System.IO.File.Create(wwwRootLocal)){
            MyFile.CopyToAsync(stream);
        }
    }
 
    [HttpPost] public Usuario EnviarSolicitudAJAX(string nombreUsuario){
        return BD.AgregarAmistad(nombreUsuario, HakunaMatata.ObtenerIdUsuario(HttpContext));
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
