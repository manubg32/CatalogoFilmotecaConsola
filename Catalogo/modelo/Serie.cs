namespace Catalogo.modelo;

public class Serie : Audiovisual
{

    private int nCapitulos;

    public int NCapitulos
    {
        get
        {
            return nCapitulos;
        }
        set
        {
            if (value > 0)
            {
                nCapitulos = value;
            }
            else
            {
                throw new Exception("El numero de capitulos no puede ser negativo");
            }
        }
    }


    public Serie (String titulo, String director, int year, int nCapitulos) : base(titulo, director, year)
    {
        NCapitulos = nCapitulos;
    }
    
    public override string Describir()
    {
        return $"La serie {Titulo} ({Year}), dirijida por el director: {Director}, tiene un total de {NCapitulos} capitulos.";
    }
    
}