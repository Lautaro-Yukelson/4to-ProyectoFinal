public class Usuario{
    public int idUsuario {get; set;}
    public string Nombre {get; set;}
    public string Contrasena {get; set;}
    public string Mail {get; set;}
    public string Token {get; set;}

    public Usuario(){}

    public Usuario(string nombre, string contrasena, string mail, string token){
        Nombre = nombre;
        Contrasena = contrasena;
        Mail = mail;
        Token = token;
    }
}