public class Puntaje{
    public int idPUntaje {get; set;}
    public int idUsuario {get; set;}
    public int idJuego {get; set;}
    public int Puntos {get; set;}

    public Puntaje(){}

    public Puntaje(int idusuario, int idjuego, int puntos){
        idUsuario = idusuario;
        idJuego = idjuego;
        Puntos = puntos;
    }
}