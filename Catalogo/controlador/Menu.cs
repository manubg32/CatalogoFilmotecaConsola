using System.Threading.Channels;
using Catalogo.modelo;

namespace Catalogo.controlador;

class Menu
{
    static GestionAv ga = new GestionAv(); //Instanciamos un GestorAv para que se encargue de lo necesario

    static void Main(string[] args)
    {
        //Cargamos la lista del fichero
        ga.CargarLista();
        
        //Mostramos el menuPrincipal al iniciar la aplicacion
        MenuPrincipal();
    }

    private static void MenuPrincipal()
    {

        int? opc = null;

        while (opc != 0)
        {
            //Mostramos el menu y pedimos opcion al usuario
            Console.Clear();
            Console.WriteLine("GESTION DE AUDIOVISUALES\n");
            Console.WriteLine("1. Dar de alta un elemento");
            Console.WriteLine("2. Buscar elemento");
            Console.WriteLine("3. Eliminar elemento");
            Console.WriteLine("4. Listar elementos");
            Console.WriteLine("0. Salir");
            Console.WriteLine("\nOpcion: ");
            opc = Int32.Parse(Console.ReadLine());

            switch (opc)
            {
                //Depende de la opcion del usuario mostramos el menu correspondiente
                case 1:
                    menuAlta();
                    break;
                case 2:
                    menuBuscar();
                    break; 
                case 3: ga.EliminarElemento();
                    break;
                case 4:
                    menuListar();
                    break;
            }
        }
        //si la opcion es 0, guardamos la lista y cerramos la aplicacion
        GestionAv.GuardarEnArchivo(ga.avs);
        Console.Clear();
        Console.WriteLine("PROGRAMA FINALIZADO");
    }

    private static void menuListar()
    {
        int opc;
        do
        {
            Console.Clear();
            Console.WriteLine("MENU LISTAR\n");
            Console.WriteLine("Seleccione el atributo por el que ordenar: \n1. Titulo \n2. Director \n3. Año");
            opc = Int32.Parse(Console.ReadLine());
            if (opc == 1 || opc == 2 || opc == 3)
            {
                ga.listarElementos(opc);
            }
        } while (opc != 1 && opc != 2 && opc != 3);
    }

    private static void menuBuscar()
    {
        string opc = "";

        Console.Clear();
        Console.WriteLine("BUSCAR ELEMENTOS\n");
        Console.WriteLine("¿Deseas filtrar series (S), películas (P) o todo (T)? [S/P/T]: ");
        do
        {
            opc = Console.ReadLine();
        } while (opc.ToLower() != "s" && opc.ToLower() != "p" && opc.ToLower() != "t");


        switch (opc)
        {
            case "s":
                ga.BuscarTodo(1);
                break;
            case "p":
                ga.BuscarTodo(2);
                break;
            case "t":
                ga.BuscarTodo(0);
                break;
        }
    }

    private static void menuAlta()
    {
        //Mostramos las opciones de alta al usuario
        int opc = 0;

        Console.Clear();
        Console.WriteLine("DAR DE ALTA\n");
        Console.WriteLine("1. Pelicula");
        Console.WriteLine("2. Serie");
        Console.WriteLine("\nOpcion: ");
        do
        {
            opc = Int32.Parse(Console.ReadLine());
        } while (opc != 1 && opc != 2);

        switch (opc)
        {
            //Segun la opcion lo llevamos al menú correspondiente
            case 1:
                MenuAltaPelicula();
                break;
            case 2:
                MenuAltaSerie();
                break;
        }
    }

    private static void MenuAltaSerie()
    {
        //Pedimos la informacion al usuario
        try
        {
            Console.Clear();
            Console.WriteLine("DAR DE ALTA SERIE\n");
            Console.WriteLine("Titulo: ");
            String titulo = Console.ReadLine();
            Console.WriteLine("Director: ");
            String director = Console.ReadLine();
            Console.WriteLine("Año: ");
            int year = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Numero de capitulos: ");
            int nCapitulos = Int32.Parse(Console.ReadLine());

            ga.AltaSerie(titulo, director, year, nCapitulos);
        }
        catch (Exception
               e) //Si se lanza alguna excepcion la copturamos, se la mostramos y lo llevamos de vuelta a este menu
        {
            Console.Clear();
            Console.WriteLine(e.Message);
            Console.WriteLine("[Enter para continuar]");
            Console.ReadLine();
            MenuAltaPelicula();
        }
    }

    private static void MenuAltaPelicula()
    {
        //Pedimos la informacion al usuario
        try
        {
            Console.Clear();
            Console.WriteLine("DAR DE ALTA PELICULA\n");
            Console.WriteLine("Titulo: ");
            String titulo = Console.ReadLine();
            Console.WriteLine("Director: ");
            String director = Console.ReadLine();
            Console.WriteLine("Año: ");
            int year = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Duracion en minutos: ");
            int duracion = Int32.Parse(Console.ReadLine());

            ga.AltaPelicula(titulo, director, year, duracion);
        }
        catch (Exception
               e) //Si se lanza alguna excepcion la copturamos, se la mostramos y lo llevamos de vuelta a este menu
        {
            Console.Clear();
            Console.WriteLine(e.Message);
            Console.WriteLine("[Enter para continuar]");
            Console.ReadLine();
            MenuAltaPelicula();
        }
    }
}