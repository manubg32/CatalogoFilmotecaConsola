using Catalogo.modelo;

namespace Catalogo.controlador;

public partial class GestionAv
{
    public void listarElementos(int opc)
    {
        Console.Clear();
        List<Audiovisual> tmp = new List<Audiovisual>();
        
        switch (opc)
        {
            case 1:
                
                tmp = avs;
                tmp.Sort((a1, a2) => a1.Titulo.CompareTo(a2.Titulo));
                foreach (Audiovisual a in tmp)
                {
                    Console.WriteLine(a.Describir());
                }
                break;
            case 2:
                tmp = avs;
                tmp.Sort((a1, a2) => a1.Director.CompareTo(a2.Director));
                foreach (Audiovisual a in tmp)
                {
                    Console.WriteLine(a.Describir());
                }
                break;
            case 3:
                tmp = avs;
                tmp.Sort((a1, a2) => a1.Year.CompareTo(a2.Year));
                foreach (Audiovisual a in tmp)
                {
                    Console.WriteLine(a.Describir());
                }
                break;
        }
        
        Console.WriteLine("\nEnter para continuar");
        Console.ReadLine();
    }
}