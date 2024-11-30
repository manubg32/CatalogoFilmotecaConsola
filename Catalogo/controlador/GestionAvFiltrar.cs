using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using Catalogo.modelo;

namespace Catalogo.controlador;

public partial class GestionAv
{
    public List<Audiovisual>
        avs = new List<Audiovisual>(); //CAMBIAR A PRIVATE y STATIC cuando termines de hacer el programa con las pruebas


    #region BUSQUEDA

    //Preguntamos al usuario si desea buscar por el atributo particular de Serie
    public void BuscarSerie(Dictionary<String, String> filtro, int n)
    {
        string f;


        Console.WriteLine("Desea filtrar por numero de capitulos [S/N]: ");
        do
        {
            f = Console.ReadLine();
        } while (f == null);

        if (f.ToLower() == "s")
        {
            Console.WriteLine("Introduzca el numero de capitulos: ");
            string nCapitulos;
            do nCapitulos = Console.ReadLine();
            while (nCapitulos == null);
            filtro.Add("nCapitulos", nCapitulos);
        }

        FiltrarTodo(filtro, n);
    }

    //Preguntamos al usuario si desea buscar por el atributo particular de Pelicula
    public void BuscarPelicula(Dictionary<String, String> filtro, int n)
    {
        string f;

        Console.WriteLine("Desea filtrar por duracion [S/N]: ");
        do f = Console.ReadLine();
        while (f == null);
        if (f.ToLower().Equals("s"))
        {
            Console.WriteLine("Introduzca la duracion en minutos: ");
            string duracion;
            do duracion = Console.ReadLine();
            while (duracion == null);
            filtro.Add("duracion", duracion);
        }

        FiltrarTodo(filtro, n);
    }

    //Primero buscamos por los atributos comunes y luego segun 'n' buscamos mas concretamente
    public void BuscarTodo(int n)
    {
        Dictionary<String, String> filtro = new Dictionary<String, String>();

        string? f;

        Console.WriteLine("Desea filtrar por titulo [S/N]: ");
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

        Console.WriteLine("Desea filtrar por director [S/N]: ");
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

        Console.WriteLine("Desea filtrar por año [S/N]: ");
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

        //Buscamos mas concretamente mandando el filtro que hemos realizado y el tipo segun 'n'
        switch (n)
        {
            case 1:
                BuscarSerie(filtro, n);
                break;
            case 2:
                BuscarPelicula(filtro, n);
                break;
            case 0:
                FiltrarTodo(filtro, n);
                break;
        }
    }

    #endregion

    #region FILTRAR

    //Filtramos por los atributos que tienen comunes todos los audiovisules y luego terminamos de filtrar segun el tipo ('n')
    private void FiltrarTodo(Dictionary<string, string> filtro, int n)
    {
        List<Audiovisual> avsFiltrados = new List<Audiovisual>();
        List<Audiovisual> tmp = new List<Audiovisual>();


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

        //Dependiendo del elemento que sea, terminamos de filtrar o listamos
        switch (n)
        {
            case 1:
                FiltrarSerie(filtro, avsFiltrados);
                break;
            case 2:
                FiltrarPelicula(filtro, avsFiltrados);
                break;
            case 0:
                listar(avsFiltrados);
                break;
        }
    }


    //SERIE Terminamos de filtrar la lista que previamente hemos filtrado y la mandamos a imprimir
    private void FiltrarSerie(Dictionary<string, string> filtro, List<Audiovisual> avsFiltrados)
    {
        List<Serie> seriesFiltradas = new List<Serie>();
        List<Serie> tmp = new List<Serie>();
        List<Serie> series = new List<Serie>();

        //Creamos una lista con todas las series
        foreach (Audiovisual a in avs)
        {
            if (a is Serie s)
            {
                series.Add(s);
            }
        }

        //Creamos una lista con solo las series que vienen en la lista de audiovisuales ya filtrados
        foreach (Audiovisual a in avsFiltrados)
        {
            if (a is Serie s)
            {
                seriesFiltradas.Add(s);
            }
        }

        //Hacemos la comprobacion para filtrar
        if (filtro.ContainsKey("nCapitulos"))
        {
            if (seriesFiltradas.Count ==
                0) //Si la lista esta vacia la creamos filtrando la lista de series creada previamente
            {
                seriesFiltradas = series.Where(s => s.NCapitulos == Int32.Parse(filtro["nCapitulos"])).ToList();
            }
            else //Si no, filtramos la lista de series que acabamos de crear a partir de los AVs filtrados
            {
                tmp = seriesFiltradas.Where(s => s.NCapitulos == Int32.Parse(filtro["nCapitulos"])).ToList();
                seriesFiltradas = tmp;
            }
        }

        //Mandamos a imprimir la lista de Series filtradas
        listarSeries(seriesFiltradas);
    }

    //PELICULA Terminamos de filtrar la lista que previamente hemos filtrado y la mandamos a imprimir
    private void FiltrarPelicula(Dictionary<string, string> filtro, List<Audiovisual> avsFiltrados)
    {
        List<Pelicula> peliculasFiltradas = new List<Pelicula>();
        List<Pelicula> tmp = new List<Pelicula>();
        List<Pelicula> peliculas = new List<Pelicula>();

        //Creamos una lista con todas las peliculas
        foreach (Audiovisual a in avs)
        {
            if (a is Pelicula p)
            {
                peliculas.Add(p);
            }
        }

        //Creamos una lista con solo las peliculas que vienen en la lista de audiovisuales ya filtrados
        foreach (Audiovisual a in avsFiltrados)
        {
            if (a is Pelicula p)
            {
                peliculasFiltradas.Add(p);
            }
        }

        //Hacemos la comprobacion para filtrar
        if (filtro.ContainsKey("duracion"))
        {
            if (peliculasFiltradas.Count ==
                0) //Si la lista esta vacia la creamos filtrando la lista de peliculas creada previamente
            {
                peliculasFiltradas = peliculas.Where(p => p.Duracion == Int32.Parse(filtro["duracion"])).ToList();
            }
            else //Si no, filtramos la lista de peliculas que acabamos de crear a partir de los AVs filtrados
            {
                tmp = peliculasFiltradas.Where(p => p.Duracion == Int32.Parse(filtro["duracion"])).ToList();
                peliculasFiltradas = tmp;
            }
        }

        //Mandamos a imprimir la lista de Series filtradas
        listarPeliculas(peliculasFiltradas);
    }

