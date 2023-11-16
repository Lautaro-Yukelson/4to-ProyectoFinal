public class Amistad{
    public int idAmistad {get; set;}
    public int idUsuario1 {get; set;}
    public int idUsuario2 {get; set;}
    public int Estado {get; set;}

    public Amistad(){}

    public Amistad(int idusuario1, int idusuario2, int estado){
        idUsuario1 = idusuario1;
        idUsuario2 = idusuario2;
        Estado = estado;
    }    
}