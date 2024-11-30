using Catalogo.modelo;

namespace Catalogo.controlador;

public partial class GestionAv
{
    public void AltaPelicula(string titulo, string? director, int year, int duracion)
    {
        //damos de alta una pelicula y la añadimos a la lista de audiovisuales

        Pelicula nueva = new Pelicula(titulo, director, year, duracion);
        avs.Add(nueva);
    }

    public void AltaSerie(string? titulo, string? director, int year, int nCapitulos)
    {
        //damos de alta una serie y la añadimos a la lista de audiovisuales
        Serie nueva = new Serie(titulo, director, year, nCapitulos);
        avs.Add(nueva);
    }
}