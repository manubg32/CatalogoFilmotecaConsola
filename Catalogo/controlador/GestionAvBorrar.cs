using Catalogo.modelo;

namespace Catalogo.controlador;

public partial class GestionAv
{
    public void EliminarElemento()
    {
        List<Audiovisual> avsFiltrados = new List<Audiovisual>();
        List<Audiovisual> tmp = new List<Audiovisual>();
        Dictionary<String, String> filtro = new Dictionary<String, String>();

        string? f;

        //Preguntamos al usuario por que atributo o atributos desea buscar el audiovisual para eliminar
        Console.Clear();
        Console.WriteLine("ELIMINAR ELEMENTO/S\n");
        Console.WriteLine("Desea eliminar por titulo [S/N]: ");
        do f = Console.ReadLine();
        while (f == null);
        if (f.ToLower() == "s")
        {
            Console.WriteLine("Introduzca el titulo: ");
            string titulo;
            do titulo = Console.ReadLine();
            while (titulo == null);
            filtro.Add("titulo", titulo);
        }

        Console.WriteLine("Desea eliminar por director [S/N]: ");
        do f = Console.ReadLine();
        while (f == null);
        if (f.ToLower().Equals("s"))
        {
            Console.WriteLine("Introduzca el director: ");
            string director;
            do director = Console.ReadLine();
            while (director == null);
            filtro.Add("director", director);
        }

        Console.WriteLine("Desea eliminar por año [S/N]: ");
        do f = Console.ReadLine();
        while (f == null);
        if (f.ToLower().Equals("s"))
        {
            Console.WriteLine("Introduzca el año: ");
            string year;
            do year = Console.ReadLine();
            while (year == null);
            filtro.Add("year", year);
        }

        //Una vez tengamos los atributos, filtramos la lista para obtener resultados
        if (filtro.ContainsKey("titulo"))
        {
            if (avsFiltrados.Count == 0)
            {
                avsFiltrados = avs.Where(a => a.Titulo.ToLower().Contains(filtro["titulo"].ToLower())).ToList();
            }
            else
            {
                tmp = avsFiltrados.Where(a => a.Titulo.ToLower().Contains(filtro["titulo"].ToLower())).ToList();
                avsFiltrados = tmp;
            }
        }

        if (filtro.ContainsKey("director"))
        {
            if (avsFiltrados.Count == 0)
            {
                avsFiltrados = avs.Where(a => a.Director.ToLower().Contains(filtro["director"].ToLower())).ToList();
            }
            else
            {
                tmp = avsFiltrados.Where(a => a.Director.ToLower().Contains(filtro["director"].ToLower())).ToList();
                avsFiltrados = tmp;
            }
        }

        if (filtro.ContainsKey("year"))
        {
            if (avsFiltrados.Count == 0)
            {
                avsFiltrados = avs.Where(a => a.Year == Int32.Parse(filtro["year"])).ToList();
            }
            else
            {
                tmp = avsFiltrados.Where(a => a.Year == Int32.Parse(filtro["year"])).ToList();
                avsFiltrados = tmp;
            }
        }

        switch (avsFiltrados.Count)
        {
            case 0:
                Console.Clear();
                Console.WriteLine("No existen registros con el filtro indicado \nEnter para continuar: ");
                Console.ReadLine();
                break;
            case 1:
                int opc = 0;
                Console.Clear();
                Console.WriteLine($"1. Eliminar {avsFiltrados[0].Describir()}\n2. Cancelar");
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    avs.RemoveAt(avs.IndexOf(avsFiltrados[0]));
                }

                break;
            case > 1:
                Console.Clear();
                Console.WriteLine($"Existen diversos registros con esos datos: \n");
                foreach (Audiovisual a in avsFiltrados)
                {
                    Console.WriteLine(a.Describir());
                }

                Console.WriteLine("\n1. Eliminar todos los registros\n2. Cancelar");
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    for (int i = avsFiltrados.Count - 1; i >= 0; i--)
                    {
                        avs.RemoveAt(avs.IndexOf(avsFiltrados[i]));
                    }
                }
                break;
                
                
        }
        
    }
}