    #endregion

    #region LISTAR

    //Listamos todas las series
    private void listarSeries(List<Serie> lista)
    {
        if (lista.Count == 0)
        {
            Console.WriteLine("No existen resultados para esa busqueda");
        }
        else
        {
            Console.Clear();
            foreach (Serie s in lista)
            {
                Console.WriteLine(s.Describir());
            }

            Console.WriteLine("\nENTER PARA CONTINUAR");
            Console.ReadLine();
        }
        
        //Si hay solo un elemento en la lista le mandamos a preguntar al usuario 
        if (lista.Count == 1)
        {
            ComprobarSerie(lista[0]);
        }
    }

    //Preguntamos si desea eliminar o modificar ese elemento
    private void ComprobarSerie(Serie serie)
    {
        Console.WriteLine("\n1. Eliminar elemento \n2. Modificar elemento \nEnter para Continuar");
        string o = Console.ReadLine();
        if (o == "1")//Eliminamos el objeto ya que es por referencia
        {
            avs.RemoveAt(avs.IndexOf(serie));
        }
        else if (o == "2") //Preguntamos por los atributos nuevos y lo modificamos siempre que no de errores
        {
            Console.Clear();
            a:
            try
            {
                string titulo;
                string director;
                int year;
                int nCapitulos;

                Console.WriteLine($"MODIFICAR {serie.Titulo}");
                Console.WriteLine("Introduzca el nuevo titulo: ");
                do titulo = Console.ReadLine();
                while (titulo == "");
                Console.WriteLine("Introduzca el nuevo director: ");
                do director = Console.ReadLine();
                while (director == "");
                Console.WriteLine("Introduzca el nuevo año de publicacion: ");
                year = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Introduzca el nuevo numero de capitulos: ");
                nCapitulos = Int32.Parse(Console.ReadLine());

                serie.Titulo = titulo;
                serie.Director = director;
                serie.Year = year;
                serie.NCapitulos = nCapitulos;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error intentelo de nuevo");
                goto a;
            }
        }
    }

    //Listamos todas las peliculas
    void listarPeliculas(List<Pelicula> lista)
    {
        if (lista.Count == 0)
        {
            Console.WriteLine("No existen resultados para esa busqueda");
        }
        else
        {
            Console.Clear();
            foreach (Pelicula p in lista)
            {
                Console.WriteLine(p.Describir());
            }

            Console.WriteLine("\nENTER PARA CONTINUAR");
            Console.ReadLine();
        }

        //Si hay solo un elemento en la lista le mandamos a preguntar al usuario 
        if (lista.Count == 1)
        {
            ComprobarPelicula(lista[0]);
        }
    }

    //Preguntamos si desea eliminar o modificar ese elemento
    private void ComprobarPelicula(Pelicula pelicula)
    {
        Console.WriteLine("1. Eliminar elemento \n2. Modificar elemento \nEnter para Continuar");
        string o = Console.ReadLine();
        if (o == "1")//Eliminamos el objeto ya que es por referencia
        {
            avs.RemoveAt(avs.IndexOf(pelicula));
        }
        else if (o == "2")//Preguntamos por los atributos nuevos y lo modificamos siempre que no de errores
        {
            Console.Clear();
            a:
            try
            {
                string titulo;
                string director;
                int year;
                int duracion;

                Console.WriteLine($"MODIFICAR {pelicula.Titulo}");
                Console.WriteLine("Introduzca el nuevo titulo: ");
                do titulo = Console.ReadLine();
                while (titulo == "");
                Console.WriteLine("Introduzca el nuevo director: ");
                do director = Console.ReadLine();
                while (director == "");
                Console.WriteLine("Introduzca el nuevo año de publicacion: ");
                year = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Introduzca la nueva duracion: ");
                duracion = Int32.Parse(Console.ReadLine());

                pelicula.Titulo = titulo;
                pelicula.Director = director;
                pelicula.Year = year;
                pelicula.Duracion = duracion;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error intentelo de nuevo");
                goto a;
            }
        }
    }

    //Listamos todos los audiovisuales en general
    void listar(List<Audiovisual> lista)
    {
        if (lista.Count == 0)
        {
            Console.WriteLine("No existen resultados para esa busqueda");
        }
        else
        {
            Console.Clear();
            foreach (Audiovisual a in lista)
            {
                Console.WriteLine(a.Describir());
            }

            Console.WriteLine("\nENTER PARA CONTINUAR");
            Console.ReadLine();

            //Si solo hay un elemento en la lista, le preguntamos al usuario depende del tipo
            if (lista.Count == 1)
            {
                if (lista[0].GetType() == typeof(Serie))
                {
                    ComprobarSerie(((Serie)lista[0]));
                } else if (lista[0].GetType() == typeof(Pelicula))
                {
                    ComprobarPelicula(((Pelicula)lista[0]));
                }
            }
        }
    }

    #endregion
}