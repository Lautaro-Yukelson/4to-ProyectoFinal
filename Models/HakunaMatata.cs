using System.ComponentModel.Design.Serialization;
using System.Buffers.Text;
using System;
using BCrypt.Net;
using System.Security;
using Newtonsoft.Json;
using System.Resources;
using System.Threading;
using System.ComponentModel;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;


public static class HakunaMatata{

    public static string IniciarSesion(HttpContext context, string Nombre, string Contrasena){
        Usuario user = HakunaMatata.ObtenerUsuario(Nombre);
        if (user != null){
            if (BCrypt.Net.BCrypt.Verify(Contrasena, user.Contrasena)){ 
                string logStatus = "1";

                CookieOptions idUser = new CookieOptions { Expires = DateTime.Now.AddDays(7) };
                context.Response.Cookies.Append("idUser", user.idUsuario.ToString(), idUser);

                CookieOptions log = new CookieOptions { Expires = DateTime.Now.AddDays(7) };
                context.Response.Cookies.Append("log", logStatus, log);

                return logStatus;
            } else {
                return "0"; 
            }
        } else {
            return "-1";
        }
    }

    public static string Registrarse(HttpContext context, string Nombre, string Contrasena, string Mail){
        Usuario user = HakunaMatata.ObtenerUsuario(Nombre);
        if (user == null){
            string logStatus = "1";
            BD.AgregarUsuario(new Usuario(Nombre, Contrasena, Mail, "token"));

            user = HakunaMatata.ObtenerUsuario(Nombre);

            CookieOptions idUser = new CookieOptions { Expires = DateTime.Now.AddDays(7) };
            context.Response.Cookies.Append("idUser", user.idUsuario.ToString(), idUser);

            CookieOptions log = new CookieOptions { Expires = DateTime.Now.AddDays(7) };
            context.Response.Cookies.Append("log", logStatus, log);

            return logStatus;
        } else {
            return "-2";  
        }
    }

    public static void Logout(HttpContext context){
        foreach (var cookie in context.Request.Cookies.Keys) { context.Response.Cookies.Delete(cookie); }
    }   

    public static string ObtenerLogStatus(HttpContext context){
        var cookie = context.Request.Cookies["log"];
        if (cookie == null){
            return "null";
        } else {
            return cookie;
        }
    }

    public static Usuario ObtenerUsuario(string Nombre){
        return BD.ObtenerUsuario(Nombre);
    }

    public static List<Juego> ObtenerJuegos(){
        return BD.ObtenerJuegos();
    }

    public static List<Puntaje> ObtenerPuntajes(int idJuego){
        return BD.ObtenerPuntajes(idJuego);
    }
}