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
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        if (ViewBag.Log == "1"){
            ViewBag.Usuario = HakunaMatata.ObtenerUsuario("", HakunaMatata.ObtenerIdUsuario(HttpContext));
            ViewBag.Notificaciones = HakunaMatata.ObtenerNotificaciones("", HakunaMatata.ObtenerIdUsuario(HttpContext));
        }
        ViewBag.Juegos = HakunaMatata.ObtenerJuegos();
        return View();
    }

    [HttpGet]
    public IActionResult Juego(int idJuego){
        ViewBag.Juego = BD.ObtenerJuego(idJuego);
        switch (ViewBag.Juego.Nombre){
            case "Tetris":
                return RedirectToAction("Tetris", "Juegos");
            case "Snake":
                return RedirectToAction("Snake", "Juegos");
            case "Buscaminas":
                return RedirectToAction("Buscaminas", "Juegos");
            case "2048":
                return RedirectToAction("_2048", "Juegos");
            case "Wordle":
                return RedirectToAction("Wordle", "Juegos");
            default:
                return View();
        }
    }

    public IActionResult Perfil(){
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        ViewBag.Usuario = BD.ObtenerUsuario("", HakunaMatata.ObtenerIdUsuario(HttpContext));
        ViewBag.Notificaciones = HakunaMatata.ObtenerNotificaciones("", HakunaMatata.ObtenerIdUsuario(HttpContext));
        return View();
    }

    [HttpPost]
    public List<Notificacion> AceptarSolicitudAJAX(int id){
        BD.AceptarAmistad(id);
        return BD.ObtenerNotificaciones("", HakunaMatata.ObtenerIdUsuario(HttpContext));
    }

    [HttpPost]
    public List<Notificacion> RechazarSolicitudAJAX(int id){
        BD.RechazarAmistad(id);
        return BD.ObtenerNotificaciones("", HakunaMatata.ObtenerIdUsuario(HttpContext));
    }

    public IActionResult Login(){
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        if (ViewBag.Log == "1"){
            return RedirectToAction("Index", "Home");
        } else {
            return View();
        }
        return View();
    }

    public IActionResult LoginAction(string Nombre, string Contrasena){
        ViewBag.Log = HakunaMatata.IniciarSesion(HttpContext, Nombre, Contrasena);
        ViewBag.Juegos = HakunaMatata.ObtenerJuegos();
        if (ViewBag.Log == "1"){
            return RedirectToAction("Index", "Home");
        } else {
            return RedirectToAction("Login", "Home");
        }
    }

    public IActionResult Register(){
        ViewBag.Log = HakunaMatata.ObtenerLogStatus(HttpContext);
        if (ViewBag.Log == "1"){
            return RedirectToAction("Index", "Home");
        } else {
            return View();
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
        return RedirectToAction("Index", "Home");
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
