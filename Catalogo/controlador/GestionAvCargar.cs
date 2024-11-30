using Catalogo.modelo;

namespace Catalogo.controlador;

public partial class GestionAv
{
    
    private const int BYTES_REG_SERIE = 215;
    private const int BYTES_REG_PELICULA = 215;
    
    
    public static List<Audiovisual> CargarDeArchivo()
    {
        var lista = new List<Audiovisual>();

        if (File.Exists("catalogo.dat"))
        {
            try
            {
                using (var fileStr = new FileStream("catalogo.dat", FileMode.Open))
                {
                    using (BinaryReader brFile = new BinaryReader(fileStr))
                    {
                        while (fileStr.Position < fileStr.Length - sizeof(Char))
                        {
                            char marca = brFile.ReadChar();
                            switch (marca)
                            {
                                case MARCA_SERIE:
                                    if (brFile.SePuedenLeer(BYTES_REG_SERIE - sizeof(Char)))
                                    {
                                        lista.Add(CargarSerie(brFile));
                                    }

                                    break;
                                case MARCA_PELICULA:
                                    if (brFile.SePuedenLeer(BYTES_REG_PELICULA - sizeof(Char)))
                                    {
                                        lista.Add(CargarPelicula(brFile));
                                    }

                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Error durantre la carga del catálogo. Se ha leído {lista.Count} registros.\nDetalles: {e.ToString()}");
            }
        }

        return lista;
    }

    private static Audiovisual CargarPelicula(BinaryReader brFile)
    {
        string titulo = brFile.ReadString().Trim();
        string director = brFile.ReadString().Trim();
        int year = brFile.ReadInt32();
        int idFoto = brFile.ReadInt32();
        int duracion = brFile.ReadInt32();

        return new Pelicula(titulo, director, year, duracion);
    }

    private static Audiovisual CargarSerie(BinaryReader br)
    {
        string titulo = br.ReadString().Trim();
        string director = br.ReadString().Trim();
        int year = br.ReadInt32();
        int idFoto = br.ReadInt32();
        int nCapitulos = br.ReadInt32();
        
        return new Serie(titulo, director, year, nCapitulos);
    }

    public void CargarLista()
    {
        avs = CargarDeArchivo();
    }
}
