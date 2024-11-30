namespace Catalogo.modelo;

public class Pelicula : Audiovisual
{
    private int duracion;
    
    public int Duracion
    {
        get
        {
            return duracion;
        }
        set
        {
            if (value > 0)
            {
                duracion = value;
            }
            else
            {
                throw new Exception("La duracion debe ser superior a 0");
            }
        }
    }

    public Pelicula(String titulo, String director, int year, int duracion) : base(titulo, director, year)
    {
        Duracion = duracion;

    }

    public override string Describir()
    {
        return $"La pelicula {Titulo} ({Year}), dirijida por el director: {Director}, tiene una duracion de {Duracion} minutos.";
    }
}