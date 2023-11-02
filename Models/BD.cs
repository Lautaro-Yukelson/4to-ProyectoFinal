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

    public static List<Puntaje> ObtenerPuntajes(int idJuego){
        string sql = "SELECT * FROM Puntajes WHERE idJuego = @idJuego";
        using (SqlConnection db = new SqlConnection(_connectionString)){
            return db.Query<Puntaje>(sql, new {idJuego}).ToList();
        }
    }
}