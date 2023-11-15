public class Notificacion{
    public int idNotificacion {get; set;}
    public int idUsuario1 {get; set;}
    public int idUsuario2 {get; set;}
    public int idAmistad {get; set;}
    public string Titulo {get; set;}
    public string Descripcion {get; set;}
    public int Estado {get; set;}
    public string UserPerfil {get; set;}
    public int  UserId {get; set;}
    public Usuario User {get; set;}

    public Notificacion(){}

    public Notificacion(int idusuario1, int idusuario2, int idamistad, string titulo, string descripcion, int estado){
        idUsuario1 = idusuario1;
        idUsuario2 = idusuario2;
        idAmistad = idamistad;
        Titulo = titulo;
        Descripcion = descripcion;
        Estado = estado;
    }
}