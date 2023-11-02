public class Usuario{
    public int idUsuario {get; set;}
    public string Nombre {get; set;}
    public string Contrasena {get; set;}
    public string Mail {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public string FotoPerfil {get; set;}
    public string Token {get; set;}

    public Usuario(){}

    public Usuario(string nombre, string contrasena, string mail, DateTime fechanacimiento, string fotoperfil, string token){
        Nombre = nombre;
        Contrasena = contrasena;
        Mail = mail;
        FechaNacimiento = fechanacimiento;
        FotoPerfil = fotoperfil;
        Token = token;
    }
}