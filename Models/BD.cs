using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Buffers.Text;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO.Compression;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;


public static class BD
{
    private static string _connectionString = "Server=localhost;DataBase=HakunaMatata;Trusted_Connection=True;";

    public static Usuario ObtenerUsuario(string Nombre, string idUsuario){
        string sql = "SELECT * FROM Usuarios WHERE Nombre = @Nombre OR idUsuario = @idUsuario";
        using (SqlConnection db = new SqlConnection(_connectionString)){
            return db.QueryFirstOrDefault<Usuario>(sql, new {Nombre, idUsuario});
        }
    }

    public static void AgregarUsuario(Usuario user){
        string sql = "INSERT INTO Usuarios (Nombre, Contrasena, Mail, FechaNacimiento, FotoPerfil, Token) VALUES (@Nombre, @Contrasena, @Mail, @FechaNacimiento, @FotoPerfil, @Token)";
        using (SqlConnection db = new SqlConnection(_connectionString)){
            db.Execute(sql, new {user.Nombre, user.Contrasena, user.Mail, user.FechaNacimiento, user.FotoPerfil, user.Token});
        }
    }

    public static List<Juego> ObtenerJuegos(){
        string sql = "SELECT * FROM Juegos";
        using (SqlConnection db = new SqlConnection(_connectionString)){
            return db.Query<Juego>(sql).ToList();
        }
    }

    public static Juego ObtenerJuego(int idJuego){
        string sql = "SELECT * FROM Juegos WHERE idJuego = @idJuego";
        using (SqlConnection db = new SqlConnection(_connectionString)){
            return db.QueryFirstOrDefault<Juego>(sql, new {idJuego});
        }
    }

    public static List<Puntaje> ObtenerPuntajes(int idJuego){
        string sql = "SELECT * FROM Puntajes WHERE idJuego = @idJuego";
        using (SqlConnection db = new SqlConnection(_connectionString)){
            return db.Query<Puntaje>(sql, new {idJuego}).ToList();
        }
    }

    public static Usuario AgregarAmistad(string nombreUsuario, string idUser){
        Usuario user = ObtenerUsuario(nombreUsuario, "0");
        string sql = "EXEC sp_EnviarSolicitud @idUsuario1, @idUsuario2";
        using (SqlConnection db = new SqlConnection(_connectionString)){
            db.Execute(sql, new {idUsuario1 = Int32.Parse(idUser), idUsuario2 = user.idUsuario});
        }
        return user;
    }

    public static List<Notificacion> ObtenerNotificaciones(string nombre, string idUsuario){
        string sql = "SELECT * FROM Notificaciones WHERE idUsuario2 = @idUsuario";
        using (SqlConnection db = new SqlConnection(_connectionString)){
            return db.Query<Notificacion>(sql, new {idUsuario}).ToList();
        }
    }
}