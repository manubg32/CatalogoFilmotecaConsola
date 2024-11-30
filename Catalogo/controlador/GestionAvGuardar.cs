using Catalogo.modelo;

namespace Catalogo.controlador;


public  partial class GestionAv
{
    private const char MARCA_SERIE = 'S';
    private const char MARCA_PELICULA = 'P';
    
    public static void GuardarEnArchivo(List<Audiovisual> audiovisuales)
    {
        if (File.Exists("catalogo.dat"))
        {
            DateTime dt = DateTime.Now;
            File.Move("catalogo.dat", $"catalogo_{dt.Day}-{dt.Month}-{dt.Year}_{dt.Ticks}_.dat");
        }

        int numGuardados = 0;

        try
        {
            using (var fileStr = new FileStream("catalogo.dat", FileMode.Create))
            {
                using (var bw = new BinaryWriter(fileStr))
                {
                    foreach (var a in audiovisuales)
                    {
                        GuardarAudiovisual(a, bw);
                        numGuardados++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: Error durantre el guardado del catálogo. Se han escrito {numGuardados} registros.\nDetalles: {ex.ToString()}");
        }
    }
    
    private static void GuardarAudiovisual(Audiovisual a, BinaryWriter bw)
    {
        if(a is Serie)
        {
            Serie serie = (Serie) a;
            bw.Write(MARCA_SERIE);
            GuardarComun(a, bw);
            bw.Write(serie.NCapitulos);
        }else if(a is Pelicula)
        {
            Pelicula pelicula = (Pelicula)a;
            bw.Write(MARCA_PELICULA);
            GuardarComun(a, bw);
            bw.Write(pelicula.Duracion);
        }
    }
    
    private static void GuardarComun(Audiovisual a, BinaryWriter bw)
    {
        bw.Write(a.Titulo.CompletarHasta(Audiovisual.MAX_TITULO));
        bw.Write(a.Director.CompletarHasta(Audiovisual.MAX_DIRECTOR));
        bw.Write(a.Year);
        bw.Write(a.idFoto);
    }

   
    
    
    
    
    
    
    
    
    
    
    
}