namespace Catalogo.controlador;

public static class UtilsStatic
{
    public static string CompletarHasta(this string str, int size)
    {
        return str.PadRight(size, ' ');
    }

    public static bool SePuedenLeer(this BinaryReader br, int numBytes)
    {
        bool sePuede = false;
        if (br != null)
        {
            sePuede = br.BaseStream.Length - br.BaseStream.Position >= numBytes;
        }
        return sePuede;
    }
}